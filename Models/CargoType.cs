using System.ComponentModel.DataAnnotations;

namespace WebVanChuyenLogicstic.Models;

public class CargoType
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tên loại hàng.")]
    [StringLength(100, ErrorMessage = "Tên loại hàng tối đa 100 ký tự.")]
    public string Name { get; set; } = string.Empty;

    [StringLength(300, ErrorMessage = "Mô tả tối đa 300 ký tự.")]
    public string? Description { get; set; }

    public bool IsSpecial { get; set; }          // hàng đặc biệt: dễ vỡ, cần bảo quản riêng
    public bool IsActive { get; set; } = true;   // false = không gợi ý trong form báo giá
}