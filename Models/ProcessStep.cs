using System.ComponentModel.DataAnnotations;

namespace WebVanChuyenLogicstic.Models;

public class ProcessStep
{
    public int Id { get; set; }

    [Range(1, 50, ErrorMessage = "Thứ tự từ 1 đến 50.")]
    public int Number { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tên bước.")]
    [StringLength(150, ErrorMessage = "Tên bước tối đa 150 ký tự.")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập mô tả.")]
    [StringLength(500, ErrorMessage = "Mô tả tối đa 500 ký tự.")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập biểu tượng.")]
    [StringLength(60)]
    [RegularExpression(@"^bi-[a-z0-9-]+$", ErrorMessage = "Biểu tượng phải có dạng bi-box-seam.")]
    public string Icon { get; set; } = "bi-box";
}