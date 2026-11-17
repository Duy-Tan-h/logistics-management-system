using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace WebVanChuyenLogicstic.Services;

public static class ImageUploader
{
    private static readonly string[] Allowed = [".jpg", ".jpeg", ".png", ".webp", ".gif", ".ico"];
    private const long MaxBytes = 3 * 1024 * 1024;   // 3 MB

    // Trả về thông báo lỗi, hoặc null nếu hợp lệ / không có file
    public static string? Validate(IFormFile? file)
    {
        if (file is null || file.Length == 0) return null;
        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!Allowed.Contains(ext)) return "Chỉ nhận ảnh .jpg, .png, .webp, .gif, .ico.";
        if (file.Length > MaxBytes) return "Ảnh tối đa 3 MB.";
        return null;
    }

    // Lưu vào wwwroot/uploads/{folder}, trả về đường dẫn dạng /uploads/{folder}/xxx.png
    public static string? Save(IFormFile? file, IWebHostEnvironment env, string folder)
    {
        if (file is null || file.Length == 0) return null;
        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        var dir = Path.Combine(env.WebRootPath, "uploads", folder);
        Directory.CreateDirectory(dir);
        var name = Guid.NewGuid().ToString("N") + ext;
        using var fs = new FileStream(Path.Combine(dir, name), FileMode.Create);
        file.CopyTo(fs);
        return $"/uploads/{folder}/{name}";
    }

    // Chỉ xóa file nằm trong wwwroot/uploads, không đụng tới ảnh có sẵn của website
    public static void Delete(string? url, IWebHostEnvironment env)
    {
        if (string.IsNullOrEmpty(url) || !url.StartsWith("/uploads/")) return;
        var root = Path.GetFullPath(Path.Combine(env.WebRootPath, "uploads"));
        var full = Path.GetFullPath(Path.Combine(env.WebRootPath, url.TrimStart('/').Replace('/', Path.DirectorySeparatorChar)));
        if (full.StartsWith(root) && File.Exists(full)) File.Delete(full);
    }
}