using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebVanChuyenLogicstic.Data;

namespace WebVanChuyenLogicstic.Controllers;

public class NewsController(AppDbContext db) : Controller
{
    public IActionResult Index(string? category, int page = 1)
    {
        const int PageSize = 9;
        var published = db.Articles.AsNoTracking().Where(a => a.IsPublished);

        ViewBag.Categories = published.Select(a => a.Category).Distinct().OrderBy(c => c).ToList();

        var q = published;
        if (!string.IsNullOrEmpty(category)) q = q.Where(a => a.Category == category);

        var totalPages = Math.Max(1, (int)Math.Ceiling(q.Count() / (double)PageSize));
        page = Math.Clamp(page, 1, totalPages);

        var items = q.OrderByDescending(a => a.IsFeatured)                       // nổi bật lên trước
                     .ThenBy(a => a.IsFeatured ? a.FeaturedOrder : 0)
                     .ThenByDescending(a => a.PublishedAt)                       // mới nhất trước
                     .Skip((page - 1) * PageSize).Take(PageSize)
                     .ToList();

        ViewBag.Category = category;
        ViewBag.Page = page;
        ViewBag.TotalPages = totalPages;
        return View(items);
    }

    public IActionResult Details(int id)
    {
        var article = db.Articles.AsNoTracking()
            .Include(a => a.Blocks.OrderBy(b => b.DisplayOrder))
            .FirstOrDefault(a => a.Id == id && a.IsPublished);
        return article is null ? NotFound() : View(article);
    }
}