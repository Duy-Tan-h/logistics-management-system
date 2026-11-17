using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebVanChuyenLogicstic.Data;
using WebVanChuyenLogicstic.Filters;
using WebVanChuyenLogicstic.Helpers;
using WebVanChuyenLogicstic.Models;

namespace WebVanChuyenLogicstic.Controllers;

[Route("Admin/Article/[action]")]
[AdminOnly(AdminRoles.Editor)]
public class ArticleController(AppDbContext db) : Controller
{
    private const string Dir = "~/Views/Admin/Article/";

    public IActionResult Index(string? q, string? category, string? status, int? page)
    {
        var query = db.Articles.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(q))
        {
            var term = q.Trim();
            query = query.Where(a => a.Title.Contains(term));
        }
        if (!string.IsNullOrEmpty(category)) query = query.Where(a => a.Category == category);
        if (status == "published") query = query.Where(a => a.IsPublished);
        else if (status == "draft") query = query.Where(a => !a.IsPublished);

        ViewBag.Q = q;
        ViewBag.Category = category;
        ViewBag.Status = status;
        return View(Dir + "Index.cshtml", query.OrderByDescending(a => a.PublishedAt).ToPagedList(page));
    }

    [HttpGet]
    public IActionResult Create() => View(Dir + "Create.cshtml", new Article());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Article model)
    {
        var blocks = CleanBlocks(model.Blocks);
        IgnoreBlockErrors();
        if (!ModelState.IsValid)
        {
            model.Blocks = blocks;
            return View(Dir + "Create.cshtml", model);
        }

        model.Id = 0;
        model.Blocks = blocks.Select(b => new ArticleBlock { Heading = b.Heading, Body = b.Body, DisplayOrder = b.DisplayOrder }).ToList();
        db.Articles.Add(model);
        db.SaveChanges();
        TempData["SuccessMessage"] = "Thêm bài viết mới thành công!";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("{id}")]
    public IActionResult Edit(int id)
    {
        var article = db.Articles.AsNoTracking()
            .Include(a => a.Blocks.OrderBy(b => b.DisplayOrder))
            .FirstOrDefault(a => a.Id == id);
        return article is null ? NotFound() : View(Dir + "Edit.cshtml", article);
    }

    [HttpPost("{id}")]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Article model)
    {
        if (id != model.Id) return BadRequest();

        var article = db.Articles.Include(a => a.Blocks).FirstOrDefault(a => a.Id == id);
        if (article is null) return NotFound();

        var blocks = CleanBlocks(model.Blocks);
        IgnoreBlockErrors();
        if (!ModelState.IsValid)
        {
            model.Blocks = blocks;
            return View(Dir + "Edit.cshtml", model);
        }

        article.Title = model.Title;
        article.Summary = model.Summary;
        article.Category = model.Category;
        article.Icon = model.Icon;
        article.ArtClass = model.ArtClass;
        article.ImageUrl = string.IsNullOrWhiteSpace(model.ImageUrl) ? null : model.ImageUrl.Trim();
        article.PublishedAt = model.PublishedAt;
        article.IsPublished = model.IsPublished;
        article.IsFeatured = model.IsFeatured;
        article.FeaturedOrder = model.FeaturedOrder;

        // Đồng bộ các đoạn nội dung: xóa đoạn bị gỡ, sửa đoạn cũ, thêm đoạn mới, đánh lại thứ tự
        foreach (var old in article.Blocks.Where(o => blocks.All(b => b.Id != o.Id)).ToList())
            db.ArticleBlocks.Remove(old);

        foreach (var b in blocks)
        {
            var existing = b.Id == 0 ? null : article.Blocks.FirstOrDefault(o => o.Id == b.Id);
            if (existing is null)
                article.Blocks.Add(new ArticleBlock { Heading = b.Heading, Body = b.Body, DisplayOrder = b.DisplayOrder });
            else
            {
                existing.Heading = b.Heading;
                existing.Body = b.Body;
                existing.DisplayOrder = b.DisplayOrder;
            }
        }

        db.SaveChanges();
        TempData["SuccessMessage"] = "Đã lưu bài viết.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("{id}")]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        var article = db.Articles.FirstOrDefault(a => a.Id == id);
        if (article is not null)
        {
            db.Articles.Remove(article); // Cascade đã khai báo -> xóa luôn các ArticleBlock liên quan
            db.SaveChanges();
            TempData["SuccessMessage"] = "Đã xóa bài viết.";
        }
        return RedirectToAction(nameof(Index));
    }

    // Bỏ đoạn trống, cắt khoảng trắng, đánh số thứ tự theo vị trí trên form
    private List<ArticleBlock> CleanBlocks(List<ArticleBlock>? raw)
    {
        var list = (raw ?? new List<ArticleBlock>())
            .Where(b => !string.IsNullOrWhiteSpace(b.Heading) || !string.IsNullOrWhiteSpace(b.Body))
            .Select((b, i) => new ArticleBlock
            {
                Id = b.Id,
                Heading = (b.Heading ?? "").Trim(),
                Body = (b.Body ?? "").Trim(),
                DisplayOrder = i + 1
            })
            .ToList();

        if (list.Any(b => b.Heading.Length > 250))
            ModelState.AddModelError(string.Empty, "Tiêu đề mỗi đoạn tối đa 250 ký tự.");
        return list;
    }

    // Các đoạn tự kiểm tra ở CleanBlocks, nên bỏ lỗi "required" mặc định của framework cho ArticleBlock
    private void IgnoreBlockErrors()
    {
        foreach (var key in ModelState.Keys.Where(k => k.StartsWith("Blocks")).ToList())
            ModelState.Remove(key);
    }
}