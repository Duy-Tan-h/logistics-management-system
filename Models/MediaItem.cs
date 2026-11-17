using System.ComponentModel.DataAnnotations;

namespace WebVanChuyenLogicstic.Models;

public static class MediaTypes
{
    public const string Partner = "Partner";
    public const string Banner = "Banner";
    public static readonly string[] All = [Partner, Banner];

    public static string Label(string? t) => t switch { Partner => "Đối tác", Banner => "Banner quảng cáo", _ => t ?? "" };
}

public class MediaItem
{
    public int Id { get; set; }

    [Required]
    [StringLength(20)]
    public string Type { get; set; } = MediaTypes.Partner;

    [Required(ErrorMessage = "Vui lòng nhập tiêu đề.")]
    [StringLength(150)]
    public string Title { get; set; } = string.Empty;

    [StringLength(300)]
    public string? ImageUrl { get; set; }          // do hệ thống gán sau khi upload

    [StringLength(300)]
    [RegularExpression(@"^(/[^\s]*|https?://\S+)$", ErrorMessage = "Liên kết phải bắt đầu bằng / hoặc http(s)://")]
    public string? LinkUrl { get; set; }

    [Range(0, 999, ErrorMessage = "Thứ tự từ 0 đến 999.")]
    public int DisplayOrder { get; set; }

    public bool IsVisible { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}