using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebVanChuyenLogicstic.Data;

namespace WebVanChuyenLogicstic.Controllers;

public class ProcessController(AppDbContext db) : Controller
{
    public IActionResult Index() =>
        View(db.ProcessSteps.AsNoTracking().OrderBy(p => p.Number).ToList());
}