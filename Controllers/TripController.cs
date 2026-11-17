using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebVanChuyenLogicstic.Data;
using WebVanChuyenLogicstic.Filters;
using WebVanChuyenLogicstic.Helpers;
using WebVanChuyenLogicstic.Models;
using WebVanChuyenLogicstic.Models.Admin;
using WebVanChuyenLogicstic.Services;

namespace WebVanChuyenLogicstic.Controllers;

[Route("Admin/Trip/{action=Index}")]
[AdminOnly(AdminRoles.Dispatcher)]
public class TripController(AppDbContext db, IOrderNotifier notifier) : Controller
{
    private const string Dir = "~/Views/Admin/Trip/";

    public IActionResult Index(string? status, int? page) //Lấy chuyến kèm xe, tài xế, danh sách đơn
    {
        var q = db.Trips.AsNoTracking()
            .Include(t => t.Vehicle).Include(t => t.Driver)
            .Include(t => t.Orders).ThenInclude(l => l.Order)
            .AsQueryable();
        if (!string.IsNullOrEmpty(status) && TripStatuses.All.Contains(status))
            q = q.Where(t => t.Status == status);
        ViewBag.Status = status;
        return View(Dir + "Index.cshtml", q.OrderByDescending(t => t.CreatedAt).ToPagedList(page));
    }

    [HttpGet("{id}")]
    public IActionResult Details(int id) //Lấy một chuyến đầy đủ; không có thì 404.
    {
        var t = db.Trips.AsNoTracking()
            .Include(x => x.Vehicle).Include(x => x.Driver)
            .Include(x => x.Orders).ThenInclude(l => l.Order)
            .FirstOrDefault(x => x.Id == id);
        return t is null ? NotFound() : View(Dir + "Details.cshtml", t);
    }

    // ---------- TẠO ----------

    [HttpGet]
    public IActionResult Create()
    {
        var vm = new TripFormViewModel();
        Fill(null);
        return View(Dir + "Form.cshtml", vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(TripFormViewModel model)
    {
        Validate(model, null);
        if (!ModelState.IsValid) { Fill(null); return View(Dir + "Form.cshtml", model); }

        var trip = new Trip { Status = TripStatuses.Planned, CreatedAt = DateTime.Now };
        db.Trips.Add(trip);
        Save(trip, model);
        TempData["SuccessMessage"] = "Đã tạo chuyến vận chuyển.";
        return RedirectToAction(nameof(Details), new { id = trip.Id });
    }

    // ---------- SỬA (chỉ khi chưa khởi hành) ----------

    [HttpGet("{id}")]
    public IActionResult Edit(int id)
    {
        var t = db.Trips.AsNoTracking().Include(x => x.Orders).FirstOrDefault(x => x.Id == id);
        if (t is null) return NotFound();
        if (t.Status != TripStatuses.Planned)
            return Fail(id, "Chuyến đã khởi hành hoặc đã kết thúc nên không sửa được.");

        Fill(id);
        return View(Dir + "Form.cshtml", new TripFormViewModel
        {
            Id = t.Id,
            Route = t.Route,
            VehicleId = t.VehicleId,
            DriverId = t.DriverId,
            DepartAt = t.DepartAt,
            Note = t.Note,
            OrderIds = t.Orders.Select(l => l.OrderId).ToList()
        });
    }

    [HttpPost("{id}")]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, TripFormViewModel model)
    {
        var trip = db.Trips.Include(x => x.Orders).FirstOrDefault(x => x.Id == id);
        if (trip is null) return NotFound();
        if (trip.Status != TripStatuses.Planned)
            return Fail(id, "Chuyến đã khởi hành hoặc đã kết thúc nên không sửa được.");

        model.Id = id;
        Validate(model, id);
        if (!ModelState.IsValid) { Fill(id); return View(Dir + "Form.cshtml", model); }

        Save(trip, model);
        TempData["SuccessMessage"] = "Đã lưu chuyến.";
        return RedirectToAction(nameof(Details), new { id });
    }

    // ---------- TRẠNG THÁI CHUYẾN ----------

    [HttpPost("{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SetStatus(int id, string status)
    {
        var trip = db.Trips.Include(x => x.Orders).FirstOrDefault(x => x.Id == id);
        if (trip is null) return NotFound();
        if (!TripStatuses.Next(trip.Status).Contains(status))
            return Fail(id, $"Không thể chuyển từ \"{trip.Status}\" sang \"{status}\".");

        if (status == TripStatuses.Running)
        {
            if (trip.Orders.Count == 0)
                return Fail(id, "Chuyến chưa có đơn nào.");
            var v = db.Vehicles.Find(trip.VehicleId);
            var d = db.Drivers.Find(trip.DriverId);
            if (v is null || v.Status != "Đang hoạt động" || d is null || d.Status != "Đang làm việc")
                return Fail(id, "Xe hoặc tài xế hiện không sẵn sàng.");
            if (db.Trips.Any(t => t.Id != id && t.Status == TripStatuses.Running
                    && (t.VehicleId == trip.VehicleId || t.DriverId == trip.DriverId)))
                return Fail(id, "Xe hoặc tài xế này đang chạy một chuyến khác.");
        }

        if (status == TripStatuses.Done)
        {
            // Chỉ hoàn thành chuyến khi mọi đơn trên chuyến đã được ghi nhận "Đã giao" hoặc "Hoàn hàng"
            var undelivered = db.Orders
                .Where(o => db.TripOrders.Any(l => l.TripId == id && l.OrderId == o.Id)
                            && o.Status != OrderStatuses.Delivered && o.Status != OrderStatuses.Returned)
                .Select(o => o.OrderCode).ToList();
            if (undelivered.Count > 0)
                return Fail(id, $"Chưa thể hoàn thành chuyến: còn {undelivered.Count} đơn chưa giao ({string.Join(", ", undelivered)}).");
        }

        if (status == TripStatuses.Cancelled)
        {
            // Hủy chuyến: trả các đơn về trạng thái chưa phân công
            var ids = trip.Orders.Select(l => l.OrderId).ToList();
            foreach (var o in db.Orders.Include(x => x.Events).Where(x => ids.Contains(x.Id)))
            {
                o.VehicleId = null; o.DriverId = null;
                o.AddEvent("Chuyến vận chuyển bị hủy, đơn chờ phân công lại");
            }
            db.TripOrders.RemoveRange(trip.Orders);
        }
        //  đồng bộ trạng thái đơn theo chuyến 
        var changed = new List<Order>();   // các đơn thực sự bị đổi trạng thái, để gửi mail
        if (status == TripStatuses.Running || status == TripStatuses.Done)
        {
            var orderIds = trip.Orders.Select(l => l.OrderId).ToList();
            var orders = db.Orders.Include(x => x.Events).Where(x => orderIds.Contains(x.Id)).ToList();
            foreach (var o in orders)
            {
                var ok = status == TripStatuses.Running
                    ? o.StartTransit("Chuyến vận chuyển bắt đầu chạy")      // Đã xác nhận -> Đang vận chuyển
                    : o.CompleteByTrip("Chuyến vận chuyển đã hoàn thành");  // -> Đã giao + mốc Hoàn thành
                if (ok) changed.Add(o);
            }
        }

        trip.Status = status;
        db.SaveChanges();  // lưu chuyến + đơn trong cùng một lần
        TempData["SuccessMessage"] = $"Đã chuyển chuyến sang \"{status}\".";

        // gửi email báo khách (lỗi mail không làm hỏng việc đổi trạng thái) 
        var mailFailed = false;
        foreach (var o in changed.Where(x => !string.IsNullOrWhiteSpace(x.Email)))
        {
            var url = Url.Action("Index", "Tracking", new { code = o.OrderCode }, Request.Scheme)!;
            try
            {
                if (await notifier.SendStatusAsync(o, null, url)) o.AddEvent("Đã gửi email thông báo");
                else mailFailed = true;
            }
            catch { mailFailed = true; }
        }
        if (changed.Count > 0) db.SaveChanges();
        if (mailFailed)
            TempData["ErrorMessage"] = "Đã cập nhật trạng thái các đơn nhưng một số email chưa gửi được (kiểm tra cấu hình SMTP / xem log).";

        return RedirectToAction(nameof(Details), new { id });
    }

    // ---------- helper ----------

    private IActionResult Fail(int id, string msg)
    {
        TempData["ErrorMessage"] = msg;
        return RedirectToAction(nameof(Details), new { id });
    }

    // Đơn đang thuộc một chuyến khác còn hoạt động
    private IQueryable<int> BusyOrderIds(int? tripId) =>
        db.TripOrders
            .Where(l => l.TripId != (tripId ?? 0)
                && (l.Trip!.Status == TripStatuses.Planned || l.Trip.Status == TripStatuses.Running))
            .Select(l => l.OrderId);

    private void Fill(int? tripId)
    {
        ViewBag.Vehicles = db.Vehicles.AsNoTracking().Where(v => v.Status == "Đang hoạt động").OrderBy(v => v.PlateNumber).ToList();
        ViewBag.Drivers = db.Drivers.AsNoTracking().Where(d => d.Status == "Đang làm việc").OrderBy(d => d.FullName).ToList();
        var busy = BusyOrderIds(tripId);
        ViewBag.Orders = db.Orders.AsNoTracking().Include(o => o.Service)
            .Where(o => o.Status == OrderStatuses.Confirmed && !busy.Contains(o.Id))
            .OrderBy(o => o.CreatedAt).ToList();
    }

    private void Validate(TripFormViewModel m, int? tripId)
    {
        var vehicle = m.VehicleId is int vid ? db.Vehicles.AsNoTracking().FirstOrDefault(x => x.Id == vid) : null;
        var driver = m.DriverId is int did ? db.Drivers.AsNoTracking().FirstOrDefault(x => x.Id == did) : null;

        if (m.VehicleId != null && (vehicle is null || vehicle.Status != "Đang hoạt động"))
            ModelState.AddModelError(nameof(m.VehicleId), "Chỉ chọn được xe đang hoạt động.");
        if (m.DriverId != null && (driver is null || driver.Status != "Đang làm việc"))
            ModelState.AddModelError(nameof(m.DriverId), "Chỉ chọn được tài xế đang làm việc.");

        var ids = m.OrderIds.Distinct().ToList();
        var busy = BusyOrderIds(tripId).ToList();
        var orders = db.Orders.AsNoTracking().Include(o => o.Service).Where(o => ids.Contains(o.Id)).ToList();

        foreach (var id in ids)
        {
            var o = orders.FirstOrDefault(x => x.Id == id);
            if (o is null || o.Status != OrderStatuses.Confirmed)
                ModelState.AddModelError(nameof(m.OrderIds), "Chỉ ghép được đơn ở trạng thái \"Đã xác nhận\".");
            else if (busy.Contains(id))
                ModelState.AddModelError(nameof(m.OrderIds), $"Đơn {o.OrderCode} đã thuộc một chuyến khác đang hoạt động.");
        }
        var exclusive = orders.FirstOrDefault(o => o.Service != null && o.Service.IsExclusive);
        if (exclusive != null && orders.Count > 1)
            ModelState.AddModelError(nameof(m.OrderIds),
                $"Đơn {exclusive.OrderCode} thuộc dịch vụ \"{exclusive.Service!.Name}\" (xe dành riêng), nên chuyến chỉ được chở một mình đơn này.");

        var total = orders.Sum(o => o.WeightKg ?? 0);
        if (vehicle?.CapacityKg is decimal cap && cap > 0 && total > cap)
            ModelState.AddModelError(nameof(m.OrderIds),
                $"Tổng khối lượng {total:n1} kg vượt tải trọng xe ({cap:n1} kg). Hãy chọn xe khác hoặc bớt đơn.");
    }

    // Lưu chuyến + đồng bộ danh sách đơn + ghi xe/tài xế vào từng đơn
    private void Save(Trip trip, TripFormViewModel m)
    {
        trip.Route = m.Route.Trim();
        trip.VehicleId = m.VehicleId!.Value;
        trip.DriverId = m.DriverId!.Value;
        trip.DepartAt = m.DepartAt;
        trip.Note = string.IsNullOrWhiteSpace(m.Note) ? null : m.Note.Trim();

        var wanted = m.OrderIds.Distinct().ToList();

        // Đơn bị bỏ khỏi chuyến
        foreach (var link in trip.Orders.Where(l => !wanted.Contains(l.OrderId)).ToList())
        {
            var o = db.Orders.Include(x => x.Events).First(x => x.Id == link.OrderId);
            o.VehicleId = null; o.DriverId = null;
            o.AddEvent("Đã gỡ khỏi chuyến vận chuyển");
            db.TripOrders.Remove(link);
        }

        // Đơn mới thêm vào chuyến
        var existing = trip.Orders.Select(l => l.OrderId).ToHashSet();
        foreach (var oid in wanted.Where(x => !existing.Contains(x)))
            trip.Orders.Add(new TripOrder { OrderId = oid });

        db.SaveChanges();

        // Ghi xe và tài xế của chuyến sang các đơn trong chuyến
        foreach (var o in db.Orders.Include(x => x.Events).Where(x => wanted.Contains(x.Id)))
        {
            var changed = o.VehicleId != trip.VehicleId || o.DriverId != trip.DriverId;
            o.VehicleId = trip.VehicleId;
            o.DriverId = trip.DriverId;
            if (changed) o.AddEvent("Đã phân công xe và tài xế");
        }
        db.SaveChanges();
    }
}