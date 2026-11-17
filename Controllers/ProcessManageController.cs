using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebVanChuyenLogicstic.Data;
using WebVanChuyenLogicstic.Filters;
using WebVanChuyenLogicstic.Helpers;
using WebVanChuyenLogicstic.Models;

namespace WebVanChuyenLogicstic.Controllers;

[Route("Admin/Process/{action=Index}")]
[AdminOnly(AdminRoles.Editor)]
public class ProcessManageController(AppDbContext db) : Controller
{
    private const string Dir = "~/Views/Admin/Process/";

    public IActionResult Index(int? page) => //danh sách
        View(Dir + "Index.cshtml", db.ProcessSteps.AsNoTracking().OrderBy(p => p.Number).ThenBy(p => p.Id).ToPagedList(page));

    [HttpGet]
    public IActionResult Create() =>
        View(Dir + "Create.cshtml", new ProcessStep { Number = (db.ProcessSteps.Max(p => (int?)p.Number) ?? 0) + 1 });// lấy số thứ tự lớn nhất hiện có, cộng 1, điền sẵn vào form

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(ProcessStep model)
    {
        Normalize(model);
        if (!ModelState.IsValid) return View(Dir + "Create.cshtml", model);

        model.Id = 0;
        db.ProcessSteps.Add(model);
        db.SaveChanges();
        Resequence(model.Id);
        TempData["SuccessMessage"] = "Đã thêm bước mới.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("{id}")]
    public IActionResult Edit(int id)
    {
        var s = db.ProcessSteps.AsNoTracking().FirstOrDefault(x => x.Id == id);
        return s is null ? NotFound() : View(Dir + "Edit.cshtml", s);
    }

    [HttpPost("{id}")]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, ProcessStep model)
    {
        if (id != model.Id) return BadRequest();
        Normalize(model);
        if (!ModelState.IsValid) return View(Dir + "Edit.cshtml", model);

        var step = db.ProcessSteps.FirstOrDefault(x => x.Id == id);
        if (step is null) return NotFound();
        step.Number = model.Number;
        step.Title = model.Title;
        step.Description = model.Description;
        step.Icon = model.Icon;
        db.SaveChanges();
        Resequence(id);

        TempData["SuccessMessage"] = "Đã lưu thay đổi.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("{id}")]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        var step = db.ProcessSteps.FirstOrDefault(x => x.Id == id);
        if (step != null)
        {
            db.ProcessSteps.Remove(step);
            db.SaveChanges();
            Resequence(null);
            TempData["SuccessMessage"] = "Đã xóa bước.";
        }
        return RedirectToAction(nameof(Index));
    }

    // Đánh lại số thứ tự 1..n; bước vừa thêm/sửa được xếp trước bước cũ trùng số
    private void Resequence(int? movedId)
    {
        var steps = db.ProcessSteps.ToList()
            .OrderBy(p => p.Number).ThenBy(p => p.Id == movedId ? 0 : 1).ThenBy(p => p.Id).ToList();
        for (var i = 0; i < steps.Count; i++) steps[i].Number = i + 1;
        db.SaveChanges();
    }

    private static void Normalize(ProcessStep m)
    {
        m.Title = (m.Title ?? "").Trim();
        m.Description = (m.Description ?? "").Trim();
        m.Icon = (m.Icon ?? "").Trim();
    }
}