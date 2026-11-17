using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Cryptography;
using WebVanChuyenLogicstic.Data;
using WebVanChuyenLogicstic.Filters;
using WebVanChuyenLogicstic.Helpers;
using WebVanChuyenLogicstic.Models;
using WebVanChuyenLogicstic.Models.Admin;
using WebVanChuyenLogicstic.Services;

namespace WebVanChuyenLogicstic.Controllers;

[Route("Admin/Customer/{action=Index}")]
[AdminOnly]
public class CustomerController(AppDbContext db, IEmailService email) : Controller
{
    public IActionResult Index(string? search, int? page)
    {
        var q = db.Users.AsNoTracking().Where(u => u.Role == "Customer");

        if (!string.IsNullOrWhiteSpace(search))
        {
            var s = search.Trim();
            q = q.Where(u => u.FullName.Contains(s) || u.Email.Contains(s) || u.Phone.Contains(s));
        }

        var rows = q.OrderBy(u => u.FullName)
            .Select(u => new CustomerRow
            {
                Id = u.Id,
                FullName = u.FullName,
                Email = u.Email,
                Phone = u.Phone,
                OrderCount = db.Orders.Count(o => o.CustomerId == u.Id),
                IsActive = u.IsActive,
                WaitingReply = db.Orders.Count(o => o.CustomerId == u.Id && o.Status == OrderStatuses.Quoted)
            })
            .ToPagedList(page);

        ViewBag.Search = search;
        ViewBag.Requests = db.PasswordResetRequests.AsNoTracking()
            .Include(r => r.User)
            .Where(r => !r.IsHandled)
            .OrderBy(r => r.CreatedAt)
            .ToList();
        return View("~/Views/Admin/Customer/Index.cshtml", rows);
    }

    [HttpPost("{id}")]
    [ValidateAntiForgeryToken]
    public IActionResult ToggleActive(int id)
    {
        var user = db.Users.FirstOrDefault(u => u.Id == id && u.Role == "Customer");
        if (user is null) return NotFound();
        user.IsActive = !user.IsActive;
        db.SaveChanges();
        TempData["SuccessMessage"] = user.IsActive ? "Đã mở khóa tài khoản." : "Đã khóa tài khoản. Khách sẽ không đăng nhập được.";
        return RedirectToAction(nameof(Details), new { id });
    }

    [HttpGet("{id}")]
    public IActionResult Details(int id)
    {
        var user = db.Users.AsNoTracking().FirstOrDefault(u => u.Id == id && u.Role == "Customer");
        if (user is null) return NotFound();

        var vm = new CustomerDetailViewModel
        {
            Customer = user,
            Orders = db.Orders.AsNoTracking()
                .Where(o => o.CustomerId == id)
                .OrderByDescending(o => o.CreatedAt)
                .ToList()
        };
        ViewBag.PendingReset = db.PasswordResetRequests.Any(r => r.UserId == id && !r.IsHandled);
        return View("~/Views/Admin/Customer/Details.cshtml", vm);
    }

    // Đặt lại mật khẩu: sinh mật khẩu tạm, gửi vào email của tài khoản; không gửi được thì hiện cho admin
    [HttpPost("{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetPassword(int id)
    {
        var user = db.Users.FirstOrDefault(u => u.Id == id && u.Role == "Customer");
        if (user is null) return NotFound();

        var temp = Convert.ToHexString(RandomNumberGenerator.GetBytes(6));   // 12 ký tự hex
        var (hash, salt) = PasswordHasher.Create(temp);
        user.PasswordHash = hash;
        user.PasswordSalt = salt;

        foreach (var r in db.PasswordResetRequests.Where(r => r.UserId == id && !r.IsHandled))
        {
            r.IsHandled = true;
            r.HandledAt = DateTime.Now;
        }
        db.SaveChanges();

        if (await SendTempPasswordAsync(user, temp))
        {
            TempData["SuccessMessage"] = "Đã đặt lại mật khẩu và gửi email cho khách.";
        }
        else
        {
            TempData["SuccessMessage"] = "Đã đặt lại mật khẩu.";
            TempData["TempPassword"] = temp;
            TempData["ErrorMessage"] = "Chưa gửi được email (kiểm tra cấu hình SMTP / xem log). Hãy đọc mật khẩu tạm cho khách qua điện thoại.";
        }
        return RedirectToAction(nameof(Details), new { id });
    }

    // Bỏ qua yêu cầu (ví dụ khách đã nhớ lại mật khẩu)
    [HttpPost("{id}")]
    [ValidateAntiForgeryToken]
    public IActionResult DismissRequest(int id)
    {
        var r = db.PasswordResetRequests.Find(id);
        if (r != null && !r.IsHandled)
        {
            r.IsHandled = true;
            r.HandledAt = DateTime.Now;
            db.SaveChanges();
            TempData["SuccessMessage"] = "Đã bỏ qua yêu cầu.";
        }
        return RedirectToAction(nameof(Index));
    }

    // Xóa tài khoản: các đơn của khách vẫn giữ, chỉ mất liên kết CustomerId (SetNull)
    [HttpPost("{id}")]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        var user = db.Users.FirstOrDefault(u => u.Id == id && u.Role == "Customer");
        if (user is null) return NotFound();

        db.Users.Remove(user);
        db.SaveChanges();
        TempData["SuccessMessage"] = "Đã xóa tài khoản khách.";
        return RedirectToAction(nameof(Index));
    }

    // Luôn gửi tới email của tài khoản trong DB
    private Task<bool> SendTempPasswordAsync(UserAccount user, string temp)
    {
        var url = Url.Action("ChangePassword", "Account", null, Request.Scheme)!;
        var html = $"""
        <div style="font-family:Arial,sans-serif;max-width:560px;margin:auto;color:#222;line-height:1.5">
          <h2 style="color:#0d6efd;margin-bottom:4px">LMS Express</h2>
          <p>Xin chào <strong>{WebUtility.HtmlEncode(user.FullName)}</strong>,</p>
          <p>Theo yêu cầu của bạn, chúng tôi đã cấp lại mật khẩu cho tài khoản.</p>
          <div style="background:#f5f7fa;border-radius:8px;padding:16px 20px;margin:16px 0">
            <div style="color:#666;font-size:13px">Mật khẩu tạm</div>
            <div style="font-size:24px;font-weight:bold;letter-spacing:1px">{temp}</div>
          </div>
          <p>Hãy đăng nhập bằng mật khẩu này, rồi đổi sang mật khẩu của riêng bạn.</p>
          <p><a href="{url}" style="background:#ff7a1a;color:#fff;padding:12px 24px;border-radius:6px;text-decoration:none;font-weight:bold">Đăng nhập và đổi mật khẩu</a></p>
          <p style="color:#999;font-size:12px">Nếu bạn không yêu cầu, hãy liên hệ chúng tôi ngay. Đây là email tự động, vui lòng không trả lời trực tiếp.</p>
        </div>
        """;
        return email.SendAsync(user.Email, "[LMS Express] Mật khẩu tạm cho tài khoản của bạn", html);
    }
}