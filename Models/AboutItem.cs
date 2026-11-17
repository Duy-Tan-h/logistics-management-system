using System.ComponentModel.DataAnnotations;

namespace WebVanChuyenLogicstic.Models;

public static class AboutKinds
{
    public const string Intro = "Intro";
    public const string Vision = "Vision";
    public const string Mission = "Mission";
    public const string CoreValue = "CoreValue";
    public const string Stat = "Stat";
    public const string Milestone = "Milestone";

    public static readonly string[] Order = [Intro, Vision, Mission, CoreValue, Stat, Milestone];
    public static readonly string[] Multi = [CoreValue, Stat, Milestone];   // thêm/sửa/xóa tự do; các loại còn lại chỉ sửa

    public static string Label(string kind) => kind switch
    {
        Intro => "Giới thiệu công ty",
        Vision => "Tầm nhìn",
        Mission => "Sứ mệnh",
        CoreValue => "Giá trị cốt lõi",
        Stat => "Số liệu nổi bật",
        Milestone => "Lịch sử hình thành",
        _ => kind
    };

    public static string Hint(string kind) => kind switch
    {
        Intro => "Đoạn mở đầu trang giới thiệu. Nội dung dài cách đoạn bằng một dòng trống.",
        Vision => "Hiện ở thẻ Tầm nhìn.",
        Mission => "Hiện ở thẻ Sứ mệnh.",
        CoreValue => "Mỗi giá trị là một thẻ có biểu tượng.",
        Stat => "Các ô con số cạnh phần giới thiệu (ví dụ 8+ năm kinh nghiệm).",
        Milestone => "Các mốc phát triển, hiện thành dòng thời gian theo thứ tự.",
        _ => ""
    };
}

public class AboutItem
{
    public int Id { get; set; }

    [Required]
    [StringLength(20)]
    public string Kind { get; set; } = AboutKinds.CoreValue;

    [StringLength(150)]
    public string? Value { get; set; }          // Intro: dòng nhãn nhỏ; Stat: con số; Milestone: năm

    [Required(ErrorMessage = "Vui lòng nhập tiêu đề.")]
    [StringLength(200, ErrorMessage = "Tiêu đề tối đa 200 ký tự.")]
    public string Title { get; set; } = string.Empty;

    [StringLength(2000, ErrorMessage = "Nội dung tối đa 2000 ký tự.")]
    public string? Content { get; set; }

    [StringLength(60)]
    [RegularExpression(@"^bi-[a-z0-9-]+$", ErrorMessage = "Biểu tượng phải có dạng bi-heart.")]
    public string? Icon { get; set; }

    [Range(0, 999, ErrorMessage = "Thứ tự từ 0 đến 999.")]
    public int DisplayOrder { get; set; }

    public bool IsVisible { get; set; } = true;
}