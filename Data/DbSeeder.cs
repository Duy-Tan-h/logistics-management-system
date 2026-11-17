using System.Globalization;
using WebVanChuyenLogicstic.Models;

namespace WebVanChuyenLogicstic.Data;

public static class DbSeeder
{
    public static void Seed(AppDbContext db)
    {
        SeedServices(db);
        SeedProcessSteps(db);
        SeedCargoTypes(db);
        SeedHomeSections(db);
        EnsurePartnersSection(db);
        EnsureBannerSection(db);
        SeedRoles(db);
        SeedAdmin(db);
        SeedSiteSetting(db);
        SeedSeoPages(db);
        SeedStaticPages(db);
        SeedAboutItems(db);
    }
    private static void SeedAboutItems(AppDbContext db)
    {
        if (db.AboutItems.Any()) return;

        db.AboutItems.AddRange(
            new AboutItem
            {
                Kind = AboutKinds.Intro,
                DisplayOrder = 1,
                Value = "CÂU CHUYỆN CỦA CHÚNG TÔI",
                Title = "Vận chuyển không chỉ là giao một kiện hàng",
                Content = "Đó là hành trình trao gửi sự tin cậy. LMS Express xây dựng hệ thống giao nhận minh bạch, đội ngũ chuyên nghiệp và quy trình kiểm soát chặt chẽ để mỗi đơn hàng đến nơi an toàn.\n\nChúng tôi không ngừng cải tiến công nghệ, mở rộng mạng lưới và tối ưu trải nghiệm cho cả khách hàng cá nhân lẫn doanh nghiệp."
            },
            new AboutItem { Kind = AboutKinds.Vision, DisplayOrder = 1, Icon = "bi-eye", Title = "Tầm nhìn", Content = "Trở thành đơn vị logistics đáng tin cậy, kết nối hàng hóa trên mọi miền đất nước bằng công nghệ minh bạch." },
            new AboutItem { Kind = AboutKinds.Mission, DisplayOrder = 1, Icon = "bi-bullseye", Title = "Sứ mệnh", Content = "Mang đến dịch vụ giao nhận an toàn, đúng hẹn và chi phí hợp lý cho cá nhân và doanh nghiệp." },

            new AboutItem { Kind = AboutKinds.CoreValue, DisplayOrder = 1, Icon = "bi-heart", Title = "Tận tâm", Content = "Đặt lợi ích và sự yên tâm của khách hàng lên hàng đầu." },
            new AboutItem { Kind = AboutKinds.CoreValue, DisplayOrder = 2, Icon = "bi-patch-check", Title = "Minh bạch", Content = "Báo giá rõ ràng, theo dõi hành trình đơn hàng từng bước." },
            new AboutItem { Kind = AboutKinds.CoreValue, DisplayOrder = 3, Icon = "bi-clock-history", Title = "Đúng hẹn", Content = "Cam kết thời gian giao hàng và chủ động thông báo khi có thay đổi." },
            new AboutItem { Kind = AboutKinds.CoreValue, DisplayOrder = 4, Icon = "bi-shield-check", Title = "An toàn", Content = "Kiểm soát hàng hóa chặt chẽ từ lúc nhận đến lúc giao." },

            new AboutItem { Kind = AboutKinds.Stat, DisplayOrder = 1, Value = "8+", Title = "Năm kinh nghiệm" },
            new AboutItem { Kind = AboutKinds.Stat, DisplayOrder = 2, Value = "63", Title = "Tỉnh thành" },
            new AboutItem { Kind = AboutKinds.Stat, DisplayOrder = 3, Value = "500+", Title = "Đối tác" },
            new AboutItem { Kind = AboutKinds.Stat, DisplayOrder = 4, Value = "1M+", Title = "Đơn thành công" },

            new AboutItem { Kind = AboutKinds.Milestone, DisplayOrder = 1, Value = "2018", Title = "Thành lập LMS Express", Content = "Bắt đầu với dịch vụ giao hàng nội thành TP. Hồ Chí Minh." },
            new AboutItem { Kind = AboutKinds.Milestone, DisplayOrder = 2, Value = "2020", Title = "Mở rộng liên tỉnh", Content = "Mở tuyến vận chuyển Bắc - Nam và dịch vụ cho thuê xe tải." },
            new AboutItem { Kind = AboutKinds.Milestone, DisplayOrder = 3, Value = "2023", Title = "Số hóa quy trình", Content = "Đưa vào hệ thống theo dõi trạng thái đơn hàng theo thời gian thực." },
            new AboutItem { Kind = AboutKinds.Milestone, DisplayOrder = 4, Value = "2026", Title = "Nền tảng báo giá trực tuyến", Content = "Khách hàng gửi yêu cầu, nhận báo giá và theo dõi đơn ngay trên website." });
        db.SaveChanges();
    }
    private static void SeedServices(AppDbContext db)
    {
        if (db.TransportServices.Any()) return;        // bảng đã có dữ liệu thì bỏ qua

        var order = 0;
        foreach (var s in new InMemoryContentRepository().GetServices())
        {
            order++;
            db.TransportServices.Add(new TransportService
            {
                Name = s.Name,
                Summary = s.Summary,
                Icon = s.Icon,
                DeliveryTime = s.DeliveryTime,
                PriceNote = s.PriceNote,
                IsExclusive = s.IsExclusive,
                IsFeatured = order <= 3,                // 3 dịch vụ đầu là nổi bật
                FeaturedOrder = order <= 3 ? order : 0
            });
        }
        db.SaveChanges();
    }
    private static void SeedCargoTypes(AppDbContext db)
    {
        if (db.CargoTypes.Any()) return;

        db.CargoTypes.AddRange(
            new CargoType { Name = "Hàng tiêu dùng", Description = "Quần áo, đồ gia dụng, hàng bán lẻ." },
            new CargoType { Name = "Thiết bị điện tử", Description = "Máy tính, điện thoại, linh kiện.", IsSpecial = true },
            new CargoType { Name = "Nội thất", Description = "Bàn ghế, tủ, đồ trang trí kích thước lớn." },
            new CargoType { Name = "Thực phẩm đóng gói", Description = "Thực phẩm khô, đồ uống đóng chai/hộp." },
            new CargoType { Name = "Vật liệu xây dựng", Description = "Gạch, xi măng, thép, vật tư công trình." },
            new CargoType { Name = "Hàng dễ vỡ", Description = "Thủy tinh, gốm sứ, cần đóng gói và xếp riêng.", IsSpecial = true });
        db.SaveChanges();
    }
    private static void SeedSiteSetting(AppDbContext db)
    {
        if (db.SiteSettings.Any()) return;

        db.SiteSettings.Add(new SiteSetting
        {
            SiteName = "LMS Express",
            Hotline = "1900 6868",
            Email = "support@lmsexpress.vn",
            Address = "123 Đường Logistics, Quận 1, TP. Hồ Chí Minh",
            WorkingHours = "T2 - T7: 7:30 - 18:00",
            FooterIntro = "Đồng hành cùng hàng triệu hành trình, mang đến dịch vụ giao nhận an toàn và hiệu quả trên toàn quốc.",
            Copyright = "© 2026 LMS Express Logistics. Bảo lưu mọi quyền."
        });
        db.SaveChanges();
    }

    private static void SeedSeoPages(AppDbContext db)
    {
        if (db.SeoPages.Any()) return;

        SeoPage P(string route, string name, string title, string desc, string? kw = null) =>
            new() { Route = route, PageName = name, MetaTitle = title, MetaDescription = desc, Keywords = kw };

        db.SeoPages.AddRange(
            P("Home/Index", "Trang chủ", "LMS Express - Vận chuyển nhanh chóng, an toàn", "Dịch vụ vận chuyển, giao nhận hàng hóa trên toàn quốc, theo dõi hành trình minh bạch, báo giá nhanh.", "vận chuyển, logistics, giao hàng, báo giá"),
            P("Home/About", "Giới thiệu", "Giới thiệu LMS Express", "Đơn vị logistics tận tâm, kết nối hàng hóa trên mọi miền đất nước."),
            P("Service/Index", "Dịch vụ", "Dịch vụ vận chuyển - LMS Express", "Giao hàng hỏa tốc, vận chuyển đường bộ, Bắc - Nam, cho thuê xe tải, kho bãi và COD."),
            P("Service/Quote", "Yêu cầu báo giá", "Yêu cầu báo giá vận chuyển", "Gửi yêu cầu để nhận báo giá vận chuyển nhanh từ LMS Express."),
            P("Process/Index", "Quy trình", "Quy trình vận chuyển - LMS Express", "Các bước xử lý đơn hàng từ tiếp nhận đến giao hàng thành công."),
            P("News/Index", "Tin tức", "Tin tức logistics - LMS Express", "Tin tức, kinh nghiệm và xu hướng ngành vận chuyển, logistics."),
            P("Home/Policy", "Chính sách", "Chính sách vận chuyển - LMS Express", "Các quy định giúp bảo vệ quyền lợi của khách hàng và hàng hóa."),
            P("Home/Terms", "Điều khoản", "Điều khoản sử dụng - LMS Express", "Quy định chung khi sử dụng hệ thống LMS Express."),
            P("Home/Faq", "Câu hỏi thường gặp", "Câu hỏi thường gặp - LMS Express", "Giải đáp nhanh những thắc mắc về dịch vụ vận chuyển."),
            P("Home/Contact", "Liên hệ", "Liên hệ LMS Express", "Liên hệ hotline hoặc gửi yêu cầu để được tư vấn."),
            P("Home/Gallery", "Thư viện ảnh", "Thư viện hình ảnh - LMS Express", "Hình ảnh hoạt động vận chuyển và kho bãi của LMS Express."));
        db.SaveChanges();
    }

    private static void SeedStaticPages(AppDbContext db)
    {
        if (db.StaticPages.Any()) return;

        db.StaticPages.AddRange(
            new StaticPage
            {
                Slug = "chinh-sach",
                Title = "Chính sách vận chuyển",
                IsSystem = true,
                Icon = "bi-shield-check",
                Subtitle = "Các quy định giúp bảo vệ quyền lợi của khách hàng và hàng hóa.",
                Content = """
                <h2>1. Phạm vi phục vụ</h2><p>LMS Express nhận vận chuyển hàng hóa hợp pháp trên phạm vi 63 tỉnh thành, tùy thuộc vào loại dịch vụ và khu vực giao nhận.</p>
                <h2>2. Quy định đóng gói</h2><p>Hàng hóa cần được đóng gói phù hợp với đặc tính sản phẩm. Hàng dễ vỡ, chất lỏng hoặc hàng có giá trị cần được khai báo khi tạo yêu cầu.</p>
                <h2>3. Thời gian giao hàng</h2><p>Thời gian dự kiến được tính từ lúc đơn hàng được tiếp nhận thành công và có thể thay đổi do thời tiết, giao thông hoặc sự kiện bất khả kháng.</p>
                <h2>4. Bồi thường</h2><p>Yêu cầu khiếu nại cần kèm mã vận đơn và chứng từ chứng minh giá trị hàng hóa. Mức bồi thường được áp dụng theo điều khoản của dịch vụ đã chọn.</p>
                """
            },
            new StaticPage
            {
                Slug = "dieu-khoan",
                Title = "Điều khoản sử dụng",
                IsSystem = true,
                Icon = "bi-file-earmark-text",
                Subtitle = "Quy định chung khi sử dụng hệ thống LMS Express.",
                Content = """
                <h2>Trách nhiệm người dùng</h2><p>Người dùng cung cấp thông tin chính xác, không gửi hàng thuộc danh mục cấm và chịu trách nhiệm về nội dung kiện hàng.</p>
                <h2>Bảo mật tài khoản</h2><p>Khách hàng có trách nhiệm bảo mật thông tin đăng nhập và thông báo ngay khi phát hiện hoạt động bất thường.</p>
                <h2>Thay đổi điều khoản</h2><p>LMS Express có thể cập nhật nội dung để phù hợp với quy định pháp luật và hoạt động vận hành thực tế.</p>
                """
            },
            new StaticPage
            {
                Slug = "cau-hoi-thuong-gap",
                Title = "Câu hỏi thường gặp",
                IsSystem = true,
                Icon = "bi-question-circle",
                Subtitle = "Giải đáp nhanh những thắc mắc về dịch vụ vận chuyển.",
                Content = """
                <div class="accordion faq-accordion" id="faq">
                <div class="accordion-item"><h3 class="accordion-header"><button class="accordion-button" data-bs-toggle="collapse" data-bs-target="#f1">Làm sao tra cứu hành trình đơn hàng?</button></h3><div id="f1" class="accordion-collapse collapse show" data-bs-parent="#faq"><div class="accordion-body">Nhập mã vận đơn vào ô “Tra cứu đơn hàng” tại trang chủ hoặc trang tra cứu.</div></div></div>
                <div class="accordion-item"><h3 class="accordion-header"><button class="accordion-button collapsed" data-bs-toggle="collapse" data-bs-target="#f2">Thời gian giao hàng mất bao lâu?</button></h3><div id="f2" class="accordion-collapse collapse" data-bs-parent="#faq"><div class="accordion-body">Tùy dịch vụ và tuyến đường, thời gian dự kiến từ 2 giờ đến 5 ngày.</div></div></div>
                <div class="accordion-item"><h3 class="accordion-header"><button class="accordion-button collapsed" data-bs-toggle="collapse" data-bs-target="#f3">LMS Express có hỗ trợ thu hộ COD không?</button></h3><div id="f3" class="accordion-collapse collapse" data-bs-parent="#faq"><div class="accordion-body">Có. Tiền thu hộ được đối soát và chuyển theo chu kỳ đã đăng ký.</div></div></div>
                </div>
                """
            },
            new StaticPage
            {
                Slug = "bao-mat",
                Title = "Chính sách bảo mật",
                Icon = "bi-lock",
                Subtitle = "Cách LMS Express thu thập và bảo vệ thông tin của bạn.",
                Content = """
                <h2>Thông tin thu thập</h2><p>Chúng tôi thu thập họ tên, số điện thoại, email và địa chỉ giao nhận để xử lý yêu cầu vận chuyển.</p>
                <h2>Mục đích sử dụng</h2><p>Thông tin chỉ dùng để báo giá, giao nhận, chăm sóc khách hàng và không chia sẻ cho bên thứ ba ngoài phạm vi vận chuyển.</p>
                <h2>Bảo mật</h2><p>Mật khẩu được mã hóa và dữ liệu được lưu trữ trên hệ thống có kiểm soát truy cập.</p>
                """
            },
            new StaticPage
            {
                Slug = "quy-dinh-gui-hang",
                Title = "Quy định gửi hàng",
                Icon = "bi-box-seam",
                Subtitle = "Danh mục hàng được nhận và hàng cấm gửi.",
                Content = """
                <h2>Hàng được nhận</h2><p>Hàng hóa hợp pháp, đóng gói đúng quy cách và khai báo đầy đủ loại hàng, khối lượng, số kiện.</p>
                <h2>Hàng cấm gửi</h2><p>Chất cháy nổ, chất ma túy, vũ khí, hàng hóa vi phạm pháp luật và các mặt hàng thuộc danh mục cấm vận chuyển.</p>
                <h2>Hàng đặc biệt</h2><p>Hàng dễ vỡ, hàng giá trị cao hoặc cần bảo quản riêng phải khai báo khi tạo yêu cầu để được xử lý phù hợp.</p>
                """
            });
        db.SaveChanges();
    }
    private static void SeedProcessSteps(AppDbContext db)
    {
        if (db.ProcessSteps.Any()) return;

        foreach (var s in new InMemoryContentRepository().GetProcessSteps())
        {
            db.ProcessSteps.Add(new ProcessStep
            {
                Number = s.Number,
                Title = s.Title,
                Description = s.Description,
                Icon = s.Icon
            });
        }
        db.SaveChanges();
    }
    private static void SeedRoles(AppDbContext db)
    {
        if (db.Roles.Any()) return;
        db.Roles.AddRange(
            new Role { Code = AdminRoles.SuperAdmin, Name = "SuperAdmin", Description = "Toàn quyền hệ thống" },
            new Role { Code = AdminRoles.Dispatcher, Name = "Điều phối viên", Description = "Đơn hàng, phương tiện, tài xế, chuyến, liên hệ" },
            new Role { Code = AdminRoles.Editor, Name = "Biên tập viên", Description = "Nội dung website: trang chủ, giới thiệu, quy trình, bài viết, trang tĩnh, hình ảnh, SEO" });
        db.SaveChanges();
    }

    private static void SeedAdmin(AppDbContext db)
    {
        if (db.AdminUsers.Any()) return;
        var superId = db.Roles.First(r => r.Code == AdminRoles.SuperAdmin).Id;

        // Chuyển admin cũ (nằm trong bảng Users) sang bảng riêng, giữ nguyên mật khẩu
        var old = db.Users.Where(u => u.Role == "Admin").ToList();
        if (old.Count > 0)
        {
            foreach (var u in old)
                db.AdminUsers.Add(new AdminUser
                {
                    FullName = u.FullName,
                    Email = u.Email,
                    PasswordHash = u.PasswordHash,
                    PasswordSalt = u.PasswordSalt,
                    RoleId = superId,
                    IsActive = u.IsActive
                });
            db.Users.RemoveRange(old);
        }
        else
        {
            var (hash, salt) = WebVanChuyenLogicstic.Services.PasswordHasher.Create("admin123");
            db.AdminUsers.Add(new AdminUser
            {
                FullName = "Quản trị viên",
                Email = "admin@lms.com",
                PasswordHash = hash,
                PasswordSalt = salt,
                RoleId = superId
            });
        }
        db.SaveChanges();
    }

    private static void SeedHomeSections(AppDbContext db)
    {
        if (db.HomeSections.Any()) return;

        db.HomeSections.AddRange(
            new HomeSection
            {
                SectionType = HomeSectionTypes.Hero,
                DisplayOrder = 1,
                Kicker = "Giao đúng hẹn · Theo dõi 24/7",
                Title = "Kết nối mọi hành trình, giao trọn niềm tin",
                Subtitle = "Giải pháp vận chuyển toàn diện dành cho cá nhân và doanh nghiệp. Nhanh chóng, minh bạch và tối ưu chi phí trên từng đơn hàng.",
                ButtonText = "Khám phá dịch vụ",
                ButtonUrl = "/Service",
                SecondaryButtonText = "Xem quy trình",
                SecondaryButtonUrl = "/Process",
                Items =
                [
                    new HomeSectionItem { DisplayOrder = 1, Value = "63",  Title = "Tỉnh thành" },
                    new HomeSectionItem { DisplayOrder = 2, Value = "98%", Title = "Đúng hẹn" },
                    new HomeSectionItem { DisplayOrder = 3, Value = "24/7", Title = "Hỗ trợ" }
                ]
            },
            new HomeSection
            {
                SectionType = HomeSectionTypes.Tracking,
                DisplayOrder = 2,
                Title = "Tra cứu đơn hàng",
                Subtitle = "Nhập mã vận đơn để xem hành trình"
            },
            new HomeSection
            {
                SectionType = HomeSectionTypes.ServiceIntro,
                DisplayOrder = 3,
                Kicker = "DỊCH VỤ NỔI BẬT",
                Title = "Giải pháp phù hợp với mọi nhu cầu",
                Subtitle = "Từ kiện hàng nhỏ đến nguyên chuyến, LMS Express luôn có phương án vận chuyển tối ưu cho bạn.",
                Body = "LMS Express cung cấp đa dạng dịch vụ vận chuyển, kho bãi và giao nhận cho cá nhân lẫn doanh nghiệp.",
                ButtonText = "Xem tất cả dịch vụ",
                ButtonUrl = "/Service"
            },
            new HomeSection
            {
                SectionType = HomeSectionTypes.WhyChooseUs,
                DisplayOrder = 4,
                Kicker = "VÌ SAO CHỌN CHÚNG TÔI",
                Title = "Vì sao khách hàng tin chọn LMS Express",
                Subtitle = "Những giá trị chúng tôi cam kết trên từng đơn hàng.",
                Items =
                [
                    new HomeSectionItem { DisplayOrder = 1, Icon = "bi-clock-history", Title = "Giao đúng hẹn", Description = "Lịch trình rõ ràng, chủ động thông báo khi có thay đổi." },
                    new HomeSectionItem { DisplayOrder = 2, Icon = "bi-geo-alt", Title = "Theo dõi minh bạch", Description = "Cập nhật trạng thái đơn hàng xuyên suốt hành trình." },
                    new HomeSectionItem { DisplayOrder = 3, Icon = "bi-cash-coin", Title = "Chi phí tối ưu", Description = "Báo giá rõ ràng, phương án phù hợp với ngân sách." },
                    new HomeSectionItem { DisplayOrder = 4, Icon = "bi-headset", Title = "Hỗ trợ tận tâm", Description = "Đội ngũ tư vấn sẵn sàng đồng hành khi bạn cần." }
                ]
            },
            new HomeSection
            {
                SectionType = HomeSectionTypes.Projects,
                DisplayOrder = 5,
                Kicker = "DỰ ÁN TIÊU BIỂU",
                Title = "Những hành trình chúng tôi đã đồng hành",
                Subtitle = "Một số dự án vận chuyển và phân phối tiêu biểu.",
                Items =
                [
                    new HomeSectionItem { DisplayOrder = 1, Icon = "bi-truck", Title = "Vận chuyển hàng nội thất Bắc - Nam", Description = "Tuyến vận chuyển ổn định cho lô hàng cồng kềnh, có hỗ trợ bốc xếp tận nơi." },
                    new HomeSectionItem { DisplayOrder = 2, Icon = "bi-shop", Title = "Phân phối hàng tiêu dùng cho chuỗi bán lẻ", Description = "Giao hàng đa điểm theo lịch cố định, đối soát rõ ràng từng cửa hàng." },
                    new HomeSectionItem { DisplayOrder = 3, Icon = "bi-buildings", Title = "Kho vận cho đơn vị thương mại điện tử", Description = "Lưu kho, đóng gói và giao COD, đồng bộ trạng thái đơn hàng." }
                ]
            },
            new HomeSection
            {
                SectionType = HomeSectionTypes.Process,
                DisplayOrder = 6,
                Kicker = "QUY TRÌNH MINH BẠCH",
                Title = "5 bước để hàng đến nơi an toàn",
                Subtitle = "Mỗi kiện hàng đều được kiểm soát xuyên suốt từ lúc tiếp nhận đến khi giao thành công.",
                ButtonText = "Xem toàn bộ quy trình",
                ButtonUrl = "/Process"
            },
            new HomeSection
            {
                SectionType = HomeSectionTypes.News,
                DisplayOrder = 7,
                Kicker = "TIN TỨC",
                Title = "Tin tức & kinh nghiệm logistics",
                Subtitle = "Cập nhật xu hướng và mẹo hữu ích cho việc vận chuyển hàng hóa.",
                ButtonText = "Xem tất cả tin tức",
                ButtonUrl = "/News"
            },
            new HomeSection
            {
                SectionType = HomeSectionTypes.Contact,
                DisplayOrder = 8,
                Kicker = "LIÊN HỆ",
                Title = "Để lại thông tin, chúng tôi sẽ liên hệ lại",
                Subtitle = "Chuyên viên LMS Express sẵn sàng tư vấn miễn phí."
            },
            new HomeSection
            {
                SectionType = HomeSectionTypes.Cta,
                DisplayOrder = 9,
                Kicker = "Bắt đầu ngay hôm nay",
                Title = "Bạn cần một giải pháp vận chuyển đáng tin cậy?",
                Subtitle = "Đăng ký tài khoản để tạo yêu cầu và quản lý hành trình hàng hóa dễ dàng.",
                ButtonText = "Tạo tài khoản miễn phí",
                ButtonUrl = "/Account/Register"
            }
        );
        db.SaveChanges();
    }
    // Chèn khối "Đối tác" vào trước khối Liên hệ nếu DB chưa có (DB cũ không chạy lại SeedHomeSections)
    private static void EnsurePartnersSection(AppDbContext db)
    {
        if (db.HomeSections.Any(s => s.SectionType == HomeSectionTypes.Partners)) return;

        var contact = db.HomeSections.FirstOrDefault(s => s.SectionType == HomeSectionTypes.Contact);
        var order = contact?.DisplayOrder ?? ((db.HomeSections.Max(s => (int?)s.DisplayOrder) ?? 0) + 1);

        foreach (var s in db.HomeSections.Where(s => s.DisplayOrder >= order).ToList())
            s.DisplayOrder++;

        db.HomeSections.Add(new HomeSection
        {
            SectionType = HomeSectionTypes.Partners,
            DisplayOrder = order,
            Title = "Đối tác đồng hành",
            Subtitle = "Những doanh nghiệp tin tưởng LMS Express.",
            IsSystem = true,
            IsVisible = true
        });
        db.SaveChanges();
    }
    // Chèn khối "Banner" ngay sau Hero nếu DB chưa có
    private static void EnsureBannerSection(AppDbContext db)
    {
        if (db.HomeSections.Any(s => s.SectionType == HomeSectionTypes.Banner)) return;

        var hero = db.HomeSections.FirstOrDefault(s => s.SectionType == HomeSectionTypes.Hero);
        var order = (hero?.DisplayOrder ?? 0) + 1;
        foreach (var s in db.HomeSections.Where(s => s.DisplayOrder >= order).ToList())
            s.DisplayOrder++;

        db.HomeSections.Add(new HomeSection
        {
            SectionType = HomeSectionTypes.Banner,
            DisplayOrder = order,
            Title = "Banner quảng cáo",
            IsSystem = true,
            IsVisible = true
        });
        db.SaveChanges();
    }
}