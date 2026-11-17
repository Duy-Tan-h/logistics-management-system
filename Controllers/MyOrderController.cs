using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebVanChuyenLogicstic.Data;
using WebVanChuyenLogicstic.Filters;
using WebVanChuyenLogicstic.Models;
using WebVanChuyenLogicstic.Services;

namespace WebVanChuyenLogicstic.Controllers;

[LoginRequired]
[Route("don-cua-toi")]
public class MyOrderController(AppDbContext db, IOrderNotifier notifier) : Controller
{
    private int UserId => HttpContext.Session.GetInt32("UserId")!.Value;

    [HttpGet("")]
    public IActionResult Index()
    {
        var orders = db.Orders.AsNoTracking()
            .Include(o => o.Service)
            .Where(o => o.CustomerId == UserId)
            .OrderByDescending(o => o.CreatedAt)
            .ToList();

        var sorted = orders.OrderByDescending(o => o.IsQuoteOpen).ThenByDescending(o => o.CreatedAt).ToList();
        return View(sorted);
    }

    [HttpGet("{code}")]
    public IActionResult Details(string code)
    {
        var order = Find(code, tracking: false);
        return order is null ? NotFound() : View(order);
    }

    [HttpPost("{code}/dong-y")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Accept(string code)
    {
        var order = Find(code, tracking: true);
        if (order is null) return NotFound();

        if (!order.TryAcceptQuote("Trong tài khoản"))
        {
            TempData["Msg"] = "Báo giá này không còn hiệu lực.";
            return RedirectToAction(nameof(Details), new { code });
        }
        db.SaveChanges();

        if (!string.IsNullOrWhiteSpace(order.Email))
        {
            var url = Url.Action("Index", "Tracking", new { code = order.OrderCode }, Request.Scheme)!;
            if (await notifier.SendStatusAsync(order, null, url))
            {
                order.AddEvent("Đã gửi email thông báo");
                db.SaveChanges();
            }
        }

        TempData["Msg"] = "Cảm ơn bạn! Đơn hàng đã được xác nhận, chúng tôi sẽ sắp xếp xe và liên hệ với bạn.";
        return RedirectToAction(nameof(Details), new { code });
    }

    [HttpPost("{code}/tu-choi")]
    [ValidateAntiForgeryToken]
    public IActionResult Decline(string code)
    {
        var order = Find(code, tracking: true);
        if (order is null) return NotFound();

        if (!order.TryDeclineQuote("Trong tài khoản"))
        {
            TempData["Msg"] = "Báo giá này không còn hiệu lực.";
            return RedirectToAction(nameof(Details), new { code });
        }
        db.SaveChanges();

        TempData["Msg"] = "Đã ghi nhận bạn từ chối báo giá. Nếu cần báo giá khác, bạn có thể gửi yêu cầu mới bất cứ lúc nào.";
        return RedirectToAction(nameof(Details), new { code });
    }
    [HttpPost("{code}/huy")]
    [ValidateAntiForgeryToken]
    public IActionResult Cancel(string code)
    {
        var order = Find(code, tracking: true);
        if (order is null) return NotFound();

        if (!order.TryCancelRequest("Trong tài khoản"))
        {
            TempData["Msg"] = "Đơn này đã được xử lý nên không thể tự hủy. Vui lòng liên hệ nhân viên.";
            return RedirectToAction(nameof(Details), new { code });
        }
        var links = db.TripOrders.Where(l => l.OrderId == order.Id && l.Trip!.Status == TripStatuses.Planned).ToList();
        if (links.Count > 0) { db.TripOrders.RemoveRange(links); order.VehicleId = null; order.DriverId = null; }
        db.SaveChanges();

        TempData["Msg"] = "Đã hủy yêu cầu vận chuyển.";
        return RedirectToAction(nameof(Details), new { code });
    }

    // Chỉ tìm trong đơn của chính người đăng nhập
    private Order? Find(string code, bool tracking)
    {
        if (string.IsNullOrWhiteSpace(code) || code.Length > 50) return null;
        var q = db.Orders.Include(o => o.Service).Include(o => o.Events).Include(o => o.Items).AsQueryable();
        if (!tracking) q = q.AsNoTracking();
        return q.FirstOrDefault(o => o.OrderCode == code && o.CustomerId == UserId);
    }
}