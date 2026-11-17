using System.ComponentModel.DataAnnotations;

namespace WebVanChuyenLogicstic.Models;

public class TransportService
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tên dịch vụ.")]
    [StringLength(150, ErrorMessage = "Tên dịch vụ tối đa 150 ký tự.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập mô tả ngắn.")]
    [StringLength(500, ErrorMessage = "Mô tả ngắn tối đa 500 ký tự.")]
    public string Summary { get; set; } = string.Empty;

    [StringLength(4000, ErrorMessage = "Mô tả chi tiết tối đa 4000 ký tự.")]
    public string? Description { get; set; }     // nội dung trang chi tiết, để trống thì dùng nội dung mặc định

    [Required(ErrorMessage = "Vui lòng nhập biểu tượng.")]
    [StringLength(60)]
    [RegularExpression(@"^bi-[a-z0-9-]+$", ErrorMessage = "Biểu tượng phải có dạng bi-truck.")]
    public string Icon { get; set; } = "bi-truck";

    [Required(ErrorMessage = "Vui lòng nhập thời gian giao dự kiến.")]
    [StringLength(100, ErrorMessage = "Tối đa 100 ký tự.")]
    public string DeliveryTime { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập ghi chú giá.")]
    [StringLength(150, ErrorMessage = "Tối đa 150 ký tự.")]
    public string PriceNote { get; set; } = string.Empty;

    public bool IsFeatured { get; set; }
    public bool IsExclusive { get; set; }   // xe dành riêng: chuyến chỉ chở đúng 1 đơn

    [Range(0, 999, ErrorMessage = "Thứ tự từ 0 đến 999.")]
    public int FeaturedOrder { get; set; }

    [StringLength(300)]
    [RegularExpression(@"^(/[^\s]*|https?://[^\s]+)$", ErrorMessage = "Đường dẫn ảnh phải bắt đầu bằng / hoặc http(s)://")]
    public string? ImageUrl { get; set; }

    public bool IsVisible { get; set; } = true;   // false = ẩn khỏi website

}