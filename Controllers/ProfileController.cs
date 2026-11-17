using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebVanChuyenLogicstic.Data;
using WebVanChuyenLogicstic.Filters;
using WebVanChuyenLogicstic.Models;
using System.Text.RegularExpressions;

namespace WebVanChuyenLogicstic.Controllers;

[LoginRequired]
[Route("ho-so")]
public class ProfileController(AppDbContext db) : Controller
{
    private const int MaxAddresses = 10;
    private int UserId => HttpContext.Session.GetInt32("UserId")!.Value;
    private static readonly Regex PhoneRx = new(@"^(0|\+84)[0-9]{9,10}$");

    private static string? ValidateAddress(string? label, string? address, string? contactName, string? phone)
    {
        if (string.IsNullOrEmpty(label) || string.IsNullOrEmpty(address))
            return "Vui lòng nhập tên gợi nhớ và địa chỉ.";
        if (label.Length > 60 || address.Length > 300)
            return "Tên gợi nhớ tối đa 60 ký tự, địa chỉ tối đa 300 ký tự.";
        if (contactName is { Length: > 100 })
            return "Tên liên hệ tối đa 100 ký tự.";
        if (phone != null && !PhoneRx.IsMatch(phone))
            return "Số điện thoại không hợp lệ (ví dụ 0901234567).";
        return null;
    }

    private static string? Clean(string? s) => string.IsNullOrWhiteSpace(s) ? null : s.Trim();

    [HttpGet("")]
    public IActionResult Index()
    {
        var vm = Build(null, null);
        return vm is null ? RedirectToAction("Login", "Account") : View(vm);
    }

    [HttpPost("cap-nhat")]
    [ValidateAntiForgeryToken]
    public IActionResult Update(ProfileViewModel model)
    {
        var user = db.Users.FirstOrDefault(u => u.Id == UserId);
        if (user is null) return RedirectToAction("Login", "Account");

        if (!ModelState.IsValid)
            return View("Index", Build(model.FullName, model.Phone));

        user.FullName = model.FullName.Trim();
        user.Phone = model.Phone.Trim();
        db.SaveChanges();
        HttpContext.Session.SetString("UserName", user.FullName);   // cập nhật tên hiển thị trên menu

        TempData["Msg"] = "Đã cập nhật hồ sơ.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("dia-chi")]
    [ValidateAntiForgeryToken]
    public IActionResult AddAddress(string? label, string? address, string? contactName, string? phone)
    {
        label = Clean(label); address = Clean(address); contactName = Clean(contactName); phone = Clean(phone);

        var err = ValidateAddress(label, address, contactName, phone);
        if (err is null && db.SavedAddresses.Count(a => a.UserId == UserId) >= MaxAddresses)
            err = $"Mỗi tài khoản lưu tối đa {MaxAddresses} địa chỉ.";

        if (err != null) TempData["Err"] = err;
        else
        {
            db.SavedAddresses.Add(new SavedAddress
            {
                UserId = UserId,
                Label = label!,
                Address = address!,
                ContactName = contactName,
                Phone = phone
            });
            db.SaveChanges();
            TempData["Msg"] = "Đã thêm địa chỉ.";
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("dia-chi/{id}/sua")]
    [ValidateAntiForgeryToken]
    public IActionResult EditAddress(int id, string? label, string? address, string? contactName, string? phone)
    {
        var a = db.SavedAddresses.FirstOrDefault(x => x.Id == id && x.UserId == UserId);   // chỉ sửa được địa chỉ của chính mình
        if (a is null) return RedirectToAction(nameof(Index));

        label = Clean(label); address = Clean(address); contactName = Clean(contactName); phone = Clean(phone);
        var err = ValidateAddress(label, address, contactName, phone);
        if (err != null) { TempData["Err"] = err; return RedirectToAction(nameof(Index)); }

        a.Label = label!; a.Address = address!; a.ContactName = contactName; a.Phone = phone;
        db.SaveChanges();
        TempData["Msg"] = "Đã cập nhật địa chỉ.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("dia-chi/{id}/xoa")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteAddress(int id)
    {
        var a = db.SavedAddresses.FirstOrDefault(x => x.Id == id && x.UserId == UserId);   // chỉ xóa được địa chỉ của chính mình
        if (a != null)
        {
            db.SavedAddresses.Remove(a);
            db.SaveChanges();
            TempData["Msg"] = "Đã xóa địa chỉ.";
        }
        return RedirectToAction(nameof(Index));
    }

    private ProfileViewModel? Build(string? fullName, string? phone)
    {
        var user = db.Users.AsNoTracking().FirstOrDefault(u => u.Id == UserId);
        if (user is null) return null;
        return new ProfileViewModel
        {
            Email = user.Email,
            FullName = fullName ?? user.FullName,
            Phone = phone ?? user.Phone,
            Addresses = db.SavedAddresses.AsNoTracking().Where(a => a.UserId == user.Id).OrderBy(a => a.Label).ToList()
        };
    }
}