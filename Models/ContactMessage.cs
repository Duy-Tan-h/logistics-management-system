using System.ComponentModel.DataAnnotations;

namespace WebVanChuyenLogicstic.Models
{
    public class ContactMessage
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập họ tên.")]
        [StringLength(150, ErrorMessage = "Họ tên tối đa 150 ký tự.")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
        [RegularExpression(@"^(0|\+84)[0-9]{9,10}$", ErrorMessage = "Số điện thoại không hợp lệ (ví dụ 0901234567).")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập email.")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng.")]
        [StringLength(200, ErrorMessage = "Email tối đa 200 ký tự.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập nội dung.")]
        [StringLength(2000, ErrorMessage = "Nội dung tối đa 2000 ký tự.")]
        public string Message { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsRead { get; set; }
    }
    public class NewsletterSubscriber
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public DateTime SubscribedAt { get; set; } = DateTime.Now;
    }
}
