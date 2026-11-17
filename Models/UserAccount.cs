namespace WebVanChuyenLogicstic.Models;

public class UserAccount
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string PasswordSalt { get; set; } = string.Empty;
    public string Role { get; set; } = "Customer";   // "Admin" hoặc "Customer"
    public bool IsActive { get; set; } = true;
}
