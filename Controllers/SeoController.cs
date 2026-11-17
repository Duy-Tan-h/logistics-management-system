using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebVanChuyenLogicstic.Data;
using WebVanChuyenLogicstic.Filters;
using WebVanChuyenLogicstic.Helpers;
using WebVanChuyenLogicstic.Models;

namespace WebVanChuyenLogicstic.Controllers;

[Route("Admin/Seo/{action=Index}")]
[AdminOnly(AdminRoles.Editor)]
public class SeoController(AppDbContext db) : Controller
{
    public IActionResult Index(int? page) =>
        View("~/Views/Admin/Seo/Index.cshtml", db.SeoPages.AsNoTracking().OrderBy(p => p.Id).ToPagedList(page));

    [HttpGet]
    public IActionResult Create() => View("~/Views/Admin/Seo/Create.cshtml", new SeoPage());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(SeoPage model)
    {
        model.Route = (model.Route ?? "").Trim().Trim('/');
        model.PageName = (model.PageName ?? "").Trim();

        if (model.Route.Length == 0 || model.Route.Length > 100)
            ModelState.AddModelError(nameof(model.Route), "Nhập route dạng Controller/Action (tối đa 100 ký tự).");
        if (model.PageName.Length == 0 || model.PageName.Length > 100)
            ModelState.AddModelError(nameof(model.PageName), "Vui lòng nhập tên trang (tối đa 100 ký tự).");
        if (db.SeoPages.Any(x => x.Route == model.Route))
            ModelState.AddModelError(nameof(model.Route), "Route này đã có cấu hình, hãy sửa cấu hình hiện có.");

        if (!ModelState.IsValid) return View("~/Views/Admin/Seo/Create.cshtml", model);

        model.Id = 0;
        model.MetaTitle = Clean(model.MetaTitle);
        model.MetaDescription = Clean(model.MetaDescription);
        model.Keywords = Clean(model.Keywords);
        db.SeoPages.Add(model);
        db.SaveChanges();
        TempData["SuccessMessage"] = "Đã thêm cấu hình SEO.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("{id}")]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        var p = db.SeoPages.FirstOrDefault(x => x.Id == id);
        if (p != null)
        {
            db.SeoPages.Remove(p);
            db.SaveChanges();
            TempData["SuccessMessage"] = "Đã xóa cấu hình SEO. Trang sẽ dùng tiêu đề mặc định.";
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("{id}")]
    public IActionResult Edit(int id)
    {
        var p = db.SeoPages.AsNoTracking().FirstOrDefault(x => x.Id == id);
        return p is null ? NotFound() : View("~/Views/Admin/Seo/Edit.cshtml", p);
    }

    [HttpPost("{id}")]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, SeoPage model)
    {
        var p = db.SeoPages.FirstOrDefault(x => x.Id == id);
        if (p is null) return NotFound();

        if (!ModelState.IsValid)
        {
            model.Id = id; model.Route = p.Route; model.PageName = p.PageName;   // hai trường chỉ đọc
            return View("~/Views/Admin/Seo/Edit.cshtml", model);
        }

        p.MetaTitle = Clean(model.MetaTitle);
        p.MetaDescription = Clean(model.MetaDescription);
        p.Keywords = Clean(model.Keywords);
        db.SaveChanges();

        TempData["SuccessMessage"] = $"Đã lưu SEO cho trang \"{p.PageName}\".";
        return RedirectToAction(nameof(Index));
    }

    private static string? Clean(string? v) => string.IsNullOrWhiteSpace(v) ? null : v.Trim();
}