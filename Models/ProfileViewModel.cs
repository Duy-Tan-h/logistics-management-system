using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WebVanChuyenLogicstic.Models;

public class ProfileViewModel
{
    [ValidateNever] public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập họ tên.")]
    [StringLength(150)]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
    [RegularExpression(@"^(0|\+84)[0-9]{9,10}$", ErrorMessage = "Số điện thoại không hợp lệ.")]
    public string Phone { get; set; } = string.Empty;

    [ValidateNever] public List<SavedAddress> Addresses { get; set; } = new();
}