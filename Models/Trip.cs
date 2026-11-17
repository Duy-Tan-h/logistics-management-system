namespace WebVanChuyenLogicstic.Models;

public static class TripStatuses
{
    public const string Planned = "Chờ khởi hành";
    public const string Running = "Đang chạy";
    public const string Done = "Hoàn thành";
    public const string Cancelled = "Đã hủy";

    public static readonly string[] All = { Planned, Running, Done, Cancelled };

    public static IReadOnlyList<string> Next(string s) => s switch
    {
        Planned => new[] { Running, Cancelled },
        Running => new[] { Done },
        _ => Array.Empty<string>()
    };
}

public class Trip
{
    public int Id { get; set; }
    public string Route { get; set; } = string.Empty;
    public int VehicleId { get; set; }
    public Vehicle? Vehicle { get; set; }
    public int DriverId { get; set; }
    public Driver? Driver { get; set; }
    public DateTime? DepartAt { get; set; }
    public string Status { get; set; } = TripStatuses.Planned;
    public string? Note { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public List<TripOrder> Orders { get; set; } = new();
}

// Bảng trung gian đơn - chuyến (DON_HANG_CHUYEN_XE)
public class TripOrder
{
    public int Id { get; set; }
    public int TripId { get; set; }
    public Trip? Trip { get; set; }
    public int OrderId { get; set; }
    public Order? Order { get; set; }
}