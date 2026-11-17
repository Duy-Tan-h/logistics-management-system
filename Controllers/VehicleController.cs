using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebVanChuyenLogicstic.Data;
using WebVanChuyenLogicstic.Filters;
using WebVanChuyenLogicstic.Models;
using WebVanChuyenLogicstic.Helpers;

namespace WebVanChuyenLogicstic.Controllers;

// "{action=Index}" (khác "[action]" ArticleController đang dùng) cho phép
// /Admin/Vehicle KHÔNG kèm /Index vẫn tự vào đúng action Index — khớp với
// link có sẵn trong sidebar.
[Route("Admin/Vehicle/{action=Index}")]
[AdminOnly(AdminRoles.Dispatcher)]
public class VehicleController(AppDbContext db) : Controller
{
    public IActionResult Index(int? page) //Lấy xe theo biển số, hiện danh sách.
    {
        var vehicles = db.Vehicles.AsNoTracking().OrderBy(v => v.PlateNumber).ToPagedList(page);
        return View("~/Views/Admin/Vehicle/Index.cshtml", vehicles);
    }

    [HttpGet]
    public IActionResult Create() => View("~/Views/Admin/Vehicle/Create.cshtml", new Vehicle());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Vehicle model)
    {
        model.PlateNumber = (model.PlateNumber ?? "").Trim().ToUpperInvariant();
        if (db.Vehicles.Any(v => v.PlateNumber == model.PlateNumber))
            ModelState.AddModelError(nameof(model.PlateNumber), "Biển số này đã tồn tại.");

        if (!ModelState.IsValid)
            return View("~/Views/Admin/Vehicle/Create.cshtml", model);

        db.Vehicles.Add(model);
        db.SaveChanges();
        TempData["SuccessMessage"] = "Đã thêm phương tiện mới.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("{id}")]
    public IActionResult Edit(int id)
    {
        var vehicle = db.Vehicles.AsNoTracking().FirstOrDefault(v => v.Id == id);
        return vehicle is null ? NotFound() : View("~/Views/Admin/Vehicle/Edit.cshtml", vehicle);
    }

    [HttpPost("{id}")]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Vehicle model)
    {
        model.PlateNumber = (model.PlateNumber ?? "").Trim().ToUpperInvariant();
        if (db.Vehicles.Any(v => v.PlateNumber == model.PlateNumber && v.Id != model.Id)) //v.Id != model.Id (không tính trùng với chính nó)
            ModelState.AddModelError(nameof(model.PlateNumber), "Biển số này đã tồn tại.");

        if (id != model.Id) return BadRequest();
        if (!ModelState.IsValid)
            return View("~/Views/Admin/Vehicle/Edit.cshtml", model);

        db.Vehicles.Update(model);
        db.SaveChanges();
        TempData["SuccessMessage"] = "Đã lưu thay đổi.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("{id}")]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        if (db.Orders.Any(o => o.VehicleId == id) || db.Trips.Any(t => t.VehicleId == id))
        {
            TempData["ErrorMessage"] = "Xe này đã được gán cho đơn hàng nên không xóa được. Hãy đổi trạng thái sang không hoạt động.";
            return RedirectToAction(nameof(Index));
        }
        var vehicle = db.Vehicles.FirstOrDefault(v => v.Id == id);
        if (vehicle != null)
        {
            db.Vehicles.Remove(vehicle);
            db.SaveChanges();
            TempData["SuccessMessage"] = "Đã xóa phương tiện.";
        }
        return RedirectToAction(nameof(Index));
    }
}