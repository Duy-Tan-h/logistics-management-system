using System.ComponentModel.DataAnnotations;

namespace WebVanChuyenLogicstic.Models;

public class Driver
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập họ tên.")]
    [StringLength(150)]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
    [StringLength(30)]
    public string Phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập số bằng lái.")]
    [StringLength(50)]
    public string LicenseNumber { get; set; } = string.Empty;

    [Required]
    [StringLength(30)]
    public string Status { get; set; } = "Đang làm việc";

    [StringLength(500)]
    public string? Note { get; set; }
}