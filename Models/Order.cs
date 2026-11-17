using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace WebVanChuyenLogicstic.Models;

public static class OrderStatuses
{
    public const string QuoteRequested = "Yêu cầu báo giá";
    public const string Quoted = "Đã báo giá";
    public const string Confirmed = "Đã xác nhận";
    public const string InTransit = "Đang vận chuyển";
    public const string Delivered = "Đã giao";
    public const string Returned = "Hoàn hàng";
    public const string Cancelled = "Đã hủy";
    

    public static readonly string[] All = { QuoteRequested, Quoted, Confirmed, InTransit, Delivered, Returned, Cancelled };

    public static bool IsFinal(string status) => status == Delivered || status == Cancelled || status == Returned;

    // Mỗi trạng thái chỉ được đi tiếp tới các bước hợp lệ
    public static IReadOnlyList<string> Next(string status) => status switch
    {
        QuoteRequested => new[] { Quoted, Cancelled },
        Quoted => new[] { Confirmed, Cancelled },
        Confirmed => new[] { InTransit, Cancelled },   // InTransit chỉ do chuyến "Đang chạy" tự chuyển
        InTransit => new[] { Delivered, Returned },              // bỏ Cancelled
        _ => Array.Empty<string>()
    };
    public static bool CanCancel(string status) => status is QuoteRequested or Quoted or Confirmed;

    public static bool CanMove(string from, string to) => Next(from).Contains(to);

    public static string Badge(string status) => status switch
    {
        QuoteRequested => "bg-warning-subtle text-warning border border-warning",
        Quoted => "bg-info-subtle text-info border border-info",
        Confirmed => "bg-primary-subtle text-primary border border-primary",
        InTransit => "bg-dark-subtle text-dark border border-dark",
        Delivered => "bg-success-subtle text-success border border-success",
        Returned => "bg-danger-subtle text-danger border border-danger",
        _ => "bg-secondary-subtle text-secondary border border-secondary"
    };
}

public static class OrderMilestones
{
    public const string Received = "Đã nhận hàng";
    public const string InTransit = "Đang vận chuyển";
    public const string Delivering = "Đang giao hàng";
    public const string Delivered = "Đã giao";
    public const string Completed = "Hoàn thành";

    public static readonly string[] All = { Received, InTransit, Delivering, Delivered, Completed };

    public static string StatusOf(string milestone) => milestone switch
    {
        Delivered or Completed => OrderStatuses.Delivered,
        _ => OrderStatuses.InTransit
    };

    // Chỉ được đi đúng 1 bước kế tiếp, không nhảy cóc, không lùi
    public static string? Next(string? current)
    {
        if (current is null) return Received;
        var i = Array.IndexOf(All, current);
        return i >= 0 && i < All.Length - 1 ? All[i + 1] : null;
    }
    public static bool IsTripDriven(string milestone) => milestone is Received or InTransit or Completed;
}

public static class OrderFormat
{
    public static string Money(decimal? value) =>
        value is null ? "—" : string.Format(new CultureInfo("vi-VN"), "{0:N0} đ", value.Value);
}

public class Order
{
    public int Id { get; set; }

    [StringLength(50)]
    public string OrderCode { get; set; } = string.Empty;

    public int? CustomerId { get; set; }
    public UserAccount? Customer { get; set; }

    [StringLength(150)] public string CustomerName { get; set; } = string.Empty;
    [StringLength(30)] public string Phone { get; set; } = string.Empty;
    [StringLength(200)] public string? Email { get; set; }

    public int? ServiceId { get; set; }
    public TransportService? Service { get; set; }

    public int? VehicleId { get; set; }
    public Vehicle? Vehicle { get; set; }

    public int? DriverId { get; set; }
    public Driver? Driver { get; set; }

    [StringLength(300)] public string PickupAddress { get; set; } = string.Empty;
    [StringLength(300)] public string DeliveryAddress { get; set; } = string.Empty;
    [StringLength(150)] public string? ReceiverName { get; set; }
    [StringLength(30)] public string? ReceiverPhone { get; set; }
    [StringLength(150)] public string? CargoType { get; set; }
    public decimal? WeightKg { get; set; }
    public int? PackageCount { get; set; }
    public DateTime? ExpectedShipDate { get; set; }

    [StringLength(30)] public string Status { get; set; } = OrderStatuses.QuoteRequested;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    [StringLength(1000)] public string? Note { get; set; }

    // ---- Báo giá ----
    public decimal? QuotedPrice { get; set; }
    public DateTime? QuoteValidUntil { get; set; }
    [StringLength(1000)] public string? QuoteNote { get; set; }
    [StringLength(64)] public string? QuoteToken { get; set; }   // mã bí mật trong link email, đổi mỗi lần báo giá lại

    [NotMapped] public bool QuoteExpired => QuoteValidUntil.HasValue && QuoteValidUntil.Value < DateTime.Now;
    [NotMapped] public bool IsQuoteOpen => Status == OrderStatuses.Quoted && !QuoteExpired;
    [NotMapped]
    public string? CurrentMilestone => Events
    .OrderByDescending(e => e.SequenceNumber)
    .Select(e => e.Title)
    .FirstOrDefault(t => OrderMilestones.All.Contains(t));

    [NotMapped]
    public bool IsClosed =>
        Status == OrderStatuses.Cancelled || CurrentMilestone == OrderMilestones.Completed || Status == OrderStatuses.Returned;

    public List<OrderEvent> Events { get; set; } = new();
    public List<OrderItem> Items { get; set; } = new();

    // Tính lại 3 cột tổng hợp từ các dòng hàng
    public void RefreshCargoSummary()
    {
        if (Items.Count == 0) { CargoType = null; WeightKg = null; PackageCount = null; return; }
        var names = string.Join(", ", Items.Select(i => i.CargoTypeName).Distinct());
        CargoType = names.Length > 150 ? names[..150] : names;
        WeightKg = Items.Any(i => i.WeightKg.HasValue) ? Items.Sum(i => i.WeightKg ?? 0) : null;
        PackageCount = Items.Sum(i => i.Quantity);
    }

    public bool TryAcceptQuote(string channel)
    {
        if (!IsQuoteOpen) return false;
        Status = OrderStatuses.Confirmed;
        AddEvent("Khách đồng ý báo giá", channel);
        return true;
    }
    // Khách tự hủy khi đơn còn "Yêu cầu báo giá"
    public bool TryCancelRequest(string channel)
    {
        if (Status != OrderStatuses.QuoteRequested && Status != OrderStatuses.Confirmed) return false;
        Status = OrderStatuses.Cancelled;
        AddEvent("Khách hủy yêu cầu", channel);
        return true;
    }

    // Chuyến "Đang chạy" -> đơn Đang vận chuyển
    public bool StartTransit(string detail)
    {
        if (Status != OrderStatuses.Confirmed) return false;
        Status = OrderStatuses.InTransit;
        AddEvent(OrderMilestones.Received, detail);
        AddEvent(OrderMilestones.InTransit, detail);
        return true;
    }

    // Chuyến "Hoàn thành" -> đơn Đã giao + mốc Hoàn thành (ghi bù mốc còn thiếu)
    public bool CompleteByTrip(string detail)
    {
        if (Status != OrderStatuses.InTransit && Status != OrderStatuses.Delivered) return false;
        if (CurrentMilestone == OrderMilestones.Completed) return false;
        while (OrderMilestones.Next(CurrentMilestone) is string next)
        {
            AddEvent(next, detail);
            if (next == OrderMilestones.Completed) break;
        }
        Status = OrderStatuses.Delivered;
        return true;
    }

    public bool TryDeclineQuote(string channel)
    {
        if (!IsQuoteOpen) return false;
        Status = OrderStatuses.Cancelled;
        AddEvent("Khách từ chối báo giá", channel);
        return true;
    }

    public void AddEvent(string title, string? detail = null)
    {
        var next = Events.Count == 0 ? 1 : Events.Max(e => e.SequenceNumber) + 1;
        Events.Add(new OrderEvent
        {
            SequenceNumber = next,
            Title = title,
            Detail = detail,
            EventAt = DateTime.Now,
            IsCompleted = true
        });

    }
}

public class OrderEvent
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public Order? Order { get; set; }
    public int SequenceNumber { get; set; }

    [StringLength(150)] public string Title { get; set; } = string.Empty;
    [StringLength(500)] public string? Detail { get; set; }
    public DateTime EventAt { get; set; } = DateTime.Now;
    public bool IsCompleted { get; set; } = true;
}