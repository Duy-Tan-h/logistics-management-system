using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebVanChuyenLogicstic.Data;
using WebVanChuyenLogicstic.Filters;
using WebVanChuyenLogicstic.Models;
using WebVanChuyenLogicstic.Services;

namespace WebVanChuyenLogicstic.Controllers;

public class AccountController(IAccountService accounts, AppDbContext db) : Controller
{
    [HttpGet]
    public IActionResult Register() => View(new RegisterViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var (ok, message) = accounts.Register(model);
        if (!ok)
        {
            ModelState.AddModelError(nameof(model.Email), message);
            return View(model);
        }

        TempData["Success"] = message;
        return RedirectToAction(nameof(Login));
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewBag.ReturnUrl = returnUrl;
        return View(new LoginViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(LoginViewModel model, string? returnUrl = null)
    {
        if (!ModelState.IsValid) return View(model);

        // Thử tài khoản quản trị trước
        var email = model.Email.Trim();
        var admin = db.AdminUsers.Include(a => a.Role).FirstOrDefault(a => a.Email == email);
        if (admin != null && PasswordHasher.Verify(model.Password, admin.PasswordHash, admin.PasswordSalt))
        {
            if (!admin.IsActive)
            {
                ModelState.AddModelError(string.Empty, "Tài khoản đã bị khóa. Vui lòng liên hệ quản trị viên.");
                return View(model);
            }
            HttpContext.Session.Clear();
            HttpContext.Session.SetInt32("AdminId", admin.Id);   // KHÔNG đặt "UserId" để tránh trùng với Id khách
            HttpContext.Session.SetString("AdminRole", admin.Role!.Code);
            HttpContext.Session.SetString("UserName", admin.FullName);
            HttpContext.Session.SetString("UserEmail", admin.Email);
            HttpContext.Session.SetString("UserRole", "Admin");   // để menu công khai hiện link "Trang quản trị"
            if (Url.IsLocalUrl(returnUrl)) return Redirect(returnUrl!);
            return Redirect("/Admin");
        }

        var account = accounts.Login(model);
        if (account is null)
        {
            ModelState.AddModelError(string.Empty, "Email hoặc mật khẩu không đúng.");
            return View(model);
        }
        if (!account.IsActive)
        {
            ModelState.AddModelError(string.Empty, "Tài khoản đã bị khóa. Vui lòng liên hệ hotline để được hỗ trợ.");
            return View(model);
        }

        HttpContext.Session.Clear();
        HttpContext.Session.SetInt32("UserId", account.Id);
        HttpContext.Session.SetString("UserName", account.FullName);
        HttpContext.Session.SetString("UserEmail", account.Email);
        HttpContext.Session.SetString("UserRole", account.Role);

        if (Url.IsLocalUrl(returnUrl)) return Redirect(returnUrl!);
        return account.Role == "Admin"
            ? RedirectToAction("Home", "Admin")
            : RedirectToAction("Index", "Home");
    }

    [LoginRequired]
    [HttpGet]
    public IActionResult ChangePassword() => View(new ChangePasswordViewModel());

    [LoginRequired]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ChangePassword(ChangePasswordViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var userId = HttpContext.Session.GetInt32("UserId")!.Value;
        var (ok, message) = accounts.ChangePassword(userId, model);
        if (!ok)
        {
            ModelState.AddModelError(string.Empty, message);
            return View(model);
        }

        TempData["Success"] = message;
        return RedirectToAction(nameof(ChangePassword));
    }
    [HttpGet]
    public IActionResult ForgotPassword() => View(new ForgotPasswordViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ForgotPassword(ForgotPasswordViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var email = model.Email.Trim();
        var user = db.Users.FirstOrDefault(u => u.Email == email && u.Role == "Customer");

        // Chỉ tạo yêu cầu khi tài khoản tồn tại và chưa có yêu cầu nào đang chờ
        if (user != null && !db.PasswordResetRequests.Any(r => r.UserId == user.Id && !r.IsHandled))
        {
            db.PasswordResetRequests.Add(new PasswordResetRequest { UserId = user.Id });
            db.SaveChanges();
        }

        // Luôn trả cùng một thông báo để không lộ email nào đã đăng ký
        TempData["Success"] = "Nếu email này đã đăng ký, nhân viên sẽ cấp lại mật khẩu và gửi vào email đó trong thời gian sớm nhất.";
        return RedirectToAction(nameof(ForgotPassword));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }
}