using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebVanChuyenLogicstic.Data;
using WebVanChuyenLogicstic.Filters;
using WebVanChuyenLogicstic.Helpers;
using WebVanChuyenLogicstic.Models;
using WebVanChuyenLogicstic.Services;

namespace WebVanChuyenLogicstic.Controllers;

[Route("Admin/Media/{action=Index}")]
[AdminOnly(AdminRoles.Editor)]
public class MediaController(AppDbContext db, IWebHostEnvironment env) : Controller
{
    private const string Dir = "~/Views/Admin/Media/";

    public IActionResult Index(string? type, int? page)
    {
        type = type != null && MediaTypes.All.Contains(type) ? type : MediaTypes.Partner;
        ViewBag.Type = type;
        var items = db.MediaItems.AsNoTracking().Where(m => m.Type == type)
            .OrderBy(m => m.DisplayOrder).ThenBy(m => m.Id).ToPagedList(page);
        return View(Dir + "Index.cshtml", items);
    }

    [HttpGet]
    public IActionResult Create(string? type) =>
        View(Dir + "Create.cshtml", new MediaItem { Type = type != null && MediaTypes.All.Contains(type) ? type : MediaTypes.Partner });

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(MediaItem model, IFormFile? image)
    {
        if (!MediaTypes.All.Contains(model.Type)) ModelState.AddModelError(nameof(model.Type), "Loại không hợp lệ.");
        var err = ImageUploader.Validate(image);
        if (err != null) ModelState.AddModelError(string.Empty, err);
        else if (image is null || image.Length == 0) ModelState.AddModelError(string.Empty, "Vui lòng chọn ảnh.");
        if (!ModelState.IsValid) return View(Dir + "Create.cshtml", model);

        model.Id = 0;
        model.Title = model.Title.Trim();
        model.LinkUrl = string.IsNullOrWhiteSpace(model.LinkUrl) ? null : model.LinkUrl.Trim();
        model.ImageUrl = ImageUploader.Save(image, env, "media");
        model.CreatedAt = DateTime.Now;
        db.MediaItems.Add(model);
        db.SaveChanges();

        TempData["SuccessMessage"] = "Đã thêm hình ảnh.";
        return RedirectToAction(nameof(Index), new { type = model.Type });
    }

    [HttpGet("{id}")]
    public IActionResult Edit(int id)
    {
        var m = db.MediaItems.AsNoTracking().FirstOrDefault(x => x.Id == id);
        return m is null ? NotFound() : View(Dir + "Edit.cshtml", m);
    }

    [HttpPost("{id}")]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, MediaItem model, IFormFile? image)
    {
        var item = db.MediaItems.FirstOrDefault(x => x.Id == id);
        if (item is null) return NotFound();

        model.Id = id;
        model.ImageUrl = item.ImageUrl;      // form không gửi lại, lấy từ DB để hiện preview
        model.CreatedAt = item.CreatedAt;

        if (!MediaTypes.All.Contains(model.Type)) ModelState.AddModelError(nameof(model.Type), "Loại không hợp lệ.");
        var err = ImageUploader.Validate(image);
        if (err != null) ModelState.AddModelError(string.Empty, err);
        if (!ModelState.IsValid) return View(Dir + "Edit.cshtml", model);

        if (image is { Length: > 0 })
        {
            ImageUploader.Delete(item.ImageUrl, env);
            item.ImageUrl = ImageUploader.Save(image, env, "media");
        }
        item.Type = model.Type;
        item.Title = model.Title.Trim();
        item.LinkUrl = string.IsNullOrWhiteSpace(model.LinkUrl) ? null : model.LinkUrl.Trim();
        item.DisplayOrder = model.DisplayOrder;
        item.IsVisible = model.IsVisible;
        db.SaveChanges();

        TempData["SuccessMessage"] = "Đã lưu thay đổi.";
        return RedirectToAction(nameof(Index), new { type = item.Type });
    }

    [HttpPost("{id}")]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        var item = db.MediaItems.FirstOrDefault(x => x.Id == id);
        var t = item?.Type ?? MediaTypes.Partner;
        if (item != null)
        {
            ImageUploader.Delete(item.ImageUrl, env);
            db.MediaItems.Remove(item);
            db.SaveChanges();
            TempData["SuccessMessage"] = "Đã xóa hình ảnh.";
        }
        return RedirectToAction(nameof(Index), new { type = t });
    }
    private static readonly string[] LibraryExt = [".jpg", ".jpeg", ".png", ".webp", ".gif"];

    // Kho ảnh: gom ảnh có sẵn trong wwwroot/images và ảnh đã upload trong wwwroot/uploads
    [HttpGet]
    public IActionResult Library(int? page)
    {
        var root = env.WebRootPath;
        var files = new List<string>();
        foreach (var folder in new[] { "images", "uploads" })
        {
            var dir = Path.Combine(root, folder);
            if (!Directory.Exists(dir)) continue;
            files.AddRange(Directory.EnumerateFiles(dir, "*.*", SearchOption.AllDirectories)
                .Where(f => LibraryExt.Contains(Path.GetExtension(f).ToLowerInvariant()))
                .Select(f => "/" + Path.GetRelativePath(root, f).Replace('\\', '/')));
        }
        return View(Dir + "Library.cshtml", files.OrderBy(f => f).ToPagedList(page, 24));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Library(List<IFormFile> images)
    {
        var ok = 0;
        string? lastError = null;
        foreach (var f in images)
        {
            var err = ImageUploader.Validate(f);
            if (err != null) { lastError = $"{f.FileName}: {err}"; continue; }
            if (ImageUploader.Save(f, env, "library") != null) ok++;
        }
        if (ok > 0) TempData["SuccessMessage"] = $"Đã tải lên {ok} ảnh.";
        if (lastError != null) TempData["ErrorMessage"] = lastError;
        return RedirectToAction(nameof(Library));
    }

    // Chỉ xóa được ảnh đã upload qua thư viện, không đụng ảnh có sẵn của website
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteFile(string path)
    {
        if (path.StartsWith("/uploads/library/") && !path.Contains(".."))
        {
            ImageUploader.Delete(path, env);
            TempData["SuccessMessage"] = "Đã xóa ảnh.";
        }
        return RedirectToAction(nameof(Library));
    }
}