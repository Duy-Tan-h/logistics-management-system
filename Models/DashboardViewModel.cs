namespace WebVanChuyenLogicstic.Models;

public record StatusStat(string Status, int Count);
public record MonthRevenue(string Label, decimal Revenue, int Orders);

public class DashboardViewModel
{
    public string Range { get; set; } = "30d";
    public string RangeLabel { get; set; } = string.Empty;

    public int TotalOrders { get; set; }
    public int DeliveredOrders { get; set; }
    public decimal Revenue { get; set; }          // tổng giá báo của các đơn đã giao
    public int TotalCustomers { get; set; }
    public int Trips { get; set; }                // đơn đã gán xe (không tính đơn hủy)
    public int TripsInProgress { get; set; }

    public int PendingQuotes { get; set; }        // đơn đang chờ báo giá (cần xử lý)
    public int UnreadContacts { get; set; }
    public int PendingGroup { get; set; }      // yêu cầu báo giá + đã báo giá + đã xác nhận
    public int InTransitOrders { get; set; }
    public int CancelledOrders { get; set; }

    public List<StatusStat> StatusStats { get; set; } = new();
    public List<MonthRevenue> Months { get; set; } = new();
    public List<Order> RecentOrders { get; set; } = new();
}