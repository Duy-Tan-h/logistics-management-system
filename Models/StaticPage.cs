using System.ComponentModel.DataAnnotations;

namespace WebVanChuyenLogicstic.Models;

public class StaticPage
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập đường dẫn.")]
    [StringLength(80)]
    [RegularExpression(@"^[a-z0-9]+(-[a-z0-9]+)*$", ErrorMessage = "Chỉ dùng chữ thường không dấu, số và dấu gạch ngang. Ví dụ: quy-dinh-gui-hang")]
    public string Slug { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập tiêu đề.")]
    [StringLength(150)]
    public string Title { get; set; } = string.Empty;

    [StringLength(300)]
    public string? Subtitle { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập nội dung.")]
    public string Content { get; set; } = string.Empty;   // HTML, chỉ admin được sửa

    [Required]
    [StringLength(60)]
    [RegularExpression(@"^bi-[a-z0-9-]+$", ErrorMessage = "Biểu tượng phải có dạng bi-shield-check.")]
    public string Icon { get; set; } = "bi-file-earmark-text";

    public bool IsPublished { get; set; } = true;
    public bool IsSystem { get; set; }   // trang gắn sẵn với menu/footer: không xóa, không đổi đường dẫn
}