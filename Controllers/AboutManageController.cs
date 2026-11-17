using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebVanChuyenLogicstic.Data;
using WebVanChuyenLogicstic.Filters;
using WebVanChuyenLogicstic.Models;

namespace WebVanChuyenLogicstic.Controllers;

[Route("Admin/About/{action=Index}")]
[AdminOnly(AdminRoles.Editor)]
public class AboutManageController(AppDbContext db) : Controller
{
    private const string Dir = "~/Views/Admin/About/";

    public IActionResult Index() =>
        View(Dir + "Index.cshtml", db.AboutItems.AsNoTracking().OrderBy(a => a.DisplayOrder).ThenBy(a => a.Id).ToList());

    [HttpGet]
    public IActionResult Create(string? kind)
    {
        if (kind is null || !AboutKinds.Multi.Contains(kind)) return RedirectToAction(nameof(Index));
        var next = (db.AboutItems.Where(a => a.Kind == kind).Max(a => (int?)a.DisplayOrder) ?? 0) + 1;
        return View(Dir + "Edit.cshtml", new AboutItem { Kind = kind, DisplayOrder = next });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(AboutItem model)
    {
        if (!AboutKinds.Multi.Contains(model.Kind)) return BadRequest();
        Normalize(model);
        Validate(model);
        if (!ModelState.IsValid) return View(Dir + "Edit.cshtml", model);

        model.Id = 0;
        db.AboutItems.Add(model);
        db.SaveChanges();
        TempData["SuccessMessage"] = "Đã thêm mục mới.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("{id}")]
    public IActionResult Edit(int id)
    {
        var item = db.AboutItems.AsNoTracking().FirstOrDefault(a => a.Id == id);
        return item is null ? NotFound() : View(Dir + "Edit.cshtml", item);
    }

    [HttpPost("{id}")]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, AboutItem model)
    {
        var item = db.AboutItems.FirstOrDefault(a => a.Id == id);
        if (item is null) return NotFound();

        model.Id = id;
        model.Kind = item.Kind;       // không cho đổi loại
        Normalize(model);
        Validate(model);
        if (!ModelState.IsValid) return View(Dir + "Edit.cshtml", model);

        item.Value = model.Value;
        item.Title = model.Title;
        item.Content = model.Content;
        item.Icon = model.Icon;
        item.DisplayOrder = model.DisplayOrder;
        item.IsVisible = model.IsVisible;
        db.SaveChanges();

        TempData["SuccessMessage"] = "Đã lưu thay đổi.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("{id}")]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        var item = db.AboutItems.FirstOrDefault(a => a.Id == id);
        if (item != null && AboutKinds.Multi.Contains(item.Kind))   // Giới thiệu/Tầm nhìn/Sứ mệnh chỉ sửa, không xóa
        {
            db.AboutItems.Remove(item);
            db.SaveChanges();
            TempData["SuccessMessage"] = "Đã xóa mục.";
        }
        return RedirectToAction(nameof(Index));
    }

    private static void Normalize(AboutItem m)// kiểm tra dữ liệu trc khi lưu
    {
        m.Title = (m.Title ?? "").Trim();
        m.Value = string.IsNullOrWhiteSpace(m.Value) ? null : m.Value.Trim();
        m.Content = string.IsNullOrWhiteSpace(m.Content) ? null : m.Content.Trim();
        m.Icon = string.IsNullOrWhiteSpace(m.Icon) ? null : m.Icon.Trim();
    }

    private void Validate(AboutItem m) //kiểm tra dữ liệu trước khi lưu
    {
        if (m.Kind != AboutKinds.Stat && string.IsNullOrWhiteSpace(m.Content))
            ModelState.AddModelError(nameof(m.Content), "Vui lòng nhập nội dung.");
        if ((m.Kind == AboutKinds.Stat || m.Kind == AboutKinds.Milestone) && string.IsNullOrWhiteSpace(m.Value))
            ModelState.AddModelError(nameof(m.Value), "Vui lòng nhập giá trị.");
    }
}