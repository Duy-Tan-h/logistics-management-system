using System.ComponentModel.DataAnnotations;

namespace WebVanChuyenLogicstic.Models.Admin;

public class TripFormViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tuyến đường.")]
    [StringLength(300, ErrorMessage = "Tuyến đường tối đa 300 ký tự.")]
    public string Route { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng chọn phương tiện.")]
    public int? VehicleId { get; set; }

    [Required(ErrorMessage = "Vui lòng chọn tài xế.")]
    public int? DriverId { get; set; }

    public DateTime? DepartAt { get; set; }

    [StringLength(500)]
    public string? Note { get; set; }

    public List<int> OrderIds { get; set; } = new();
}