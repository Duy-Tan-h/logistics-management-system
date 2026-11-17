namespace WebVanChuyenLogicstic.Models;

public static class AdminRoles
{
    public const string SuperAdmin = "SuperAdmin";
    public const string Dispatcher = "Dispatcher";
    public const string Editor = "Editor";
}

public class Role
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;      // SuperAdmin / Dispatcher / Editor
    public string Name { get; set; } = string.Empty;      // tên hiển thị
    public string? Description { get; set; }
}

public class AdminUser
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string PasswordSalt { get; set; } = string.Empty;
    public int RoleId { get; set; }
    public Role? Role { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}