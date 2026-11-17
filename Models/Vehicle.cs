using System.ComponentModel.DataAnnotations;

namespace WebVanChuyenLogicstic.Models;

public class Vehicle
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập biển số.")]
    [StringLength(20, ErrorMessage = "Tối đa 20 ký tự.")]
    public string PlateNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng chọn loại xe.")]
    [StringLength(50)]
    public string VehicleType { get; set; } = string.Empty;

    [Range(0, 100000, ErrorMessage = "Tải trọng không hợp lệ.")]
    public decimal? CapacityKg { get; set; }

    [Required]
    [StringLength(30)]
    public string Status { get; set; } = "Đang hoạt động";

    [StringLength(500)]
    public string? Note { get; set; }
}