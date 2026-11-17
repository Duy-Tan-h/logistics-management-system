using System.ComponentModel.DataAnnotations;

namespace WebVanChuyenLogicstic.Models.Admin
{
    // Danh sách khuôn được phép dùng để tạo Section mới
    public static class DuplicableSectionTypes
    {
        public static readonly Dictionary<string, string> Options = new()
    {
        { HomeSectionTypes.WhyChooseUs, "Danh sách thẻ (icon + tiêu đề + mô tả)" },
        { HomeSectionTypes.Projects,    "Danh sách dự án / ảnh" },
        { HomeSectionTypes.ServiceIntro,"Khối giới thiệu (chữ + nút)" },
        { HomeSectionTypes.Cta,         "Banner kêu gọi hành động" },
    };
    }
    public class HomeSectionCreateViewModel
    {
        [Required(ErrorMessage = "Vui lòng chọn 1 khuôn giao diện.")]
        public string SectionType { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập tiêu đề.")]
        [StringLength(250)]
        public string Title { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Subtitle { get; set; }

        public string? Body { get; set; }

        [StringLength(100)] public string? ButtonText { get; set; }
        [StringLength(300)] public string? ButtonUrl { get; set; }
    }
}
