using System.ComponentModel.DataAnnotations;

namespace WebVanChuyenLogicstic.Models.Admin;

public class HomeSectionEditViewModel
{
    public int Id { get; set; }

    public string SectionType { get; set; } = string.Empty;   // chỉ để hiển thị
    // 3 thuộc tính MỚI — chỉ để hiển thị, không có ô input nào gửi lên 3 giá trị này 
    public string SectionLabel { get; set; } = string.Empty;
    public bool SupportsItems { get; set; }
    public List<HomeSectionItem> Items { get; set; } = new();


    [StringLength(100, ErrorMessage = "Tối đa 100 ký tự.")]
    public string? Kicker { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tiêu đề.")]
    [StringLength(250, ErrorMessage = "Tối đa 250 ký tự.")]
    public string Title { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "Tối đa 500 ký tự.")]
    public string? Subtitle { get; set; }

    public string? Body { get; set; }

    [StringLength(100)]
    public string? ButtonText { get; set; }

    [StringLength(300)]
    [RegularExpression(@"^(/|https?://|tel:|mailto:).*", ErrorMessage = "Link phải bắt đầu bằng /, http(s)://, tel: hoặc mailto:")]
    public string? ButtonUrl { get; set; }

    [StringLength(100)]
    public string? SecondaryButtonText { get; set; }

    [StringLength(300)]
    [RegularExpression(@"^(/|https?://|tel:|mailto:).*", ErrorMessage = "Link phải bắt đầu bằng /, http(s)://, tel: hoặc mailto:")]
    public string? SecondaryButtonUrl { get; set; }
}