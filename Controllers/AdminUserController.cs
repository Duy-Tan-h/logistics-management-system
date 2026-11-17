using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebVanChuyenLogicstic.Data;
using WebVanChuyenLogicstic.Filters;
using WebVanChuyenLogicstic.Helpers;
using WebVanChuyenLogicstic.Models;
using WebVanChuyenLogicstic.Models.Admin;
using WebVanChuyenLogicstic.Services;

namespace WebVanChuyenLogicstic.Controllers;

[Route("Admin/Users/{action=Index}")]
[AdminOnly]   // chỉ SuperAdmin
public class AdminUserController(AppDbContext db) : Controller
{
    private const string Dir = "~/Views/Admin/Users/";
    private int? MyId => HttpContext.Session.GetInt32("AdminId");

    public IActionResult Index(int? page) =>
        View(Dir + "Index.cshtml",
            db.AdminUsers.AsNoTracking().Include(a => a.Role).OrderBy(a => a.FullName).ToPagedList(page));

    [HttpGet]
    public IActionResult Create()
    {
        FillRoles();
        return View(Dir + "Form.cshtml", new AdminUserFormViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(AdminUserFormViewModel m)
    {
        m.Email = (m.Email ?? "").Trim();
        if (string.IsNullOrWhiteSpace(m.Password))
            ModelState.AddModelError(nameof(m.Password), "Vui lòng nhập mật khẩu ban đầu.");
        CheckCommon(m);
        if (!ModelState.IsValid) { FillRoles(); return View(Dir + "Form.cshtml", m); }

        var (hash, salt) = PasswordHasher.Create(m.Password!);
        db.AdminUsers.Add(new AdminUser
        {
            FullName = m.FullName.Trim(),
            Email = m.Email,
            PasswordHash = hash,
            PasswordSalt = salt,
            RoleId = m.RoleId!.Value,
            IsActive = m.IsActive
        });
        db.SaveChanges();
        TempData["SuccessMessage"] = "Đã thêm tài khoản quản trị.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("{id}")]
    public IActionResult Edit(int id)
    {
        var a = db.AdminUsers.AsNoTracking().FirstOrDefault(x => x.Id == id);
        if (a is null) return NotFound();
        FillRoles();
        return View(Dir + "Form.cshtml", new AdminUserFormViewModel
        {
            Id = a.Id,
            FullName = a.FullName,
            Email = a.Email,
            RoleId = a.RoleId,
            IsActive = a.IsActive
        });
    }

    [HttpPost("{id}")]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, AdminUserFormViewModel m)
    {
        var a = db.AdminUsers.Include(x => x.Role).FirstOrDefault(x => x.Id == id);
        if (a is null) return NotFound();

        m.Id = id;
        m.Email = (m.Email ?? "").Trim();
        CheckCommon(m);

        // E3: không được khóa / hạ quyền SuperAdmin cuối cùng; không tự khóa mình
        var newRole = db.Roles.FirstOrDefault(r => r.Id == m.RoleId);
        var losesSuper = a.IsActive && a.Role!.Code == AdminRoles.SuperAdmin
            && (!m.IsActive || newRole?.Code != AdminRoles.SuperAdmin);
        if (losesSuper && IsLastSuperAdmin(id))
            ModelState.AddModelError(string.Empty, "Không thể khóa hoặc hạ quyền SuperAdmin cuối cùng.");
        if (id == MyId && !m.IsActive)
            ModelState.AddModelError(string.Empty, "Bạn không thể tự khóa tài khoản của mình.");

        if (!ModelState.IsValid) { FillRoles(); return View(Dir + "Form.cshtml", m); }

        a.FullName = m.FullName.Trim();
        a.Email = m.Email;
        a.RoleId = m.RoleId!.Value;
        a.IsActive = m.IsActive;
        if (!string.IsNullOrWhiteSpace(m.Password))
        {
            var (hash, salt) = PasswordHasher.Create(m.Password);
            a.PasswordHash = hash;
            a.PasswordSalt = salt;
        }
        db.SaveChanges();

        if (id == MyId) HttpContext.Session.SetString("UserName", a.FullName);
        TempData["SuccessMessage"] = "Đã lưu tài khoản.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("{id}")]
    [ValidateAntiForgeryToken]
    public IActionResult ToggleActive(int id)
    {
        var a = db.AdminUsers.Include(x => x.Role).FirstOrDefault(x => x.Id == id);
        if (a is null) return NotFound();

        if (a.IsActive)
        {
            if (id == MyId)
                TempData["ErrorMessage"] = "Bạn không thể tự khóa tài khoản của mình.";
            else if (a.Role!.Code == AdminRoles.SuperAdmin && IsLastSuperAdmin(id))
                TempData["ErrorMessage"] = "Không thể khóa SuperAdmin cuối cùng.";
            else
            {
                a.IsActive = false;
                db.SaveChanges();
                TempData["SuccessMessage"] = "Đã khóa tài khoản. Người này không đăng nhập được nữa.";
            }
        }
        else
        {
            a.IsActive = true;
            db.SaveChanges();
            TempData["SuccessMessage"] = "Đã mở khóa tài khoản.";
        }
        return RedirectToAction(nameof(Index));
    }

    // ---------- helper ----------

    private bool IsLastSuperAdmin(int id) =>
        !db.AdminUsers.Any(x => x.Id != id && x.IsActive && x.Role!.Code == AdminRoles.SuperAdmin);

    private void FillRoles() => ViewBag.Roles = db.Roles.AsNoTracking().OrderBy(r => r.Id).ToList();

    private void CheckCommon(AdminUserFormViewModel m)
    {
        if (!string.IsNullOrEmpty(m.Email)
            && (db.AdminUsers.Any(x => x.Email == m.Email && x.Id != m.Id) || db.Users.Any(u => u.Email == m.Email)))
            ModelState.AddModelError(nameof(m.Email), "Email này đã được sử dụng.");

        if (m.RoleId != null && !db.Roles.Any(r => r.Id == m.RoleId))
            ModelState.AddModelError(nameof(m.RoleId), "Vai trò không hợp lệ.");

        if (!string.IsNullOrWhiteSpace(m.Password) && m.Password.Length < 6)
            ModelState.AddModelError(nameof(m.Password), "Mật khẩu phải có ít nhất 6 ký tự.");
    }
}