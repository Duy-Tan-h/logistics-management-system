using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebVanChuyenLogicstic.Data;
using WebVanChuyenLogicstic.Filters;
using WebVanChuyenLogicstic.Models;

namespace WebVanChuyenLogicstic.Controllers;

[AdminOnly(AdminRoles.Dispatcher, AdminRoles.Editor)] // lớp: mọi vai trò quản trị đều vào được Index
public class AdminController(AppDbContext db) : Controller
{

    // Mỗi vai trò có trang làm việc riêng
    public IActionResult Index() => HttpContext.Session.GetString("AdminRole") switch
    {
        AdminRoles.Dispatcher => Redirect("/Admin/Order"),
        AdminRoles.Editor => Redirect("/Admin/HomeContent/Index"),
        _ => RedirectToAction(nameof(Home))
    };

    [AdminOnly]   // Dashboard: chỉ SuperAdmin
    public IActionResult Home(string? range)
    {
        var (from, key, label) = ParseRange(range); // 7d / 30d / month / year / all
        var orders = db.Orders.AsNoTracking().AsQueryable();// Tạo truy vấn lấy đơn hàng, không theo dõi thay đổi và cho phép thêm điều kiện lọc 
        if (from != null) orders = orders.Where(o => o.CreatedAt >= from);// Nếu có ngày bắt đầu thì chỉ lấy đơn hàng được tạo từ ngày đó trở đi

        var statusCounts = orders.GroupBy(o => o.Status) // 1 truy vấn đếm theo trạng thái
            .Select(g => new { Status = g.Key, N = g.Count() })
            .ToList();
        int N(string s) => statusCounts.FirstOrDefault(x => x.Status == s)?.N ?? 0;// Hàm lấy số đơn theo trạng thái, nếu không có thì trả về 0
        var vm = new DashboardViewModel
        {
            Range = key,// Lưu mã khoảng thời gian đang được chọn
            RangeLabel = label,// Lưu tên khoảng thời gian để hiển thị trên giao diện
            TotalOrders = statusCounts.Sum(x => x.N),// Tính tổng số đơn hàng trong khoảng thời gian đã lọc
            DeliveredOrders = statusCounts.Where(x => x.Status == OrderStatuses.Delivered).Sum(x => x.N), // Đếm số đơn hàng đã giao thành công
            Revenue = orders.Where(o => o.Status != OrderStatuses.Cancelled).Sum(o => o.QuotedPrice) ?? 0m,// Tính tổng giá trị đơn không bị hủy, nếu null thì lấy 0
            Trips = db.Trips.Count(t => t.Status != TripStatuses.Cancelled),// Đếm tổng số chuyến xe không bị hủy trong database
            TripsInProgress = db.Trips.Count(t => t.Status == TripStatuses.Running),// Đếm số chuyến xe đang chạy
            TotalCustomers = db.Users.Count(u => u.Role == "Customer"),// Đếm tổng số tài khoản có vai trò khách hàng
            PendingQuotes = db.Orders.Count(o => o.Status == OrderStatuses.QuoteRequested), // Đếm số đơn đang yêu cầu báo giá trong toàn bộ database
            UnreadContacts = db.Contacts.Count(c => !c.IsRead), // Đếm số liên hệ chưa được đọc
            PendingGroup = N(OrderStatuses.QuoteRequested) + N(OrderStatuses.Quoted) + N(OrderStatuses.Confirmed),// Cộng số đơn yêu cầu báo giá, đã báo giá và đã xác nhận
            InTransitOrders = N(OrderStatuses.InTransit),// Lấy số đơn hàng đang vận chuyển
            CancelledOrders = N(OrderStatuses.Cancelled), // Lấy số đơn hàng đã bị hủy
            StatusStats = OrderStatuses.All // Lấy danh sách tất cả trạng thái đơn hàng của hệ thống0=
                .Select(s => new StatusStat(s, statusCounts.FirstOrDefault(x => x.Status == s)?.N ?? 0))
                .ToList(),
            RecentOrders = db.Orders.AsNoTracking().OrderByDescending(o => o.CreatedAt).Take(6).ToList()
        };

        // Doanh thu 6 tháng gần nhất (không phụ thuộc bộ lọc thời gian)
        var now = DateTime.Now;
        var start = new DateTime(now.Year, now.Month, 1).AddMonths(-5);
        var recent = db.Orders.AsNoTracking()
            .Where(o => o.CreatedAt >= start)
            .Select(o => new { o.CreatedAt, o.Status, o.QuotedPrice })
            .ToList();
        vm.Months = Enumerable.Range(0, 6).Select(i => start.AddMonths(i)).Select(m => new MonthRevenue(
            $"{m.Month:00}/{m.Year}",
            recent.Where(o => o.Status != OrderStatuses.Cancelled && o.CreatedAt.Year == m.Year && o.CreatedAt.Month == m.Month)
                  .Sum(o => o.QuotedPrice ?? 0m),
            recent.Count(o => o.CreatedAt.Year == m.Year && o.CreatedAt.Month == m.Month))).ToList();

        return View(vm);
    }

    // Xuất báo cáo đơn hàng theo khoảng thời gian đang chọn (mở được bằng Excel)
    public IActionResult ExportOrders(string? range)
    {
        var (from, _, _) = ParseRange(range);
        var q = db.Orders.AsNoTracking().AsQueryable();
        if (from != null) q = q.Where(o => o.CreatedAt >= from);

        var rows = q.OrderByDescending(o => o.CreatedAt)
            .Select(o => new { o.OrderCode, o.CustomerName, o.Phone, o.PickupAddress, o.DeliveryAddress, o.Status, o.QuotedPrice, o.CreatedAt })
            .ToList();

        var sb = new StringBuilder();
        sb.AppendLine("Mã đơn,Khách hàng,Số điện thoại,Điểm lấy hàng,Điểm giao hàng,Trạng thái,Giá báo (VNĐ),Ngày tạo");
        foreach (var r in rows)
        {
            sb.AppendLine(string.Join(",",
                Csv(r.OrderCode), Csv(r.CustomerName), Csv(r.Phone), Csv(r.PickupAddress), Csv(r.DeliveryAddress),
                Csv(r.Status), Csv(r.QuotedPrice?.ToString("0")), Csv(r.CreatedAt.ToString("dd/MM/yyyy HH:mm"))));
        }

        var bytes = new UTF8Encoding(true).GetPreamble().Concat(Encoding.UTF8.GetBytes(sb.ToString())).ToArray();
        return File(bytes, "text/csv", $"bao-cao-don-hang-{DateTime.Now:yyyyMMdd}.csv");
    }

    private static (DateTime? From, string Key, string Label) ParseRange(string? range)
    {
        var today = DateTime.Now.Date;
        return range switch
        {
            "7d" => (today.AddDays(-6), "7d", "7 ngày gần nhất"),
            "month" => (new DateTime(today.Year, today.Month, 1), "month", "tháng này"),
            "year" => (new DateTime(today.Year, 1, 1), "year", "năm nay"),
            "all" => (null, "all", "toàn bộ thời gian"),
            _ => (today.AddDays(-29), "30d", "30 ngày gần nhất")
        };
    }

    // Bao dấu ngoặc kép, và chặn công thức độc hại khi mở bằng Excel
    private static string Csv(string? v)
    {
        v ??= "";
        if (v.Length > 0 && "=+-@".Contains(v[0])) v = "'" + v;
        return "\"" + v.Replace("\"", "\"\"") + "\"";
    }
}