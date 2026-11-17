using System.Net;
using WebVanChuyenLogicstic.Models;

namespace WebVanChuyenLogicstic.Services;

public interface IOrderNotifier
{
    Task<bool> SendReceivedAsync(Order order, string trackingUrl);
    Task<bool> SendQuoteAsync(Order order, string respondUrl);
    Task<bool> SendStatusAsync(Order order, string? note, string trackingUrl);
}

public class OrderNotifier(IEmailService email) : IOrderNotifier
{
    public Task<bool> SendReceivedAsync(Order order, string trackingUrl)
    {
        if (string.IsNullOrWhiteSpace(order.Email)) return Task.FromResult(false);
        return email.SendAsync(order.Email,
            $"[LMS Express] Đã nhận yêu cầu báo giá {order.OrderCode}",
            Build(order, "Chúng tôi đã nhận được yêu cầu báo giá của bạn. Chuyên viên sẽ liên hệ trong thời gian sớm nhất.", null, trackingUrl, "Tra cứu đơn hàng"));
    }

    public Task<bool> SendQuoteAsync(Order order, string respondUrl)
    {
        if (string.IsNullOrWhiteSpace(order.Email)) return Task.FromResult(false);

        var until = order.QuoteValidUntil?.ToString("dd/MM/yyyy") ?? "—";
        var noteHtml = string.IsNullOrWhiteSpace(order.QuoteNote)
            ? ""
            : $"<p style=\"background:#f5f7fa;padding:10px 14px;border-radius:6px\"><strong>Ghi chú về giá:</strong> {E(order.QuoteNote)}</p>";

        var html = $"""
        <div style="font-family:Arial,sans-serif;max-width:560px;margin:auto;color:#222;line-height:1.5">
          <h2 style="color:#0d6efd;margin-bottom:4px">LMS Express</h2>
          <p>Xin chào <strong>{E(order.CustomerName)}</strong>,</p>
          <p>Chúng tôi đã có báo giá cho yêu cầu vận chuyển của bạn.</p>
          <div style="background:#f5f7fa;border-radius:8px;padding:16px 20px;margin:16px 0">
            <div style="color:#666;font-size:13px">Giá báo</div>
            <div style="font-size:28px;font-weight:bold">{E(OrderFormat.Money(order.QuotedPrice))}</div>
            <div style="color:#666;font-size:13px;margin-top:6px">Hiệu lực đến hết ngày {E(until)}</div>
          </div>
          {noteHtml}
          <table style="border-collapse:collapse;width:100%;font-size:14px">
            <tr><td style="padding:6px 0;color:#666;width:130px">Mã đơn hàng</td><td><strong>{E(order.OrderCode)}</strong></td></tr>
            <tr><td style="padding:6px 0;color:#666">Lấy hàng</td><td>{E(order.PickupAddress)}</td></tr>
            <tr><td style="padding:6px 0;color:#666">Giao hàng</td><td>{E(order.DeliveryAddress)}</td></tr>
          </table>
          <p style="margin-top:20px">
            <a href="{respondUrl}" style="background:#ff7a1a;color:#fff;padding:12px 24px;border-radius:6px;text-decoration:none;font-weight:bold">Xem và trả lời báo giá</a>
          </p>
          <p style="color:#666;font-size:13px">Bạn đồng ý thì bấm nút trên để xác nhận đơn hàng. Bạn cũng có thể gọi cho chúng tôi để trao đổi thêm.</p>
          <p style="color:#999;font-size:12px">Đây là email tự động, vui lòng không trả lời trực tiếp. Không chuyển tiếp email này cho người khác vì liên kết trong thư dùng để xác nhận đơn của bạn.</p>
        </div>
        """;

        return email.SendAsync(order.Email, $"[LMS Express] Báo giá cho đơn {order.OrderCode}", html);
    }

    public Task<bool> SendStatusAsync(Order order, string? note, string trackingUrl)
    {
        if (string.IsNullOrWhiteSpace(order.Email)) return Task.FromResult(false);

        var message = order.Status switch
        {
            OrderStatuses.Confirmed => "Đơn hàng của bạn đã được <strong>xác nhận</strong>. Chúng tôi sẽ sắp xếp xe và tài xế để vận chuyển.",
            OrderStatuses.InTransit => "Đơn hàng của bạn <strong>đang được vận chuyển</strong>.",
            OrderStatuses.Delivered => "Đơn hàng của bạn đã được <strong>giao thành công</strong>. Cảm ơn bạn đã sử dụng dịch vụ.",
            OrderStatuses.Returned => "Rất tiếc, đơn hàng của bạn <strong>không giao được và sẽ được hoàn về</strong>. Chúng tôi sẽ liên hệ để thống nhất cách xử lý.",
            OrderStatuses.Cancelled => "Đơn hàng của bạn đã bị <strong>hủy</strong>. Nếu có thắc mắc, vui lòng liên hệ với chúng tôi.",
            _ => $"Trạng thái đơn hàng: <strong>{E(order.Status)}</strong>."
        };

        return email.SendAsync(order.Email,
            $"[LMS Express] Đơn {order.OrderCode}: {order.Status}",
            Build(order, message, note, trackingUrl, "Tra cứu đơn hàng"));
    }

    private static string E(string? s) => WebUtility.HtmlEncode(s ?? string.Empty);

    private static string Build(Order o, string message, string? note, string url, string buttonText)
    {
        var noteHtml = string.IsNullOrWhiteSpace(note)
            ? ""
            : $"<p style=\"background:#f5f7fa;padding:10px 14px;border-radius:6px\"><strong>Ghi chú:</strong> {E(note)}</p>";

        return $"""
        <div style="font-family:Arial,sans-serif;max-width:560px;margin:auto;color:#222;line-height:1.5">
          <h2 style="color:#0d6efd;margin-bottom:4px">LMS Express</h2>
          <p>Xin chào <strong>{E(o.CustomerName)}</strong>,</p>
          <p>{message}</p>
          {noteHtml}
          <table style="border-collapse:collapse;width:100%;font-size:14px">
            <tr><td style="padding:6px 0;color:#666;width:130px">Mã đơn hàng</td><td><strong>{E(o.OrderCode)}</strong></td></tr>
            <tr><td style="padding:6px 0;color:#666">Lấy hàng</td><td>{E(o.PickupAddress)}</td></tr>
            <tr><td style="padding:6px 0;color:#666">Giao hàng</td><td>{E(o.DeliveryAddress)}</td></tr>
            <tr><td style="padding:6px 0;color:#666">Trạng thái</td><td>{E(o.Status)}</td></tr>
          </table>
          <p style="margin-top:20px">
            <a href="{url}" style="background:#0d6efd;color:#fff;padding:10px 20px;border-radius:6px;text-decoration:none">{buttonText}</a>
          </p>
          <p style="color:#999;font-size:12px">Đây là email tự động, vui lòng không trả lời trực tiếp.</p>
        </div>
        """;
    }
}