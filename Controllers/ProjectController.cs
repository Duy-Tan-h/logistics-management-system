using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebVanChuyenLogicstic.Data;
using WebVanChuyenLogicstic.Models;

namespace WebVanChuyenLogicstic.Controllers;

public class ProjectController(AppDbContext db) : Controller
{
    public IActionResult Index()
    {
        var items = db.HomeSectionItems.AsNoTracking()
            .Include(i => i.Section)
            .Where(i => i.Section!.SectionType == HomeSectionTypes.Projects && i.IsVisible)
            .OrderBy(i => i.DisplayOrder)
            .ToList();
        return View(items);
    }
}