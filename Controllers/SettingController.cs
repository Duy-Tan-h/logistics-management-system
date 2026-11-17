using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebVanChuyenLogicstic.Data;
using WebVanChuyenLogicstic.Filters;
using WebVanChuyenLogicstic.Models;
using WebVanChuyenLogicstic.Services;

namespace WebVanChuyenLogicstic.Controllers;

[Route("Admin/Setting/{action=Index}")]
[AdminOnly]
public class SettingController(AppDbContext db, IWebHostEnvironment env) : Controller
{
    private const string ViewPath = "~/Views/Admin/Setting/Index.cshtml";

    [HttpGet]
    public IActionResult Index() =>
        View(ViewPath, db.SiteSettings.AsNoTracking().FirstOrDefault() ?? new SiteSetting());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index(SiteSetting model, IFormFile? logo, IFormFile? favicon, bool removeLogo, bool removeFavicon)
    {
        var s = db.SiteSettings.FirstOrDefault() ?? new SiteSetting();

        var err = ImageUploader.Validate(logo) ?? ImageUploader.Validate(favicon);
        if (err != null) ModelState.AddModelError(string.Empty, err);

        if (!ModelState.IsValid)
        {
            model.LogoUrl = s.LogoUrl;          // form không gửi lại đường dẫn ảnh, lấy lại từ DB để hiện preview
            model.FaviconUrl = s.FaviconUrl;
            return View(ViewPath, model);
        }

        if (s.Id == 0) db.SiteSettings.Add(s);

        s.SiteName = model.SiteName.Trim();
        s.Hotline = model.Hotline.Trim();
        s.Email = model.Email.Trim();
        s.Address = model.Address.Trim();
        s.WorkingHours = Clean(model.WorkingHours);
        s.FooterIntro = Clean(model.FooterIntro);
        s.Copyright = Clean(model.Copyright);
        s.FacebookUrl = Clean(model.FacebookUrl);
        s.YoutubeUrl = Clean(model.YoutubeUrl);
        s.LinkedInUrl = Clean(model.LinkedInUrl);
        s.TikTokUrl = Clean(model.TikTokUrl);
        s.ZaloUrl = Clean(model.ZaloUrl);

        if (removeLogo) { ImageUploader.Delete(s.LogoUrl, env); s.LogoUrl = null; }
        if (logo is { Length: > 0 }) { ImageUploader.Delete(s.LogoUrl, env); s.LogoUrl = ImageUploader.Save(logo, env, "site"); }

        if (removeFavicon) { ImageUploader.Delete(s.FaviconUrl, env); s.FaviconUrl = null; }
        if (favicon is { Length: > 0 }) { ImageUploader.Delete(s.FaviconUrl, env); s.FaviconUrl = ImageUploader.Save(favicon, env, "site"); }

        db.SaveChanges();
        TempData["SuccessMessage"] = "Đã lưu cấu hình website.";
        return RedirectToAction(nameof(Index));
    }

    private static string? Clean(string? v) => string.IsNullOrWhiteSpace(v) ? null : v.Trim();
}