using System.ComponentModel.DataAnnotations;

namespace WebVanChuyenLogicstic.Models;

public class SiteSetting
{
    private const string UrlPattern = @"^https?://\S+$";
    private const string UrlMsg = "Đường dẫn phải bắt đầu bằng http:// hoặc https://";

    public int Id { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tên website.")]
    [StringLength(100)]
    public string SiteName { get; set; } = "LMS Express";

    [StringLength(300)] public string? LogoUrl { get; set; }
    [StringLength(300)] public string? FaviconUrl { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập hotline.")]
    [StringLength(30)]
    public string Hotline { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập email.")]
    [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
    [StringLength(150)]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập địa chỉ.")]
    [StringLength(300)]
    public string Address { get; set; } = string.Empty;

    [StringLength(100)] public string? WorkingHours { get; set; }
    [StringLength(500)] public string? FooterIntro { get; set; }
    [StringLength(200)] public string? Copyright { get; set; }

    [StringLength(300)][RegularExpression(UrlPattern, ErrorMessage = UrlMsg)] public string? FacebookUrl { get; set; }
    [StringLength(300)][RegularExpression(UrlPattern, ErrorMessage = UrlMsg)] public string? YoutubeUrl { get; set; }
    [StringLength(300)][RegularExpression(UrlPattern, ErrorMessage = UrlMsg)] public string? LinkedInUrl { get; set; }
    [StringLength(300)][RegularExpression(UrlPattern, ErrorMessage = UrlMsg)] public string? TikTokUrl { get; set; }
    [StringLength(300)][RegularExpression(UrlPattern, ErrorMessage = UrlMsg)] public string? ZaloUrl { get; set; }
}