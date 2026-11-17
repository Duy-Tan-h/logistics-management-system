using Microsoft.EntityFrameworkCore;
using WebVanChuyenLogicstic.Models;

namespace WebVanChuyenLogicstic.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    // Mỗi DbSet = 1 bảng trong database
    public DbSet<TransportService> TransportServices => Set<TransportService>();
    public DbSet<ProcessStep> ProcessSteps => Set<ProcessStep>();
    public DbSet<UserAccount> Users => Set<UserAccount>();
    public DbSet<Article> Articles => Set<Article>();
    public DbSet<ArticleBlock> ArticleBlocks => Set<ArticleBlock>();
    public DbSet<ContactMessage> Contacts => Set<ContactMessage>();
    public DbSet<NewsletterSubscriber> NewsletterSubscribers => Set<NewsletterSubscriber>();
    public DbSet<HomeSection> HomeSections => Set<HomeSection>();
    public DbSet<HomeSectionItem> HomeSectionItems => Set<HomeSectionItem>();
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<Driver> Drivers => Set<Driver>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderEvent> OrderEvents => Set<OrderEvent>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<PasswordResetRequest> PasswordResetRequests => Set<PasswordResetRequest>();
    public DbSet<SavedAddress> SavedAddresses => Set<SavedAddress>();
    public DbSet<CargoType> CargoTypes => Set<CargoType>();
    public DbSet<SiteSetting> SiteSettings => Set<SiteSetting>();
    public DbSet<SeoPage> SeoPages => Set<SeoPage>();
    public DbSet<StaticPage> StaticPages => Set<StaticPage>();
    public DbSet<MediaItem> MediaItems => Set<MediaItem>();
    public DbSet<AboutItem> AboutItems => Set<AboutItem>();
    public DbSet<Trip> Trips => Set<Trip>();
    public DbSet<TripOrder> TripOrders => Set<TripOrder>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<AdminUser> AdminUsers => Set<AdminUser>();
    // Cấu hình chi tiết: tên bảng, độ dài cột, quan hệ giữa các bảng
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TransportService>(e =>
        {
            e.ToTable("Services");
            e.Property(x => x.Name).HasMaxLength(150);
            e.Property(x => x.Summary).HasMaxLength(500);
            e.Property(x => x.Icon).HasMaxLength(60);
            e.Property(x => x.DeliveryTime).HasMaxLength(100);
            e.Property(x => x.PriceNote).HasMaxLength(150);
            e.Property(x => x.ImageUrl).HasMaxLength(300);
        });

        modelBuilder.Entity<ProcessStep>(e =>
        {
            e.ToTable("ProcessSteps");
            e.Property(x => x.Title).HasMaxLength(150);
            e.Property(x => x.Description).HasMaxLength(500);
            e.Property(x => x.Icon).HasMaxLength(60);
        });

        modelBuilder.Entity<UserAccount>(e =>
        {
            e.ToTable("Users");
            e.Property(x => x.FullName).HasMaxLength(150);
            e.Property(x => x.Email).HasMaxLength(200);
            e.Property(x => x.Phone).HasMaxLength(30);
            e.Property(x => x.PasswordHash).HasMaxLength(200);
            e.Property(x => x.PasswordSalt).HasMaxLength(100);
            e.Property(x => x.Role).HasMaxLength(30);
            e.HasIndex(x => x.Email).IsUnique();          // không cho trùng email
        });

        modelBuilder.Entity<Article>(e =>
        {
            e.ToTable("Articles");
            e.Property(x => x.Icon).HasMaxLength(60);
            e.Property(x => x.Category).HasMaxLength(100);
            e.Property(x => x.Title).HasMaxLength(250);
            e.Property(x => x.Summary).HasMaxLength(500);
            e.Property(x => x.ArtClass).HasMaxLength(50);
            e.HasMany(x => x.Blocks).WithOne(b => b.Article)
                .HasForeignKey(b => b.ArticleId)
                .OnDelete(DeleteBehavior.Cascade);        // xóa bài => xóa luôn các đoạn
        });

        modelBuilder.Entity<ArticleBlock>(e =>
        {
            e.ToTable("ArticleBlocks");
            e.Property(x => x.Heading).HasMaxLength(250);
        });

        modelBuilder.Entity<ContactMessage>(e =>
        {
            e.ToTable("Contacts");
            e.Property(x => x.FullName).HasMaxLength(150);
            e.Property(x => x.PhoneNumber).HasMaxLength(30);
            e.Property(x => x.Email).HasMaxLength(200);
            e.Property(x => x.Message).HasMaxLength(2000);
        });

        modelBuilder.Entity<NewsletterSubscriber>(e =>
        {
            e.ToTable("NewsletterSubscribers");
            e.Property(x => x.Email).HasMaxLength(200);
            e.HasIndex(x => x.Email).IsUnique();
        });

        modelBuilder.Entity<HomeSection>(e =>
        {
            e.ToTable("HomeSections");
            e.Property(x => x.SectionType).HasMaxLength(50);
            e.Property(x => x.Kicker).HasMaxLength(100);
            e.Property(x => x.Title).HasMaxLength(250);
            e.Property(x => x.Subtitle).HasMaxLength(500);
            e.Property(x => x.ButtonText).HasMaxLength(100);
            e.Property(x => x.ButtonUrl).HasMaxLength(300);
            e.Property(x => x.SecondaryButtonText).HasMaxLength(100);
            e.Property(x => x.SecondaryButtonUrl).HasMaxLength(300);
            e.HasMany(x => x.Items).WithOne(i => i.Section)
                .HasForeignKey(i => i.SectionId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<HomeSectionItem>(e =>
        {
            e.ToTable("HomeSectionItems");
            e.Property(x => x.Title).HasMaxLength(250);
            e.Property(x => x.Description).HasMaxLength(1000);
            e.Property(x => x.Icon).HasMaxLength(60);
            e.Property(x => x.ImageUrl).HasMaxLength(300);
            e.Property(x => x.LinkUrl).HasMaxLength(300);
            e.Property(x => x.Value).HasMaxLength(50);
        });
        modelBuilder.Entity<Vehicle>(e =>
        {
            e.ToTable("Vehicles");
            e.Property(x => x.PlateNumber).HasMaxLength(20);
            e.Property(x => x.VehicleType).HasMaxLength(50);
            e.Property(x => x.Status).HasMaxLength(30);
            e.Property(x => x.Note).HasMaxLength(500);
            e.HasIndex(x => x.PlateNumber).IsUnique();   // không cho trùng biển số
        });

        modelBuilder.Entity<Driver>(e =>
        {
            e.ToTable("Drivers");
            e.Property(x => x.FullName).HasMaxLength(150);
            e.Property(x => x.Phone).HasMaxLength(30);
            e.Property(x => x.LicenseNumber).HasMaxLength(50);
            e.Property(x => x.Status).HasMaxLength(30);
            e.Property(x => x.Note).HasMaxLength(500);
            e.HasIndex(x => x.LicenseNumber).IsUnique();   // không cho trùng số bằng lái
        });
        modelBuilder.Entity<Order>(e =>
        {
            e.ToTable("Orders");
            e.Property(x => x.OrderCode).HasMaxLength(50);
            e.Property(x => x.CustomerName).HasMaxLength(150);
            e.Property(x => x.Phone).HasMaxLength(30);
            e.Property(x => x.Email).HasMaxLength(200);
            e.Property(x => x.PickupAddress).HasMaxLength(300);
            e.Property(x => x.DeliveryAddress).HasMaxLength(300);
            e.Property(x => x.CargoType).HasMaxLength(150);
            e.Property(x => x.WeightKg).HasPrecision(18, 2);
            e.Property(x => x.Status).HasMaxLength(30);
            e.Property(x => x.Note).HasMaxLength(1000);
            e.Property(x => x.QuotedPrice).HasPrecision(18, 0);
            e.Property(x => x.QuoteNote).HasMaxLength(1000);
            e.Property(x => x.QuoteToken).HasMaxLength(64);
            e.HasIndex(x => x.QuoteToken).IsUnique().HasFilter("[QuoteToken] IS NOT NULL");
            e.HasIndex(x => x.OrderCode).IsUnique();

            // Xóa xe / tài xế / dịch vụ / tài khoản thì đơn cũ vẫn giữ, chỉ bỏ liên kết
            e.HasOne(x => x.Customer).WithMany().HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.SetNull);
            e.HasOne(x => x.Service).WithMany().HasForeignKey(x => x.ServiceId).OnDelete(DeleteBehavior.SetNull);
            e.HasOne(x => x.Vehicle).WithMany().HasForeignKey(x => x.VehicleId).OnDelete(DeleteBehavior.SetNull);
            e.HasOne(x => x.Driver).WithMany().HasForeignKey(x => x.DriverId).OnDelete(DeleteBehavior.SetNull);
            e.HasMany(x => x.Events).WithOne(ev => ev.Order)
                .HasForeignKey(ev => ev.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
            e.HasMany(x => x.Items).WithOne(i => i.Order)
                .HasForeignKey(i => i.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<OrderEvent>(e =>
        {
            e.ToTable("OrderEvents");
            e.Property(x => x.Title).HasMaxLength(150);
            e.Property(x => x.Detail).HasMaxLength(500);
        });
        modelBuilder.Entity<OrderItem>(e =>
        {
            e.ToTable("OrderItems");
            e.Property(x => x.CargoTypeName).HasMaxLength(100);
            e.Property(x => x.WeightKg).HasPrecision(18, 2);
            e.Property(x => x.Note).HasMaxLength(300);
            e.HasOne(x => x.CargoType).WithMany().HasForeignKey(x => x.CargoTypeId)
                .OnDelete(DeleteBehavior.SetNull);   // xóa loại hàng thì dòng hàng cũ vẫn giữ
        });
        modelBuilder.Entity<PasswordResetRequest>(e =>
        {
            e.ToTable("PasswordResetRequests");
            e.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);   // xóa khách thì xóa luôn yêu cầu của họ
        });
        modelBuilder.Entity<SavedAddress>(e =>
        {
            e.ToTable("SavedAddresses");
            e.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);   // xóa khách thì xóa luôn danh bạ
        });
        modelBuilder.Entity<CargoType>(e =>
        {
            e.ToTable("CargoTypes");
            e.Property(x => x.Name).HasMaxLength(100);
            e.Property(x => x.Description).HasMaxLength(300);
            e.HasIndex(x => x.Name).IsUnique();   // không cho trùng tên loại hàng
        });
        modelBuilder.Entity<SiteSetting>(e => e.ToTable("SiteSettings"));

        modelBuilder.Entity<SeoPage>(e =>
        {
            e.ToTable("SeoPages");
            e.HasIndex(x => x.Route).IsUnique();
        });

        modelBuilder.Entity<StaticPage>(e =>
        {
            e.ToTable("StaticPages");
            e.HasIndex(x => x.Slug).IsUnique();
        });

        modelBuilder.Entity<MediaItem>(e => e.ToTable("MediaItems"));

        modelBuilder.Entity<AboutItem>(e => e.ToTable("AboutItems"));

        modelBuilder.Entity<Trip>(e =>
        {
            e.ToTable("Trips");
            e.Property(x => x.Route).HasMaxLength(300);
            e.Property(x => x.Status).HasMaxLength(30);
            e.Property(x => x.Note).HasMaxLength(500);
            e.HasOne(x => x.Vehicle).WithMany().HasForeignKey(x => x.VehicleId).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(x => x.Driver).WithMany().HasForeignKey(x => x.DriverId).OnDelete(DeleteBehavior.Restrict);
            e.HasMany(x => x.Orders).WithOne(l => l.Trip).HasForeignKey(l => l.TripId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<TripOrder>(e =>
        {
            e.ToTable("TripOrders");
            e.HasOne(x => x.Order).WithMany().HasForeignKey(x => x.OrderId).OnDelete(DeleteBehavior.Cascade);
        });
        modelBuilder.Entity<Role>(e =>
        {
            e.ToTable("Roles");
            e.Property(x => x.Code).HasMaxLength(30);
            e.Property(x => x.Name).HasMaxLength(100);
            e.Property(x => x.Description).HasMaxLength(300);
            e.HasIndex(x => x.Code).IsUnique();
        });

        modelBuilder.Entity<AdminUser>(e =>
        {
            e.ToTable("AdminUsers");
            e.Property(x => x.FullName).HasMaxLength(150);
            e.Property(x => x.Email).HasMaxLength(200);
            e.Property(x => x.PasswordHash).HasMaxLength(200);
            e.Property(x => x.PasswordSalt).HasMaxLength(100);
            e.HasIndex(x => x.Email).IsUnique();
            e.HasOne(x => x.Role).WithMany().HasForeignKey(x => x.RoleId).OnDelete(DeleteBehavior.Restrict);
        });
    }
}