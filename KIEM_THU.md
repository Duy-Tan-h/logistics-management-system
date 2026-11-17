# Kết quả kiểm tra giao diện

Ngày kiểm tra: 22/09/2026.

## Biên dịch

- ASP.NET Core MVC, target net8.0.
- Build thành công với .NET SDK 8.0.100.
- 0 lỗi và 0 cảnh báo biên dịch.

## Trình duyệt

- Kiểm tra tự động bằng Chromium qua Playwright.
- 22 trang/trạng thái × 4 chiều rộng = **88 lượt kiểm tra**.
- Kích thước: 1440, 768, 390 và 320 px.
- Tất cả trả HTTP 200; không phát hiện tràn ngang sau khi sửa.
- **50 liên kết nội bộ** không trả lỗi HTTP.
- Không có lỗi JavaScript hoặc yêu cầu tải tài nguyên thất bại trong lượt kiểm tra cuối.
- Đường dẫn không tồn tại và bài viết không tồn tại trả HTTP 404, kèm giao diện hướng dẫn quay về trang chủ.
- Đã xem ảnh chụp trang chủ trên desktop/mobile và trang đăng nhập trên desktop để kiểm tra bố cục.

## Tương tác đã thử

- Lọc 3 bài thuộc chủ đề Kinh nghiệm; tìm không dấu; trạng thái không có kết quả; xóa bộ lọc.
- Hiện/ẩn mật khẩu.
- Đăng ký: mật khẩu không khớp, dữ liệu hợp lệ và xóa mật khẩu sau khi kiểm tra.
- Đăng nhập demo, liên hệ, báo giá và quên mật khẩu hiển thị thông báo đúng: chưa xác thực, chưa gửi hoặc chưa lưu.
- Mở/đóng FAQ và menu mobile.
- Gửi biểu mẫu tra cứu: mã mẫu viết thường vẫn tìm thấy; mã khác hiển thị không tìm thấy.

## Giới hạn

Đây là kiểm tra giao diện và luồng minh họa, không phải kiểm toán bảo mật hay nghiệm thu nghiệp vụ. Chưa kiểm thử Safari, Firefox, thiết bị thật, API, database, thanh toán hoặc email/SMS vì chưa triển khai các phần này.

Ảnh chụp nằm trong thư mục PreviewImages. Kết quả chi tiết nằm trong ui-test-results.json.
