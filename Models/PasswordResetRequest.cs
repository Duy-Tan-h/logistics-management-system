namespace WebVanChuyenLogicstic.Models;

public class PasswordResetRequest
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public UserAccount? User { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public bool IsHandled { get; set; }
    public DateTime? HandledAt { get; set; }
}