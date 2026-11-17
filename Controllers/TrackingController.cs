using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebVanChuyenLogicstic.Data;

namespace WebVanChuyenLogicstic.Controllers;

public class TrackingController(AppDbContext db) : Controller
{
    [HttpGet]
    public IActionResult Index(string? code)
    {
        if (string.IsNullOrWhiteSpace(code)) return View();

        code = code.Trim().ToUpperInvariant();
        ViewBag.TrackingCode = code;
        ViewBag.HasResult = true;

        var order = db.Orders.AsNoTracking()
            .Include(o => o.Service).Include(o => o.Events)
            .FirstOrDefault(o => o.OrderCode == code);

        return View(order);   // null => view hiện "không tìm thấy"
    }

    // Vẫn là URL /Tracking (POST), chỉ đổi tên method C# để không trùng với Index ở trên.
    // Form ở trang chủ và trang Tracking không cần sửa gì.
    [HttpPost]
    [ActionName("Index")]
    [ValidateAntiForgeryToken]
    public IActionResult Search(string? trackingCode)
        => RedirectToAction(nameof(Index), new { code = trackingCode?.Trim() });
}