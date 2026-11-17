using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using WebVanChuyenLogicstic.Data;
using WebVanChuyenLogicstic.Models;
using WebVanChuyenLogicstic.Services;

namespace WebVanChuyenLogicstic.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IContentService _contentService;
        private readonly IHomeService _homeService;
        private readonly AppDbContext _db;

        public HomeController(ILogger<HomeController> logger, IContentService contentService, IHomeService homeService, AppDbContext db)
        {
            _logger = logger;
            _contentService = contentService;
            _homeService = homeService;
            _db = db;
        }

        public IActionResult Index() => View(_homeService.GetHomePage());

        public IActionResult About() =>
    View(_db.AboutItems.AsNoTracking().Where(a => a.IsVisible).OrderBy(a => a.DisplayOrder).ThenBy(a => a.Id).ToList());
        public IActionResult Contact() => View();
        public IActionResult Policy() => ShowStaticPage("chinh-sach");
        public IActionResult Terms() => ShowStaticPage("dieu-khoan");
        public IActionResult Faq() => ShowStaticPage("cau-hoi-thuong-gap");
        public IActionResult Privacy() => ShowStaticPage("bao-mat");

        // Các trang tĩnh do admin tạo thêm: /trang/ten-trang
        [HttpGet("trang/{slug}")]
        public IActionResult Page(string slug) => ShowStaticPage(slug);

        

        private IActionResult ShowStaticPage(string slug)
        {
            var page = _db.StaticPages.AsNoTracking().FirstOrDefault(p => p.Slug == slug && p.IsPublished);
            return page is null ? NotFound() : View("StaticPage", page);
        }

        // 1. Hứng dữ liệu từ Form Liên hệ — lưu thẳng vào bảng Contacts
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitContact(ContactMessage model)
        {
            // Không cho client tự đặt các trường hệ thống
            model.Id = 0;
            model.IsRead = false;
            model.CreatedAt = DateTime.Now;

            if (!ModelState.IsValid)
                return View("Contact", model);   // hiện lỗi từng trường, giữ nguyên dữ liệu đã nhập

            try
            {
                _db.Contacts.Add(model);
                _db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError(string.Empty, "Không thể thực hiện, vui lòng thử lại.");
                return View("Contact", model);
            }

            TempData["SuccessMessage"] = "Cảm ơn bạn! Yêu cầu của bạn đã được gửi thành công, nhân viên điều phối sẽ liên hệ sớm nhất.";
            return RedirectToAction("Contact");
        }

        // 2. Hứng dữ liệu từ form Đăng ký nhận tin — lưu vào bảng NewsletterSubscribers
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubscribeNews(string? SubscribeEmail)
        {
            var email = (SubscribeEmail ?? "").Trim();

            if (email.Length == 0 || email.Length > 200 || !new EmailAddressAttribute().IsValid(email))
                TempData["NewsletterError"] = "Email không đúng định dạng, vui lòng nhập lại.";
            else if (_db.NewsletterSubscribers.Any(x => x.Email == email))
                TempData["NewsletterError"] = "Email đã được đăng ký.";
            else
            {
                try
                {
                    _db.NewsletterSubscribers.Add(new NewsletterSubscriber { Email = email });
                    _db.SaveChanges();
                    TempData["NewsletterSuccess"] = $"Đăng ký nhận bản tin thành công với email: {email}";
                }
                catch (DbUpdateException)
                {
                    TempData["NewsletterError"] = "Không thể thực hiện, vui lòng thử lại.";
                }
            }

            var referer = Request.Headers["Referer"].ToString();
            return Redirect(string.IsNullOrEmpty(referer) ? "/" : referer);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}