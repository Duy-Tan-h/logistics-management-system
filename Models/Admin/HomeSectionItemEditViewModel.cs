using System.ComponentModel.DataAnnotations;

namespace WebVanChuyenLogicstic.Models.Admin
{
    public class HomeSectionItemEditViewModel
    {
        public int Id { get; set; }            // 0 = thêm mới
        public int SectionId { get; set; }

        public string SectionType { get; set; } = string.Empty;
        public string SectionLabel { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập tiêu đề.")]
        [StringLength(250, ErrorMessage = "Tối đa 250 ký tự.")]
        public string Title { get; set; } = string.Empty;

        [StringLength(1000, ErrorMessage = "Tối đa 1000 ký tự.")]
        public string? Description { get; set; }

        [StringLength(60, ErrorMessage = "Tối đa 60 ký tự.")]
        [RegularExpression(@"^bi-[a-z0-9-]+$", ErrorMessage = "Icon phải có dạng bi-truck, bi-clock-history...")]
        public string? Icon { get; set; }

        [StringLength(300, ErrorMessage = "Tối đa 300 ký tự.")]
        [RegularExpression(@"^(/|https?://).*", ErrorMessage = "Link phải bắt đầu bằng / hoặc http(s)://")]
        public string? ImageUrl { get; set; }

        [StringLength(300, ErrorMessage = "Tối đa 300 ký tự.")]
        [RegularExpression(@"^(/|https?://).*", ErrorMessage = "Link phải bắt đầu bằng / hoặc http(s)://")]
        public string? LinkUrl { get; set; }

        [StringLength(50, ErrorMessage = "Tối đa 50 ký tự.")]
        public string? Value { get; set; }

        public bool IsVisible { get; set; } = true;
    }
}