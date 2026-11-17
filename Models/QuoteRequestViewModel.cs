using System.ComponentModel.DataAnnotations;

namespace WebVanChuyenLogicstic.Models;

public class QuoteRequestViewModel
{
    [Required(ErrorMessage = "Vui lòng nhập điểm lấy hàng.")]
    [StringLength(300)]
    public string PickupAddress { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập điểm giao hàng.")]
    [StringLength(300)]
    public string DeliveryAddress { get; set; } = string.Empty;
    [Required(ErrorMessage = "Vui lòng nhập tên người nhận.")]
    [StringLength(150)]
    public string ReceiverName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập số điện thoại người nhận.")]
    [RegularExpression(@"^(0|\+84)[0-9]{9,10}$", ErrorMessage = "Số điện thoại người nhận không hợp lệ.")]
    public string ReceiverPhone { get; set; } = string.Empty;


    public List<OrderItemInput> Items { get; set; } = new();

    public int? ServiceId { get; set; }
    public DateTime? ExpectedShipDate { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập họ tên.")]
    [StringLength(150)]
    public string CustomerName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
    [StringLength(30)]
    public string Phone { get; set; } = string.Empty;

    [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
    [StringLength(200)]
    public string? Email { get; set; }

    [StringLength(1000)]
    public string? Note { get; set; }
}