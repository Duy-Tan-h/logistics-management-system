using System.Net;
using System.Net.Mail;
using System.Text;
using Microsoft.Extensions.Options;

namespace WebVanChuyenLogicstic.Services;

public class EmailSettings
{
    public bool Enabled { get; set; }
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; } = 587;
    public bool EnableSsl { get; set; } = true;
    public string User { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FromEmail { get; set; } = string.Empty;
    public string FromName { get; set; } = "LMS Express";
}

public interface IEmailService
{
    // true = đã gửi; false = tắt/cấu hình thiếu/lỗi (lỗi đã được ghi log)
    Task<bool> SendAsync(string to, string subject, string htmlBody);
}

public class EmailService(IOptions<EmailSettings> options, ILogger<EmailService> logger) : IEmailService
{
    public async Task<bool> SendAsync(string to, string subject, string htmlBody)
    {
        var s = options.Value;
        if (!s.Enabled || string.IsNullOrWhiteSpace(s.Host) || string.IsNullOrWhiteSpace(s.FromEmail))
            return false;

        try
        {
            using var client = new SmtpClient(s.Host, s.Port)
            {
                EnableSsl = s.EnableSsl,
                Credentials = new NetworkCredential(s.User, s.Password)
            };
            using var msg = new MailMessage
            {
                From = new MailAddress(s.FromEmail, s.FromName, Encoding.UTF8),
                Subject = subject,
                SubjectEncoding = Encoding.UTF8,
                Body = htmlBody,
                BodyEncoding = Encoding.UTF8,
                IsBodyHtml = true
            };
            msg.To.Add(to);
            await client.SendMailAsync(msg);
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Không gửi được email tới {To}", to);
            return false;
        }
    }
}