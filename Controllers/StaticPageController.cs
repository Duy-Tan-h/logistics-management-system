using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebVanChuyenLogicstic.Data;
using WebVanChuyenLogicstic.Filters;
using WebVanChuyenLogicstic.Helpers;
using WebVanChuyenLogicstic.Models;

namespace WebVanChuyenLogicstic.Controllers;

[Route("Admin/StaticPage/{action=Index}")]
[AdminOnly(AdminRoles.Editor)]
public class StaticPageController(AppDbContext db) : Controller
{
    private const string Dir = "~/Views/Admin/StaticPage/";

    public IActionResult Index(int? page) =>
        View(Dir + "Index.cshtml", db.StaticPages.AsNoTracking().OrderBy(p => p.Id).ToPagedList(page));

    [HttpGet]
    public IActionResult Create() => View(Dir + "Create.cshtml", new StaticPage());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(StaticPage model)
    {
        model.Slug = (model.Slug ?? "").Trim();
        CheckSlug(model);
        if (!ModelState.IsValid) return View(Dir + "Create.cshtml", model);

        model.Id = 0;
        model.IsSystem = false;     // trang do admin tạo thì được phép xóa
        model.Title = model.Title.Trim();
        model.Subtitle = string.IsNullOrWhiteSpace(model.Subtitle) ? null : model.Subtitle.Trim();
        db.StaticPages.Add(model);
        db.SaveChanges();
        TempData["SuccessMessage"] = "Đã thêm trang mới.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("{id}")]
    public IActionResult Edit(int id)
    {
        var p = db.StaticPages.AsNoTracking().FirstOrDefault(x => x.Id == id);
        return p is null ? NotFound() : View(Dir + "Edit.cshtml", p);
    }

    [HttpPost("{id}")]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, StaticPage model)
    {
        var page = db.StaticPages.FirstOrDefault(x => x.Id == id);
        if (page is null) return NotFound();

        model.Id = id;
        model.IsSystem = page.IsSystem;
        if (page.IsSystem)
        {
            model.Slug = page.Slug;              // trang hệ thống không đổi đường dẫn
            ModelState.Remove(nameof(model.Slug));
        }
        model.Slug = (model.Slug ?? "").Trim();
        CheckSlug(model);
        if (!ModelState.IsValid) return View(Dir + "Edit.cshtml", model);

        page.Slug = model.Slug;
        page.Title = model.Title.Trim();
        page.Subtitle = string.IsNullOrWhiteSpace(model.Subtitle) ? null : model.Subtitle.Trim();
        page.Icon = model.Icon.Trim();
        page.Content = model.Content;
        page.IsPublished = model.IsPublished;
        db.SaveChanges();

        TempData["SuccessMessage"] = "Đã lưu thay đổi.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("{id}")]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        var p = db.StaticPages.FirstOrDefault(x => x.Id == id);
        if (p is null) return RedirectToAction(nameof(Index));

        if (p.IsSystem)
        {
            TempData["ErrorMessage"] = "Đây là trang hệ thống (gắn với menu/footer) nên không xóa được. Bạn có thể tắt \"Hiển thị\" hoặc sửa nội dung.";
            return RedirectToAction(nameof(Index));
        }

        db.StaticPages.Remove(p);
        db.SaveChanges();
        TempData["SuccessMessage"] = "Đã xóa trang.";
        return RedirectToAction(nameof(Index));
    }

    private void CheckSlug(StaticPage m)
    {
        if (!string.IsNullOrEmpty(m.Slug) && db.StaticPages.Any(x => x.Slug == m.Slug && x.Id != m.Id))
            ModelState.AddModelError(nameof(m.Slug), "Đường dẫn này đã được trang khác sử dụng.");
    }
}