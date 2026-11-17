namespace WebVanChuyenLogicstic.Models.Admin;

public class CustomerRow
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public int OrderCount { get; set; }
    public bool IsActive { get; set; }
    public int WaitingReply { get; set; }   // số đơn đang "Đã báo giá", chờ khách trả lời
}

public class CustomerDetailViewModel
{
    public UserAccount Customer { get; set; } = null!;
    public List<Order> Orders { get; set; } = new();
}