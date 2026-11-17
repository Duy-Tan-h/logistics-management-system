using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebVanChuyenLogicstic.Data;
using WebVanChuyenLogicstic.Filters;
using WebVanChuyenLogicstic.Helpers;
using WebVanChuyenLogicstic.Models;

namespace WebVanChuyenLogicstic.Controllers;

[Route("Admin/Driver/{action=Index}")]
[AdminOnly(AdminRoles.Dispatcher)]
public class DriverController(AppDbContext db) : Controller
{
    public IActionResult Index(int? page) //Hiện danh sách tài xế.
    {
        var drivers = db.Drivers.AsNoTracking().OrderBy(d => d.FullName).ToPagedList(page);
        return View("~/Views/Admin/Driver/Index.cshtml", drivers);
    }

    [HttpGet]
    public IActionResult Create() => View("~/Views/Admin/Driver/Create.cshtml", new Driver());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Driver model)
    {
        model.LicenseNumber = (model.LicenseNumber ?? "").Trim().ToUpperInvariant();
        if (db.Drivers.Any(d => d.LicenseNumber == model.LicenseNumber))
            ModelState.AddModelError(nameof(model.LicenseNumber), "Số bằng lái này đã tồn tại.");
        if (!ModelState.IsValid)
            return View("~/Views/Admin/Driver/Create.cshtml", model);

        db.Drivers.Add(model);
        db.SaveChanges();
        TempData["SuccessMessage"] = "Đã thêm tài xế mới.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("{id}")]
    public IActionResult Edit(int id)
    {
        var driver = db.Drivers.AsNoTracking().FirstOrDefault(d => d.Id == id);
        return driver is null ? NotFound() : View("~/Views/Admin/Driver/Edit.cshtml", driver);
    }

    [HttpPost("{id}")]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Driver model)
    {
        if (id != model.Id) return BadRequest();
        model.LicenseNumber = (model.LicenseNumber ?? "").Trim().ToUpperInvariant();
        if (db.Drivers.Any(d => d.LicenseNumber == model.LicenseNumber && d.Id != model.Id))
            ModelState.AddModelError(nameof(model.LicenseNumber), "Số bằng lái này đã tồn tại.");
        if (!ModelState.IsValid)
            return View("~/Views/Admin/Driver/Edit.cshtml", model);

        db.Drivers.Update(model);
        db.SaveChanges();
        TempData["SuccessMessage"] = "Đã lưu thay đổi.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("{id}")]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        if (db.Orders.Any(o => o.DriverId == id) || db.Trips.Any(t => t.DriverId == id))
        {
            TempData["ErrorMessage"] = "Tài xế này đã được phân công nên không xóa được. Hãy đổi trạng thái sang nghỉ.";
            return RedirectToAction(nameof(Index));
        }
        var driver = db.Drivers.FirstOrDefault(d => d.Id == id);
        if (driver != null)
        {
            db.Drivers.Remove(driver);
            db.SaveChanges();
            TempData["SuccessMessage"] = "Đã xóa tài xế.";
        }
        return RedirectToAction(nameof(Index));
    }
}