using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebVanChuyenLogicstic.Data;
using WebVanChuyenLogicstic.Models;
using WebVanChuyenLogicstic.Services;

namespace WebVanChuyenLogicstic.Controllers;

public class ServiceController(AppDbContext db, IOrderNotifier notifier) : Controller
{
    public IActionResult Index() =>
    View(db.TransportServices.AsNoTracking().Where(s => s.IsVisible)
        .OrderByDescending(s => s.IsFeatured).ThenBy(s => s.FeaturedOrder).ThenBy(s => s.Id)
        .ToList());

    public IActionResult Details(int id)
    {
        var service = db.TransportServices.AsNoTracking().FirstOrDefault(s => s.Id == id && s.IsVisible);
        return service is null ? NotFound() : View(service);
    }

    [HttpGet]
    public IActionResult Quote(int? serviceId)
    {
        LoadServices();
        LoadSavedAddresses();
        LoadCargoTypes();
        var vm = new QuoteRequestViewModel { ServiceId = serviceId };
        vm.Items.Add(new OrderItemInput());

        if (CurrentCustomerId() is int uid)
        {
            var u = db.Users.AsNoTracking().FirstOrDefault(x => x.Id == uid);
            if (u != null) { vm.CustomerName = u.FullName; vm.Phone = u.Phone; vm.Email = u.Email; }
        }
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Quote(QuoteRequestViewModel model)
    {
        if (model.ServiceId is null)
            ModelState.AddModelError(nameof(model.ServiceId), "Vui lòng chọn dịch vụ.");
        else if (!db.TransportServices.Any(s => s.Id == model.ServiceId && s.IsVisible))
            ModelState.AddModelError(nameof(model.ServiceId), "Dịch vụ không hợp lệ.");
        var types = OrderItemInput.Validate(model.Items, db, ModelState);

        if (!ModelState.IsValid)
        {
            LoadServices();
            LoadSavedAddresses();
            LoadCargoTypes();
            if (model.Items.Count == 0) model.Items.Add(new OrderItemInput());
            return View(model);
        }
        var customerId = CurrentCustomerId();
        var email = string.IsNullOrWhiteSpace(model.Email) ? null : model.Email.Trim();
        if (email is null && customerId is int cid)
            email = db.Users.AsNoTracking().Where(x => x.Id == cid).Select(x => x.Email).FirstOrDefault();

        var order = new Order
        {
            CustomerId = customerId,
            OrderCode = GenerateOrderCode(),
            CustomerName = model.CustomerName.Trim(),
            ReceiverName = model.ReceiverName.Trim(),
            ReceiverPhone = model.ReceiverPhone.Trim(),
            Phone = model.Phone.Trim(),
            Email = email,
            ServiceId = model.ServiceId,
            PickupAddress = model.PickupAddress.Trim(),
            DeliveryAddress = model.DeliveryAddress.Trim(),            
            ExpectedShipDate = model.ExpectedShipDate,
            Note = model.Note,
            Status = OrderStatuses.QuoteRequested,
            CreatedAt = DateTime.Now
        };
        order.Events.Add(new OrderEvent
        {
            SequenceNumber = 1,
            Title = "Đã tiếp nhận yêu cầu báo giá",
            Detail = "Chuyên viên sẽ liên hệ trong thời gian sớm nhất.",
            EventAt = DateTime.Now,
            IsCompleted = true
        });
        foreach (var it in OrderItemInput.ToItems(model.Items, types)) order.Items.Add(it);
        order.RefreshCargoSummary();
        db.Orders.Add(order);
        db.SaveChanges();   // đơn được lưu trước; mail lỗi cũng không mất đơn

        if (!string.IsNullOrEmpty(order.Email))
        {
            var url = Url.Action("Index", "Tracking", new { code = order.OrderCode }, Request.Scheme)!;
            if (await notifier.SendReceivedAsync(order, url))
            {
                order.Events.Add(new OrderEvent
                {
                    SequenceNumber = 2,
                    Title = "Đã gửi email xác nhận",         
                    EventAt = DateTime.Now,
                    IsCompleted = true
                });
                db.SaveChanges();
            }
        }

        TempData["QuoteCode"] = order.OrderCode;
        return RedirectToAction(nameof(Quote));
    }
    private int? CurrentCustomerId()
    {
        if (HttpContext.Session.GetString("UserRole") != "Customer") return null;
        var id = HttpContext.Session.GetInt32("UserId");
        return id is int v && db.Users.Any(u => u.Id == v) ? v : null;
    }

    private void LoadServices() =>
     ViewBag.Services = db.TransportServices.AsNoTracking().Where(s => s.IsVisible).OrderBy(s => s.Name).ToList();

    private string GenerateOrderCode()
    {
        string code;
        do
        {
            code = $"LMS{DateTime.Now:yyyyMMdd}-{Random.Shared.Next(1000, 10000)}";
        } while (db.Orders.Any(o => o.OrderCode == code));
        return code;
    }
    private void LoadSavedAddresses()
    {
        ViewBag.SavedAddresses = CurrentCustomerId() is int uid
            ? db.SavedAddresses.AsNoTracking().Where(a => a.UserId == uid).OrderBy(a => a.Label).ToList()
            : new List<SavedAddress>();
    }
    private void LoadCargoTypes() =>
    ViewBag.CargoTypes = db.CargoTypes.AsNoTracking().Where(c => c.IsActive).OrderBy(c => c.Name).ToList();
}