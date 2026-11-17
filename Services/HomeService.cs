using Microsoft.EntityFrameworkCore;
using WebVanChuyenLogicstic.Data;
using WebVanChuyenLogicstic.Models;

namespace WebVanChuyenLogicstic.Services
{
    public class HomeService(AppDbContext db) : IHomeService
    {
        private const int MaxFeaturedServices = 6;   // tối đa số thẻ dịch vụ nổi bật ở trang chủ
        private const int MaxFeaturedArticles = 3;       // số bài tin tức mới nhất

        public HomeViewModel GetHomePage() => new()
        {
            // Section đang bật, sắp theo DisplayOrder, kèm các mục con đang bật
            Sections = db.HomeSections.AsNoTracking()
                .Where(s => s.IsVisible)
                .Include(s => s.Items.Where(i => i.IsVisible).OrderBy(i => i.DisplayOrder))
                .OrderBy(s => s.DisplayOrder)
                .ToList(),

            // Dịch vụ được tích "Nổi bật", theo FeaturedOrder
            FeaturedServices = db.TransportServices.AsNoTracking()
                .Where(s => s.IsFeatured && s.IsVisible)
                .OrderBy(s => s.FeaturedOrder).ThenBy(s => s.Id)
                .Take(MaxFeaturedServices)
                .ToList(),

            ProcessSteps = db.ProcessSteps.AsNoTracking()
                .OrderBy(p => p.Number)
                .ToList(),

            FeaturedArticles = db.Articles.AsNoTracking()
            .Where(a => a.IsPublished && a.IsFeatured)
            .OrderBy(a => a.FeaturedOrder).ThenByDescending(a => a.PublishedAt)
            .Take(MaxFeaturedArticles)
            .ToList()
        };
    }
}
