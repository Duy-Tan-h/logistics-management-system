using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebVanChuyenLogicstic.Data;
using WebVanChuyenLogicstic.Filters;
using WebVanChuyenLogicstic.Models;
using WebVanChuyenLogicstic.Helpers;

namespace WebVanChuyenLogicstic.Controllers
{
    [Route("Admin/Contact/[action]")]
    [AdminOnly(AdminRoles.Dispatcher)]
    public class ContactController(AppDbContext db) : Controller
    {
        // 1. Hiển thị danh sách Liên hệ và Email đăng ký
        public IActionResult Index(int? page, int? subPage)
        {
            ViewBag.Subscribers = db.NewsletterSubscribers.AsNoTracking()
                .OrderByDescending(s => s.SubscribedAt).ToPagedList(subPage, 10, "subPage", "subscribers-pane");

            var contacts = db.Contacts.AsNoTracking()
                .OrderBy(c => c.IsRead).ThenByDescending(c => c.CreatedAt).ToPagedList(page);

            return View("~/Views/Admin/Contact/Index.cshtml", contacts);
        }

        [HttpPost("{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult ToggleReplied(int id)
        {
            var c = db.Contacts.FirstOrDefault(x => x.Id == id);
            if (c != null)
            {
                c.IsRead = !c.IsRead;
                db.SaveChanges();
                TempData["SuccessMessage"] = c.IsRead ? "Đã đánh dấu đã phản hồi." : "Đã chuyển về chưa phản hồi.";
            }
            return RedirectToAction(nameof(Index));
        }

        // 2. Xóa tin nhắn liên hệ
        [HttpPost("{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var contact = db.Contacts.FirstOrDefault(c => c.Id == id);
            if (contact != null)
            {
                db.Contacts.Remove(contact);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Đã xóa tin nhắn liên hệ thành công!";
            }
            return RedirectToAction(nameof(Index));
        }

        // 3. Xóa email đăng ký nhận tin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteSubscriber(string email)
        {
            var sub = db.NewsletterSubscribers.FirstOrDefault(s => s.Email == email);
            if (sub != null)
            {
                db.NewsletterSubscribers.Remove(sub);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Đã xóa email khỏi danh sách nhận tin!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}