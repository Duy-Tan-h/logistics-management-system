using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using WebVanChuyenLogicstic.Data;
using WebVanChuyenLogicstic.Models;

namespace WebVanChuyenLogicstic.Filters;

public class AdminOnlyAttribute(params string[] allowedRoles) : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var http = context.HttpContext;
        var returnUrl = http.Request.Path + http.Request.QueryString;

        if (http.Session.GetInt32("AdminId") is not int adminId)
        {
            // Khách đã đăng nhập -> về trang chủ; chưa đăng nhập -> trang Login
            context.Result = string.IsNullOrEmpty(http.Session.GetString("UserRole"))
                ? new RedirectToActionResult("Login", "Account", new { returnUrl })
                : new RedirectToActionResult("Index", "Home", null);
            return;
        }

        var db = http.RequestServices.GetRequiredService<AppDbContext>();
        var admin = db.AdminUsers.AsNoTracking().Include(a => a.Role)
            .FirstOrDefault(a => a.Id == adminId && a.IsActive);
        if (admin is null)   // bị khóa hoặc đã xóa
        {
            http.Session.Clear();
            context.Result = new RedirectToActionResult("Login", "Account", new { returnUrl });
            return;
        }

        var code = admin.Role!.Code;
        http.Session.SetString("AdminRole", code);   // luôn đồng bộ với DB
        if (code == AdminRoles.SuperAdmin || allowedRoles.Contains(code)) return;

        context.Result = new ContentResult
        {
            StatusCode = 403,
            ContentType = "text/html; charset=utf-8",
            Content = "<div style='font-family:Arial;padding:40px'><h3>Bạn không có quyền truy cập chức năng này.</h3><a href='/Admin'>Quay lại trang làm việc</a></div>"
        };
    }
}