using Microsoft.AspNetCore.Mvc;
using WebVanChuyenLogicstic.Filters;
using WebVanChuyenLogicstic.Models;
using WebVanChuyenLogicstic.Models.Admin;
using WebVanChuyenLogicstic.Services;
using WebVanChuyenLogicstic.Helpers;
namespace WebVanChuyenLogicstic.Controllers;

[AdminOnly(AdminRoles.Editor)]
[Route("Admin/HomeContent/[action]/{id?}")]
public class HomeContentController(IAdminHomeService service) : Controller
{
    private const string ViewDir = "~/Views/Admin/HomeContent/";

    [HttpGet]
    public IActionResult Index(int? page) => View(ViewDir + "Index.cshtml", service.GetSections().ToPagedList(page));

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult ToggleSection(int id)
    {
        service.ToggleSection(id);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult ReorderSections(List<ReorderItemViewModel> sections)
    {
        service.ReorderSections(sections.Select(x => (x.Id, x.Order)).ToList());
        TempData["SuccessMessage"] = "Đã lưu thứ tự mới.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult EditSection(int id)
    {
        var vm = service.GetSectionForEdit(id);
        return vm is null ? NotFound() : View(ViewDir + "EditSection.cshtml", vm);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult EditSection(int id, HomeSectionEditViewModel model)
    {
        if (id != model.Id) return BadRequest();
        if (!ModelState.IsValid)
        {
            service.FillDisplayInfo(model);
            return View(ViewDir + "EditSection.cshtml", model);
        }
        if (!service.UpdateSection(model)) return NotFound();
        TempData["SuccessMessage"] = "Đã lưu.";
        return RedirectToAction(nameof(EditSection), new { id });
    }

    [HttpGet]
    public IActionResult CreateSection()
    {
        ViewBag.Types = service.GetCreatableTypes();
        return View(ViewDir + "CreateSection.cshtml", new HomeSectionCreateViewModel());
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult CreateSection(HomeSectionCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Types = service.GetCreatableTypes();
            return View(ViewDir + "CreateSection.cshtml", model);
        }
        var newId = service.CreateSection(model);
        TempData["SuccessMessage"] = "Đã tạo section mới.";
        return RedirectToAction(nameof(EditSection), new { id = newId });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult DeleteSection(int id)
    {
        var deleted = service.DeleteSection(id);
        TempData[deleted ? "SuccessMessage" : "ErrorMessage"] = deleted ? "Đã xóa section." : "Không thể xóa section này.";
        return RedirectToAction(nameof(Index));
    }

    // ---------- Mục con ----------
    [HttpGet]
    public IActionResult CreateItem(int sectionId)
    {
        var vm = service.GetNewItem(sectionId);
        return vm is null ? NotFound() : View(ViewDir + "ItemForm.cshtml", vm);
    }

    [HttpGet]
    public IActionResult EditItem(int id)
    {
        var vm = service.GetItemForEdit(id);
        return vm is null ? NotFound() : View(ViewDir + "ItemForm.cshtml", vm);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult SaveItem(HomeSectionItemEditViewModel model)
    {
        if (!ModelState.IsValid)
        {
            service.FillItemDisplayInfo(model);
            return View(ViewDir + "ItemForm.cshtml", model);
        }
        var sectionId = service.SaveItem(model);
        if (sectionId is null) return NotFound();
        TempData["SuccessMessage"] = model.Id == 0 ? "Đã thêm mục mới." : "Đã lưu mục.";
        return RedirectToAction(nameof(EditSection), new { id = sectionId });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult ToggleItem(int id)
    {
        var sectionId = service.ToggleItem(id);
        return sectionId is null ? NotFound() : RedirectToAction(nameof(EditSection), new { id = sectionId });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult MoveItem(int id, int direction)
    {
        var sectionId = service.MoveItem(id, direction);
        return sectionId is null ? NotFound() : RedirectToAction(nameof(EditSection), new { id = sectionId });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult DeleteItem(int id)
    {
        var sectionId = service.DeleteItem(id);
        if (sectionId is null) return NotFound();
        TempData["SuccessMessage"] = "Đã xóa mục.";
        return RedirectToAction(nameof(EditSection), new { id = sectionId });
    }
}