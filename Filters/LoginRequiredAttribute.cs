using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using WebVanChuyenLogicstic.Data;

namespace WebVanChuyenLogicstic.Filters;

public class LoginRequiredAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var http = context.HttpContext;
        if (http.Session.GetInt32("UserId") is int uid)
        {
            var db = http.RequestServices.GetRequiredService<AppDbContext>();
            if (db.Users.Any(u => u.Id == uid && u.IsActive)) return;
            http.Session.Clear();   // tài khoản bị khóa hoặc đã xóa
        }

        var returnUrl = http.Request.Path + http.Request.QueryString;
        context.Result = new RedirectToActionResult("Login", "Account", new { returnUrl });
    }
}