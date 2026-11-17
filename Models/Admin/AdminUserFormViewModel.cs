using System.ComponentModel.DataAnnotations;

namespace WebVanChuyenLogicstic.Models.Admin;

public class AdminUserFormViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập họ tên.")]
    [StringLength(150, MinimumLength = 2, ErrorMessage = "Họ tên từ 2 đến 150 ký tự.")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập email.")]
    [EmailAddress(ErrorMessage = "Email không đúng định dạng.")]
    [StringLength(200)]
    public string Email { get; set; } = string.Empty;

    // Thêm mới: bắt buộc. Sửa: để trống = giữ mật khẩu cũ
    [StringLength(100)]
    public string? Password { get; set; }

    [Required(ErrorMessage = "Vui lòng chọn vai trò.")]
    public int? RoleId { get; set; }

    public bool IsActive { get; set; } = true;
}