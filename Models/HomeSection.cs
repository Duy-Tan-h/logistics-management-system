namespace WebVanChuyenLogicstic.Models
{
    public static class HomeSectionTypes
    {
        public const string Hero = "Hero";
        public const string Tracking = "Tracking";
        public const string ServiceIntro = "ServiceIntro";
        public const string WhyChooseUs = "WhyChooseUs";
        public const string Projects = "Projects";
        public const string Process = "Process";
        public const string News = "News";
        public const string Contact = "Contact";
        public const string Cta = "Cta";
        public const string Partners = "Partners";
        public const string Banner = "Banner";
    }
    public class HomeSection
    {
        public bool IsSystem { get; set; } = true;
        public int Id { get; set; }
        public string SectionType { get; set; } = string.Empty;

        public string? Kicker { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Subtitle { get; set; }
        public string? Body { get; set; }

        public string? ButtonText { get; set; }
        public string? ButtonUrl { get; set; }
        public string? SecondaryButtonText { get; set; }
        public string? SecondaryButtonUrl { get; set; }

        public int DisplayOrder { get; set; }          // nhỏ đứng trước
        public bool IsVisible { get; set; } = true;

        public List<HomeSectionItem> Items { get; set; } = [];
    } 
    public class HomeSectionItem
    {
        public int Id { get; set; }
        public int SectionId { get; set; }
        public HomeSection? Section { get; set; }

        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Icon { get; set; }
        public string? ImageUrl { get; set; }
        public string? LinkUrl { get; set; }
        public string? Value { get; set; }             // dùng cho số liệu, ví dụ "98%"
        public int DisplayOrder { get; set; }
        public bool IsVisible { get; set; } = true;
    }
}
