using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebVanChuyenLogicstic.Data;
using WebVanChuyenLogicstic.Filters;
using WebVanChuyenLogicstic.Helpers;
using WebVanChuyenLogicstic.Models;

namespace WebVanChuyenLogicstic.Controllers;

[Route("Admin/CargoType/{action=Index}")]
[AdminOnly]
public class CargoTypeController(AppDbContext db) : Controller
{
    private const string Dir = "~/Views/Admin/CargoType/";

    public IActionResult Index(int? page)
    {
        var list = db.CargoTypes.AsNoTracking().OrderBy(c => c.Name).ToPagedList(page);

        // Đơn lưu loại hàng dạng chữ nên đếm theo tên
        var counts = db.Orders.AsNoTracking()
            .Where(o => o.CargoType != null)
            .GroupBy(o => o.CargoType!)
            .Select(g => new { Name = g.Key, N = g.Count() })
            .ToList();
        ViewBag.OrderCounts = counts.ToDictionary(x => x.Name, x => x.N, StringComparer.OrdinalIgnoreCase);
        return View(Dir + "Index.cshtml", list);
    }

    [HttpGet]
    public IActionResult Create() => View(Dir + "Create.cshtml", new CargoType());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CargoType model)
    {
        Normalize(model);
        Validate(model);
        if (!ModelState.IsValid) return View(Dir + "Create.cshtml", model);

        model.Id = 0;
        db.CargoTypes.Add(model);
        db.SaveChanges();
        TempData["SuccessMessage"] = "Đã thêm loại hàng hóa.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("{id}")]
    public IActionResult Edit(int id)
    {
        var c = db.CargoTypes.AsNoTracking().FirstOrDefault(x => x.Id == id);
        return c is null ? NotFound() : View(Dir + "Edit.cshtml", c);
    }

    [HttpPost("{id}")]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, CargoType model)
    {
        if (id != model.Id) return BadRequest();
        Normalize(model);
        Validate(model);
        if (!ModelState.IsValid) return View(Dir + "Edit.cshtml", model);

        if (!db.CargoTypes.Any(x => x.Id == id)) return NotFound();
        db.CargoTypes.Update(model);
        db.SaveChanges();
        TempData["SuccessMessage"] = "Đã lưu thay đổi.";
        return RedirectToAction(nameof(Index));
    }

    // Đơn cũ lưu loại hàng dạng chữ nên xóa loại hàng không ảnh hưởng đơn nào
    [HttpPost("{id}")]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        if (db.OrderItems.Any(i => i.CargoTypeId == id))
        {
            TempData["ErrorMessage"] = "Loại hàng này đã có trong đơn nên không xóa được. Hãy sửa và bỏ tích \"gợi ý\" để ẩn khỏi form báo giá.";
            return RedirectToAction(nameof(Index));
        }
        var c = db.CargoTypes.FirstOrDefault(x => x.Id == id);
        if (c != null)
        {
            db.CargoTypes.Remove(c);
            db.SaveChanges();
            TempData["SuccessMessage"] = "Đã xóa loại hàng hóa.";
        }
        return RedirectToAction(nameof(Index));
    }

    private static void Normalize(CargoType m)
    {
        m.Name = (m.Name ?? "").Trim();
        m.Description = string.IsNullOrWhiteSpace(m.Description) ? null : m.Description.Trim();
    }

    private void Validate(CargoType m)
    {
        if (!string.IsNullOrEmpty(m.Name) && db.CargoTypes.Any(x => x.Name == m.Name && x.Id != m.Id))
            ModelState.AddModelError(nameof(m.Name), "Loại hàng này đã tồn tại.");
    }
}