using Microsoft.EntityFrameworkCore;
namespace WebVanChuyenLogicstic
{
    public class Program
    {
        public static void Main(string[] args)
        {
            System.Globalization.CultureInfo.DefaultThreadCurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            // Đăng ký các lớp theo kiến trúc Repository - Service - Controller.
            builder.Services.AddSingleton<Data.IContentRepository, Data.InMemoryContentRepository>();
            builder.Services.AddScoped<Data.IUserRepository, Data.EfUserRepository>();
            builder.Services.AddScoped<Services.IContentService, Services.ContentService>();
            builder.Services.AddScoped<Services.IAccountService, Services.AccountService>();
            builder.Services.AddScoped<Services.IHomeService, Services.HomeService>();
            builder.Services.AddScoped<Services.IAdminHomeService, Services.AdminHomeService>();
            builder.Services.Configure<Services.EmailSettings>(builder.Configuration.GetSection("Email"));
            builder.Services.AddScoped<Services.IEmailService, Services.EmailService>();
            builder.Services.AddScoped<Services.IOrderNotifier, Services.OrderNotifier>();
            builder.Services.AddDbContext<Data.AppDbContext>
                (options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<Data.AppDbContext>();
                db.Database.Migrate();
                Data.DbSeeder.Seed(db);
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
