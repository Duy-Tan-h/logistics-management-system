using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebVanChuyenLogicstic.Data;
using WebVanChuyenLogicstic.Filters;
using WebVanChuyenLogicstic.Models;
using WebVanChuyenLogicstic.Services;
using WebVanChuyenLogicstic.Helpers;

namespace WebVanChuyenLogicstic.Controllers;

[Route("Admin/Order/{action=Index}")]
[AdminOnly(AdminRoles.Dispatcher)]
public class OrderController(AppDbContext db, IOrderNotifier notifier) : Controller
{
    public IActionResult Index(string? status, string? q, int? page) //danh sách lọc tìm
    {
        var query = db.Orders.AsNoTracking()
       .Include(o => o.Service).Include(o => o.Vehicle).Include(o => o.Driver)
       .AsQueryable();

        if (!string.IsNullOrWhiteSpace(status) && OrderStatuses.All.Contains(status))
            query = query.Where(o => o.Status == status);
        if (!string.IsNullOrWhiteSpace(q))
        {
            var s = q.Trim();
            query = query.Where(o => o.OrderCode.Contains(s) || o.CustomerName.Contains(s) || o.Phone.Contains(s));
        }
        ViewBag.Q = q;    
        ViewBag.CurrentStatus = status;
        ViewBag.Counts = db.Orders.GroupBy(o => o.Status).ToDictionary(g => g.Key, g => g.Count());
        return View("~/Views/Admin/Order/Index.cshtml", query.OrderByDescending(o => o.CreatedAt).ToPagedList(page));
    }

    [HttpGet("{id}")]
    public IActionResult Details(int id) //Gọi LoadOrder(id, tracking: false); không có thì NotFound(), có thì hiện trang chi tiết.
    {
        var order = LoadOrder(id, tracking: false);
        if (order is null) return NotFound();
        //FillAssignLists();
        return View("~/Views/Admin/Order/Details.cshtml", order);
    }

    // ---------- BÁO GIÁ ----------

    [HttpPost("{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SendQuote(int id, decimal? price, DateTime? validUntil, string? note)
    {
        var order = LoadOrder(id, tracking: true);
        if (order is null) return NotFound();

        if (order.Status != OrderStatuses.QuoteRequested && order.Status != OrderStatuses.Quoted)
            return Fail(id, "Chỉ gửi được báo giá khi đơn đang ở bước báo giá.");
        if (price is null || price <= 0)
            return Fail(id, "Vui lòng nhập giá hợp lệ.");
        if (validUntil is null || validUntil.Value.Date < DateTime.Today)
            return Fail(id, "Hạn hiệu lực phải từ hôm nay trở đi.");

        var isRevision = order.Status == OrderStatuses.Quoted;
        var cleanNote = string.IsNullOrWhiteSpace(note) ? null : note.Trim();
        if (cleanNote is { Length: > 1000 }) cleanNote = cleanNote[..1000];

        order.QuotedPrice = price;
        order.QuoteValidUntil = validUntil.Value.Date.AddDays(1).AddSeconds(-1);   // hết ngày được chọn
        order.QuoteNote = cleanNote;
        order.QuoteToken = Convert.ToHexString(RandomNumberGenerator.GetBytes(16)).ToLowerInvariant();  // link cũ mất hiệu lực
        order.Status = OrderStatuses.Quoted;
        order.AddEvent(isRevision ? "Đã cập nhật báo giá" : "Đã gửi báo giá",
            $"Hiệu lực đến hết ngày {validUntil.Value:dd/MM/yyyy}");   // KHÔNG ghi giá: trang tra cứu công khai hiện mục này
        db.SaveChanges();

        if (string.IsNullOrWhiteSpace(order.Email))
        {
            TempData["SuccessMessage"] = "Đã lưu báo giá.";
            TempData["ErrorMessage"] = $"Khách không để lại email. Hãy gọi {order.Phone} để báo giá.";
            return RedirectToAction(nameof(Details), new { id });
        }

        var respondUrl = Url.Action("Index", "QuoteResponse", new { token = order.QuoteToken }, Request.Scheme)!;
        if (await notifier.SendQuoteAsync(order, respondUrl))
        {
            order.AddEvent("Đã gửi email báo giá");
            db.SaveChanges();
            TempData["SuccessMessage"] = "Đã lưu báo giá và gửi email cho khách.";
        }
        else
        {
            TempData["SuccessMessage"] = "Đã lưu báo giá.";
            TempData["ErrorMessage"] = $"Chưa gửi được email (kiểm tra cấu hình SMTP / xem log). Hãy gọi khách theo số {order.Phone}.";
        }
        return RedirectToAction(nameof(Details), new { id });
    }

    // Khách đồng ý qua điện thoại / trực tiếp, nhân viên ghi nhận
    [HttpPost("{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CustomerAccepted(int id)
    {
        var order = LoadOrder(id, tracking: true);
        if (order is null) return NotFound();
        if (order.Status != OrderStatuses.Quoted)
            return Fail(id, "Đơn không ở trạng thái chờ khách trả lời báo giá.");

        order.Status = OrderStatuses.Confirmed;
        order.AddEvent("Khách đồng ý báo giá", "Nhân viên ghi nhận qua điện thoại / trao đổi trực tiếp");
        db.SaveChanges();
        TempData["SuccessMessage"] = "Đã xác nhận đơn hàng.";

        await NotifyStatusAsync(order, null);
        return RedirectToAction(nameof(Details), new { id });
    }

    [HttpPost("{id}")]
    [ValidateAntiForgeryToken]
    public IActionResult CustomerDeclined(int id)
    {
        var order = LoadOrder(id, tracking: true);
        if (order is null) return NotFound();
        if (order.Status != OrderStatuses.Quoted)
            return Fail(id, "Đơn không ở trạng thái chờ khách trả lời báo giá.");

        order.Status = OrderStatuses.Cancelled;
        order.AddEvent("Khách từ chối báo giá", "Nhân viên ghi nhận");
        db.SaveChanges();
        TempData["SuccessMessage"] = "Đã hủy đơn do khách từ chối báo giá.";
        return RedirectToAction(nameof(Details), new { id });
    }

    // ---------- CÁC BƯỚC SAU ----------

    //[HttpPost("{id}")]
    //[ValidateAntiForgeryToken]
    //public IActionResult Assign(int id, int? vehicleId, int? driverId)
    //{
    //    var order = LoadOrder(id, tracking: true);
    //    if (order is null) return NotFound();
    //    if (OrderStatuses.IsFinal(order.Status))
    //        return Fail(id, "Đơn đã kết thúc, không thể đổi xe/tài xế.");

    //    var vehicle = vehicleId is null ? null : db.Vehicles.Find(vehicleId);
    //    var driver = driverId is null ? null : db.Drivers.Find(driverId);
    //    if ((vehicleId != null && vehicle is null) || (driverId != null && driver is null))
    //        return Fail(id, "Xe hoặc tài xế không tồn tại.");
    //    if (vehicle != null && vehicle.Status != "Đang hoạt động")
    //        return Fail(id, "Xe này đang không hoạt động.");
    //    if (driver != null && driver.Status != "Đang làm việc")
    //        return Fail(id, "Tài xế này đang không làm việc.");

    //    order.VehicleId = vehicle?.Id;
    //    order.DriverId = driver?.Id;

    //    // Không ghi biển số / tên tài xế vào mốc: trang tra cứu công khai hiện mốc này
    //    if (vehicle != null || driver != null)
    //        order.AddEvent("Đã phân công xe và tài xế");

    //    db.SaveChanges();
    //    TempData["SuccessMessage"] = "Đã cập nhật phân công.";
    //    return RedirectToAction(nameof(Details), new { id });
    //}

    [HttpPost("{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateStatus(int id, string status, string? detail)
    {
        var order = LoadOrder(id, tracking: true);
        if (order is null) return NotFound();

        if (status != OrderStatuses.Cancelled)
            return Fail(id, "Tiến độ vận chuyển được cập nhật bằng nút ghi nhận mốc.");

        if (!OrderStatuses.CanCancel(order.Status))
            return Fail(id, order.Status == OrderStatuses.InTransit
                ? "Đơn đang vận chuyển nên không thể hủy. Hãy xử lý sự cố với tài xế và ghi chú vào hành trình."
                : $"Không thể hủy đơn đang ở trạng thái \"{order.Status}\".");

        if (string.IsNullOrWhiteSpace(detail))
            return Fail(id, "Vui lòng nhập lý do hủy đơn.");

        var note = string.IsNullOrWhiteSpace(detail) ? null : detail.Trim();
        order.Status = status;
        ReleaseFromTrips(order);
        order.AddEvent(status, note);
        db.SaveChanges();
        TempData["SuccessMessage"] = $"Đã chuyển trạng thái: {status}.";

        await NotifyStatusAsync(order, note);
        return RedirectToAction(nameof(Details), new { id });
    }

    [HttpPost("{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ReturnOrder(int id, string? detail)
    {
        var order = LoadOrder(id, tracking: true);
        if (order is null) return NotFound();
        if (order.Status != OrderStatuses.InTransit)
            return Fail(id, "Chỉ ghi nhận hoàn hàng cho đơn đang vận chuyển.");
        if (string.IsNullOrWhiteSpace(detail))
            return Fail(id, "Vui lòng nhập lý do hoàn hàng.");

        var note = detail.Trim();
        if (note.Length > 500) note = note[..500];
        order.Status = OrderStatuses.Returned;
        order.AddEvent(OrderStatuses.Returned, note);
        db.SaveChanges();
        TempData["SuccessMessage"] = "Đã ghi nhận hoàn hàng. Đơn kết thúc, chuyến có thể hoàn thành khi các đơn còn lại đã giao.";

        await NotifyStatusAsync(order, note);
        return RedirectToAction(nameof(Details), new { id });
    }

    [HttpPost("{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddMilestone(int id, string milestone, string? detail)
    {
        var order = LoadOrder(id, tracking: true);
        if (order is null) return NotFound();

        if (order.Status == OrderStatuses.Confirmed)
            return Fail(id, "Đơn chỉ bắt đầu vận chuyển khi chuyến chứa đơn này được chuyển sang \"Đang chạy\".");
        if (order.Status != OrderStatuses.InTransit)
            return Fail(id, "Đơn đã giao/hoàn thành hoặc đã kết thúc, không ghi nhận thêm mốc.");

        var next = OrderMilestones.Next(order.CurrentMilestone);
        if (next is null)
            return Fail(id, "Đơn đã hoàn thành, không còn mốc tiếp theo.");
        if (OrderMilestones.IsTripDriven(next))
            return Fail(id, next == OrderMilestones.Completed
                ? "Mốc \"Hoàn thành\" tự cập nhật khi chuyến vận chuyển hoàn thành."
                : "Mốc này tự cập nhật theo chuyến vận chuyển.");
        if (milestone != next)
            return Fail(id, $"Mốc không hợp lệ. Mốc tiếp theo phải là \"{next}\".");

        var newStatus = OrderMilestones.StatusOf(next);

        var note = string.IsNullOrWhiteSpace(detail) ? null : detail.Trim();
        var statusChanged = order.Status != newStatus;
        order.Status = newStatus;
        order.AddEvent(next, note);
        db.SaveChanges();
        TempData["SuccessMessage"] = $"Đã ghi nhận mốc: {next}.";

        if (statusChanged) await NotifyStatusAsync(order, note);
        return RedirectToAction(nameof(Details), new { id });
    }

    [HttpPost("{id}")]
    [ValidateAntiForgeryToken]
    public IActionResult AddNote(int id, string title, string? detail)
    {
        var order = LoadOrder(id, tracking: true);
        if (order is null) return NotFound();
        if (string.IsNullOrWhiteSpace(title))
            return Fail(id, "Vui lòng nhập tiêu đề mốc hành trình.");
        if (OrderMilestones.All.Contains(title.Trim()))
            return Fail(id, "Tên này là mốc chuẩn, hãy dùng nút ghi nhận mốc.");

        order.AddEvent(title.Trim(), string.IsNullOrWhiteSpace(detail) ? null : detail.Trim());
        db.SaveChanges();
        return RedirectToAction(nameof(Details), new { id });
    }

    [HttpPost("{id}")]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        var order = db.Orders.FirstOrDefault(o => o.Id == id);
        if (order != null)
        {
            if (order.Status != OrderStatuses.Cancelled)
            {
                TempData["ErrorMessage"] = "Chỉ xóa được đơn đã hủy. Hãy hủy đơn trước.";
                return RedirectToAction(nameof(Index));
            }
            db.Orders.Remove(order);   // OrderEvents xóa theo (cascade)
            db.SaveChanges();
            TempData["SuccessMessage"] = "Đã xóa đơn hàng.";
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("{id}")]
    [ValidateAntiForgeryToken]
    public IActionResult EditEvent(int id, int eventId, string? detail)
    {
        var ev = db.OrderEvents.FirstOrDefault(e => e.Id == eventId && e.OrderId == id);
        if (ev is null) return NotFound();

        var d = string.IsNullOrWhiteSpace(detail) ? null : detail.Trim();
        ev.Detail = d is { Length: > 500 } ? d[..500] : d;
        db.SaveChanges();
        TempData["SuccessMessage"] = "Đã sửa ghi chú.";
        return RedirectToAction(nameof(Details), new { id });
    }

    private static bool CanEditInfo(string s) =>
    s is OrderStatuses.QuoteRequested or OrderStatuses.Quoted or OrderStatuses.Confirmed;

    [HttpGet("{id}")]
    public IActionResult EditInfo(int id)
    {
        var o = db.Orders.AsNoTracking().Include(x => x.Items).FirstOrDefault(x => x.Id == id);
        if (o is null) return NotFound();
        if (!CanEditInfo(o.Status))
            return Fail(id, "Đơn đã vận chuyển hoặc đã kết thúc nên không sửa được thông tin.");

        FillCargoTypes();
        return View("~/Views/Admin/Order/Edit.cshtml", new Models.Admin.OrderEditViewModel
        {
            Id = o.Id,
            OrderCode = o.OrderCode,
            Status = o.Status,
            CustomerName = o.CustomerName,
            Phone = o.Phone,
            Email = o.Email,
            ReceiverName = o.ReceiverName ?? "",
            ReceiverPhone = o.ReceiverPhone ?? "",
            PickupAddress = o.PickupAddress,
            DeliveryAddress = o.DeliveryAddress,
            ExpectedShipDate = o.ExpectedShipDate,
            Note = o.Note,
            // Đơn cũ chưa có dòng hàng: dựng 1 dòng từ số liệu cũ để admin chọn loại hàng rồi lưu
            Items = o.Items.Count > 0
                ? o.Items.OrderBy(i => i.Id).Select(i => new OrderItemInput
                {
                    CargoTypeId = i.CargoTypeId,
                    Quantity = i.Quantity,
                    WeightKg = i.WeightKg,
                    IsFragile = i.IsFragile,
                    Note = i.Note
                }).ToList()
                : new List<OrderItemInput> { new() { Quantity = o.PackageCount ?? 1, WeightKg = o.WeightKg } }
        });
    }

    [HttpPost("{id}")]
    [ValidateAntiForgeryToken]
    public IActionResult EditInfo(int id, Models.Admin.OrderEditViewModel model)
    {
        var o = db.Orders.Include(x => x.Events).Include(x => x.Items).FirstOrDefault(x => x.Id == id);
        if (o is null) return NotFound();
        if (!CanEditInfo(o.Status))
            return Fail(id, "Đơn đã vận chuyển hoặc đã kết thúc nên không sửa được thông tin.");

        var types = OrderItemInput.Validate(model.Items, db, ModelState);
        if (!ModelState.IsValid)
        {
            model.Id = id; model.OrderCode = o.OrderCode; model.Status = o.Status;
            if (model.Items.Count == 0) model.Items.Add(new OrderItemInput());
            FillCargoTypes();
            return View("~/Views/Admin/Order/Edit.cshtml", model);
        }

        o.CustomerName = model.CustomerName.Trim();
        o.Phone = model.Phone.Trim();
        o.Email = string.IsNullOrWhiteSpace(model.Email) ? null : model.Email.Trim();
        o.ReceiverName = model.ReceiverName.Trim();
        o.ReceiverPhone = model.ReceiverPhone.Trim();
        o.PickupAddress = model.PickupAddress.Trim();
        o.DeliveryAddress = model.DeliveryAddress.Trim();
        o.ExpectedShipDate = model.ExpectedShipDate;
        o.Note = string.IsNullOrWhiteSpace(model.Note) ? null : model.Note.Trim();

        db.OrderItems.RemoveRange(o.Items.ToList());   // thay toàn bộ dòng hàng bằng danh sách mới
        o.Items.Clear();
        foreach (var it in OrderItemInput.ToItems(model.Items, types)) o.Items.Add(it);
        o.RefreshCargoSummary();

        o.AddEvent("Đã cập nhật thông tin đơn");   // không ghi chi tiết: trang tra cứu công khai hiện mốc này
        db.SaveChanges();

        TempData["SuccessMessage"] = "Đã cập nhật thông tin đơn.";
        return RedirectToAction(nameof(Details), new { id });
    }

    // ---------- helper ----------

    private IActionResult Fail(int id, string message)
    {
        TempData["ErrorMessage"] = message;
        return RedirectToAction(nameof(Details), new { id });
    }

    // Gửi mail theo trạng thái hiện tại (chỉ các trạng thái cần báo khách)
    private async Task NotifyStatusAsync(Order order, string? note)
    {
        var shouldMail = order.Status is OrderStatuses.Confirmed or OrderStatuses.InTransit
            or OrderStatuses.Delivered or OrderStatuses.Returned or OrderStatuses.Cancelled;
        if (!shouldMail || string.IsNullOrWhiteSpace(order.Email)) return;

        var url = Url.Action("Index", "Tracking", new { code = order.OrderCode }, Request.Scheme)!;
        if (await notifier.SendStatusAsync(order, note, url))
        {
            order.AddEvent("Đã gửi email thông báo");   // không ghi địa chỉ email
            db.SaveChanges();
        }
        else
        {
            TempData["ErrorMessage"] = "Đã đổi trạng thái nhưng chưa gửi được email cho khách (kiểm tra cấu hình SMTP / xem log).";
        }
    }

    private Order? LoadOrder(int id, bool tracking)
    {
        var q = db.Orders.Include(o => o.Service).Include(o => o.Vehicle)
            .Include(o => o.Driver).Include(o => o.Events).Include(o => o.Items).AsQueryable();
        if (!tracking) q = q.AsNoTracking();
        return q.FirstOrDefault(o => o.Id == id);
    }

    // Đơn bị hủy thì gỡ khỏi chuyến chưa kết thúc
    private void ReleaseFromTrips(Order order)
    {
        var links = db.TripOrders.Where(l => l.OrderId == order.Id
            && (l.Trip!.Status == TripStatuses.Planned || l.Trip.Status == TripStatuses.Running)).ToList();
        if (links.Count == 0) return;
        db.TripOrders.RemoveRange(links);
        order.VehicleId = null;
        order.DriverId = null;
    }
    private void FillCargoTypes() =>
    ViewBag.CargoTypes = db.CargoTypes.AsNoTracking().OrderBy(c => c.Name).ToList();
}