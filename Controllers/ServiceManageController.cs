using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebVanChuyenLogicstic.Data;
using WebVanChuyenLogicstic.Filters;
using WebVanChuyenLogicstic.Helpers;
using WebVanChuyenLogicstic.Models;

namespace WebVanChuyenLogicstic.Controllers;

[Route("Admin/Service/{action=Index}")]
[AdminOnly]
public class ServiceManageController(AppDbContext db) : Controller
{
    private const string Dir = "~/Views/Admin/Service/";

    public IActionResult Index(int? page) //danh sách kèm số đơn của từng dịch vụ
    {
        var services = db.TransportServices.AsNoTracking().OrderBy(s => s.Id).ToPagedList(page);
        ViewBag.OrderCounts = db.Orders.AsNoTracking()
            .Where(o => o.ServiceId != null)
            .GroupBy(o => o.ServiceId)
            .Select(g => new { Id = g.Key, N = g.Count() })
            .ToList()
            .ToDictionary(x => x.Id!.Value, x => x.N);
        return View(Dir + "Index.cshtml", services);
    }

    [HttpGet]
    public IActionResult Create() => View(Dir + "Create.cshtml", new TransportService());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(TransportService model)
    {
        Normalize(model);
        Validate(model);
        if (!ModelState.IsValid) return View(Dir + "Create.cshtml", model);

        model.Id = 0;
        db.TransportServices.Add(model);
        db.SaveChanges();
        TempData["SuccessMessage"] = "Đã thêm dịch vụ mới.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("{id}")]
    public IActionResult Edit(int id)
    {
        var s = db.TransportServices.AsNoTracking().FirstOrDefault(x => x.Id == id);
        return s is null ? NotFound() : View(Dir + "Edit.cshtml", s);
    }

    [HttpPost("{id}")]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, TransportService model)
    {
        if (id != model.Id) return BadRequest();
        Normalize(model);
        Validate(model);
        if (!ModelState.IsValid) return View(Dir + "Edit.cshtml", model);

        if (!db.TransportServices.Any(x => x.Id == id)) return NotFound();
        db.TransportServices.Update(model);
        db.SaveChanges();
        TempData["SuccessMessage"] = "Đã lưu thay đổi.";
        return RedirectToAction(nameof(Index));
    }

    // Bật/tắt hiển thị nhanh ngay ở danh sách
    [HttpPost("{id}")]
    [ValidateAntiForgeryToken]
    public IActionResult Toggle(int id)
    {
        var s = db.TransportServices.FirstOrDefault(x => x.Id == id);
        if (s != null)
        {
            s.IsVisible = !s.IsVisible;
            db.SaveChanges();
            TempData["SuccessMessage"] = s.IsVisible ? "Đã hiện dịch vụ trên website." : "Đã ẩn dịch vụ khỏi website.";
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("{id}")]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        var s = db.TransportServices.FirstOrDefault(x => x.Id == id);
        if (s is null) return RedirectToAction(nameof(Index));

        // Xóa sẽ làm đơn cũ mất tên dịch vụ, nên chặn và gợi ý ẩn
        var used = db.Orders.Count(o => o.ServiceId == id);
        if (used > 0)
        {
            TempData["ErrorMessage"] = $"Dịch vụ \"{s.Name}\" đã có {used} đơn hàng nên không xóa được. Hãy chọn Ẩn để dịch vụ không còn hiện trên website.";
            return RedirectToAction(nameof(Index));
        }

        db.TransportServices.Remove(s);
        db.SaveChanges();
        TempData["SuccessMessage"] = "Đã xóa dịch vụ.";
        return RedirectToAction(nameof(Index));
    }

    private static void Normalize(TransportService m)
    {
        m.Name = (m.Name ?? "").Trim();
        m.Summary = (m.Summary ?? "").Trim();
        m.Icon = (m.Icon ?? "").Trim();
        m.DeliveryTime = (m.DeliveryTime ?? "").Trim();
        m.PriceNote = (m.PriceNote ?? "").Trim();
        m.Description = string.IsNullOrWhiteSpace(m.Description) ? null : m.Description.Trim();
        m.ImageUrl = string.IsNullOrWhiteSpace(m.ImageUrl) ? null : m.ImageUrl.Trim();
    }

    private void Validate(TransportService m)
    {
        if (!string.IsNullOrEmpty(m.Name) &&
            db.TransportServices.Any(x => x.Name == m.Name && x.Id != m.Id))
            ModelState.AddModelError(nameof(m.Name), "Tên dịch vụ này đã tồn tại.");
    }
}