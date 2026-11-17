using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebVanChuyenLogicstic.Data;
using WebVanChuyenLogicstic.Models;
using WebVanChuyenLogicstic.Services;

namespace WebVanChuyenLogicstic.Controllers;

[Route("bao-gia/{token}")]
public class QuoteResponseController(AppDbContext db, IOrderNotifier notifier) : Controller
{
    [HttpGet("")]
    public IActionResult Index(string token)
    {
        var order = Find(token, tracking: false);
        if (order is null) Response.StatusCode = 404;
        return View(order);
    }

    [HttpPost("dong-y")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Accept(string token)
    {
        var order = Find(token, tracking: true);
        if (order is null) return NotFound();

        if (!order.TryAcceptQuote("Qua email"))
        {
            TempData["Msg"] = "Báo giá này không còn hiệu lực.";
            return RedirectToAction(nameof(Index), new { token });
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
        return RedirectToAction(nameof(Index), new { token });
    }

    [HttpPost("tu-choi")]
    [ValidateAntiForgeryToken]
    public IActionResult Decline(string token)
    {
        var order = Find(token, tracking: true);
        if (order is null) return NotFound();

        if (!order.TryDeclineQuote("Qua email"))
        {
            TempData["Msg"] = "Báo giá này không còn hiệu lực.";
            return RedirectToAction(nameof(Index), new { token });
        }
        db.SaveChanges();

        TempData["Msg"] = "Đã ghi nhận bạn từ chối báo giá. Nếu cần báo giá khác, bạn có thể gửi yêu cầu mới bất cứ lúc nào.";
        return RedirectToAction(nameof(Index), new { token });
    }

    private Order? Find(string token, bool tracking)
    {
        if (string.IsNullOrWhiteSpace(token) || token.Length > 64) return null;
        var q = db.Orders.Include(o => o.Service).Include(o => o.Events).AsQueryable();
        if (!tracking) q = q.AsNoTracking();
        return q.FirstOrDefault(o => o.QuoteToken == token);
    }
}