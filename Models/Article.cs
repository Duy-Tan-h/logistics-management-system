using System.ComponentModel.DataAnnotations;

namespace WebVanChuyenLogicstic.Models
{
    public static class ArticleCategories
    {
        public static readonly string[] All =
        [
            "Tin tức", "Thông báo", "Khuyến mãi", "Hướng dẫn gửi hàng",
            "Thị trường logistics", "Kinh nghiệm", "Xuất nhập khẩu"
        ];
    }

    public class Article
    {
        [RegularExpression(@"^(/[^\s]*|https?://\S+)$", ErrorMessage = "Đường dẫn ảnh phải bắt đầu bằng / hoặc http(s)://")]
        public string? ImageUrl { get; set; }
        public bool IsFeatured { get; set; }
        public int FeaturedOrder { get; set; }
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập biểu tượng.")]
        [StringLength(60)]
        public string Icon { get; set; } = "bi-newspaper";

        [Required(ErrorMessage = "Vui lòng chọn chuyên mục.")]
        [StringLength(100)]
        public string Category { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập tiêu đề.")]
        [StringLength(250, ErrorMessage = "Tiêu đề tối đa 250 ký tự.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập tóm tắt.")]
        [StringLength(500, ErrorMessage = "Tóm tắt tối đa 500 ký tự.")]
        public string Summary { get; set; } = string.Empty;

        public DateTime PublishedAt { get; set; } = DateTime.Now;

        [StringLength(50)]
        public string ArtClass { get; set; } = "art-one";
        public bool IsPublished { get; set; } = true;

        public List<ArticleBlock> Blocks { get; set; } = [];   // 1 bài có nhiều đoạn nội dung
    }
    public class ArticleBlock
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }        // khóa ngoại trỏ về bài viết
        public Article? Article { get; set; }

        public string Heading { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public int DisplayOrder { get; set; }
    }
}