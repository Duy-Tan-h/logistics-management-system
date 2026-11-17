using System.ComponentModel.DataAnnotations;

namespace WebVanChuyenLogicstic.Models;

public class SavedAddress
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public UserAccount? User { get; set; }
    [StringLength(100)] public string? ContactName { get; set; }
    [StringLength(30)] public string? Phone { get; set; }

    [StringLength(60)] public string Label { get; set; } = string.Empty;     // "Nhà", "Kho Bình Dương"
    [StringLength(300)] public string Address { get; set; } = string.Empty;
}