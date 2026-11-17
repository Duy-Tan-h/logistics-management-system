namespace WebVanChuyenLogicstic.Models;

public class HomeViewModel
{
    public IReadOnlyList<HomeSection> Sections { get; set; } = [];
    public IReadOnlyList<TransportService> FeaturedServices { get; set; } = [];
    public IReadOnlyList<ProcessStep> ProcessSteps { get; set; } = [];
    public IReadOnlyList<Article> FeaturedArticles { get; set; } = [];
}
// Gói "section cần vẽ" cùng "dữ liệu cả trang" để truyền vào từng partial view
public record HomeSectionContext(HomeSection Section, HomeViewModel Page);
