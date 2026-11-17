# LMS Express Logistics

Website vận chuyển – giao nhận hàng hóa, xây dựng bằng **ASP.NET Core MVC (.NET 8)**, **Entity Framework Core 8** và **SQL Server**. Gồm phần website dành cho khách hàng và khu vực quản trị (Admin).

---

## 1. Chức năng chính

**Khách hàng**

- Xem trang chủ, giới thiệu, dịch vụ, quy trình, tin tức, chính sách, câu hỏi thường gặp, liên hệ.
- Đăng ký, đăng nhập, quản lý hồ sơ cá nhân; gửi yêu cầu cấp lại mật khẩu (Admin xử lý).
- Gửi yêu cầu báo giá với nhiều dòng hàng (loại hàng, số lượng, khối lượng, hàng dễ vỡ); lưu địa chỉ hay dùng.
- Xem, đồng ý hoặc hủy báo giá (trong tài khoản hoặc qua liên kết trong email).
- Tra cứu hành trình đơn hàng bằng mã vận đơn; xem danh sách "Đơn của tôi".

**Quản trị (Admin)** – phân quyền theo 3 vai trò

| Vai trò | Quyền |
|---|---|
| SuperAdmin | Toàn quyền hệ thống, quản lý tài khoản quản trị |
| Điều phối viên | Đơn hàng, phương tiện, tài xế, chuyến xe, liên hệ |
| Biên tập viên | Nội dung website: trang chủ, giới thiệu, quy trình, bài viết, trang tĩnh, hình ảnh, SEO |

- Quản lý đơn hàng: tiếp nhận, gửi báo giá, phân công xe/tài xế, cập nhật trạng thái
  (Yêu cầu báo giá → Đã báo giá → Đã xác nhận → Đang vận chuyển → Đã giao / Đã hủy).
- Quản lý xe, tài xế, **chuyến xe (ghép nhiều đơn vào một chuyến)**, dịch vụ, loại hàng.
- Quản lý nội dung: trang chủ, giới thiệu, quy trình, dự án, tin tức, trang tĩnh, thư viện hình ảnh (đối tác, banner), SEO, cài đặt website.
- Quản lý khách hàng; xem tin nhắn liên hệ và yêu cầu cấp lại mật khẩu.

---

## 2. Yêu cầu hệ thống

| Thành phần | Yêu cầu |
|---|---|
| Hệ điều hành | Windows 10/11 |
| IDE | Visual Studio 2022 (workload **ASP.NET and web development**) |
| .NET | .NET 8 SDK |
| Cơ sở dữ liệu | SQL Server Express (2016 trở lên) hoặc LocalDB |
| Công cụ (khuyến nghị) | SQL Server Management Studio (SSMS) |

---

## 3. Cài đặt và chạy

### Bước 1. Giải nén project

Giải nén vào thư mục bất kỳ, ví dụ `D:\LMS\`. Mở file `WebVanChuyenLogicstic.sln` bằng Visual Studio 2022.

### Bước 2. Tạo cơ sở dữ liệu (chọn một trong hai cách)

**Cách A – Import file SQL (khuyến nghị, có sẵn dữ liệu mẫu)**

1. Mở SSMS và kết nối vào SQL Server.
2. Mở file `Database/LmsExpressDb.sql` (**File → Open → File**) rồi bấm **Execute** (F5).
3. Chờ đến khi tab *Messages* báo xong, không có dòng đỏ.

Script tự **xóa database `LmsExpressDb` cũ (nếu có)**, tạo mới database cùng đầy đủ 26 bảng, khóa ngoại, lịch sử migration và toàn bộ dữ liệu hiện có của hệ thống (dịch vụ, bài viết, đơn hàng, sự kiện đơn, xe, tài xế, chuyến xe, tài khoản...). Database được tạo ở thư mục dữ liệu mặc định của SQL Server trên máy đó, không phụ thuộc đường dẫn hay phiên bản SQL Server. Không cần chạy `Update-Database`.

**Cách B – Dùng Migration (chỉ có dữ liệu nền)**

1. Trong Visual Studio, mở **Tools → NuGet Package Manager → Package Manager Console**.
2. Chạy lệnh:
   ```powershell
   Update-Database
   ```
   (Có thể bỏ qua bước này: khi chạy app, hệ thống tự áp dụng migration.)
3. Khi chạy app, dữ liệu nền (dịch vụ, loại hàng, quy trình, khối trang chủ, vai trò, tài khoản Admin...) được thêm tự động. Cách này **không** có bài viết, xe, tài xế hay đơn hàng mẫu.

### Bước 3. Sửa chuỗi kết nối cơ sở dữ liệu (nếu cần)

Mặc định `appsettings.json` dùng SQL Server Express cài trên máy:

```json
"ConnectionStrings": {
  "Default": "Server=.\\SQLEXPRESS;Database=LmsExpressDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

Nếu máy dùng loại SQL Server khác, đổi phần `Server=` cho phù hợp:

| Trường hợp | Giá trị |
|---|---|
| SQL Server Express (mặc định) | `.\\SQLEXPRESS` |
| LocalDB (đi kèm Visual Studio) | `(localdb)\\MSSQLLocalDB` |
| SQL Server bản đầy đủ | `.` hoặc `localhost` |

> Trong file JSON, dấu `\` phải viết thành `\\`.
> Cách xem tên server: mở SSMS, tên hiển thị ở ô **Server name** trong cửa sổ kết nối.
> Dùng LocalDB thì import file SQL bằng SSMS kết nối vào `(localdb)\MSSQLLocalDB`.

### Bước 4. Chạy ứng dụng

1. Trên thanh công cụ, chọn profile **https** (hoặc **http**) cạnh nút ▶.
2. Nhấn **Ctrl + F5**.
3. Trình duyệt mở trang web tại:
   - `https://localhost:7221`
   - hoặc `http://localhost:5198`

Nếu trình duyệt cảnh báo chứng chỉ HTTPS, chạy một lần lệnh sau rồi chạy lại:

```powershell
dotnet dev-certs https --trust
```

---

## 4. Tài khoản đăng nhập

**Khu vực quản trị** (đăng nhập tại trang **Đăng nhập**, hệ thống tự chuyển vào khu Admin):

| Vai trò | Email | Mật khẩu |
|---|---|---|
| SuperAdmin | `admin@lms.com` | `admin123` |
| SuperAdmin | `haiduy01366@gmail.com` | `123456` |

**Khách hàng:** tự đăng ký tại trang **Đăng ký**.

**Tra cứu đơn hàng mẫu:** vào trang **Tra cứu**, nhập mã `LMS20261005-6869` (đơn đã giao).

> Nên đổi mật khẩu Admin nếu triển khai thật.

---

## 5. Cấu hình gửi email (tùy chọn)

Hệ thống dùng email để gửi xác nhận yêu cầu, báo giá và thông báo trạng thái đơn hàng. **Nếu không cấu hình email, ứng dụng vẫn chạy bình thường**; chỉ khi gửi báo giá, Admin sẽ thấy thông báo *"Chưa gửi được email..."* và cần liên hệ khách qua điện thoại.

Để bật gửi email, dùng tài khoản Gmail riêng của dự án (đã bật Xác minh 2 bước và tạo sẵn Mật khẩu ứng dụng):

1. Trong Visual Studio, chuột phải vào project → **Manage User Secrets**.
2. Dán nội dung sau vào file `secrets.json`:

   ```json
   {
     "Email": {
       "Enabled": true,
       "User": "LMSExpress123@gmail.com",
       "Password": "ddtqaessngjksryd",
       "FromEmail": "LMSExpress123@gmail.com"
     }
   }
   ```

3. Lưu file (Ctrl + S) và chạy lại ứng dụng.

> **Lưu ý:** `LMSExpress123@gmail.com` là tài khoản tạo riêng cho đồ án, và `Password` ở trên là **Mật khẩu ứng dụng** (16 ký tự), không phải mật khẩu đăng nhập Gmail. Mật khẩu này chỉ dùng để chạy đồ án, không đưa lên Git hay chia sẻ công khai. Thông tin được đặt trong User Secrets nên không nằm trong `appsettings.json`.
>
> Muốn dùng Gmail khác: bật **Xác minh 2 bước**, tạo **Mật khẩu ứng dụng** (Google Account → Security → App passwords) rồi thay `User`, `Password`, `FromEmail`.

Cấu hình `Host` (`smtp.gmail.com`), `Port` (587) và `EnableSsl` đã có sẵn trong `appsettings.json`.

---

## 6. Hình ảnh

- `wwwroot/images/` – ảnh cố định của giao diện (tin tức, dịch vụ, dự án).
- `wwwroot/uploads/` – ảnh do Admin tải lên (đối tác, banner...). Cơ sở dữ liệu chỉ lưu **đường dẫn**, vì vậy cần giữ nguyên thư mục này khi sao chép project, nếu không ảnh sẽ bị vỡ.

Tên thư mục phân biệt chữ hoa/chữ thường trên macOS và Linux (ví dụ `images/Services`), nên khi đổi tên thư mục hãy giữ đúng như trong project.

**Quy ước đặt tên ảnh:** dùng chữ thường, không dấu, không khoảng trắng, nối bằng dấu gạch ngang (ví dụ `cho-vlxd.png`). Khi đổi tên một ảnh, phải sửa đường dẫn tương ứng trong cơ sở dữ liệu (cột `ImageUrl` của các bảng như `HomeSectionItems`, `Articles`, `Services`, `MediaItems`) hoặc trong trang quản trị.

---

## 7. Cấu trúc thư mục

| Thư mục | Nội dung |
|---|---|
| `Controllers/` | Tiếp nhận request, gọi Service và trả về View |
| `Models/` | Thực thể (Entity) và ViewModel |
| `Data/` | `AppDbContext`, Repository và `DbSeeder` (dữ liệu nền) |
| `Services/` | Xử lý nghiệp vụ, gửi email, băm mật khẩu (PBKDF2 + salt) |
| `Filters/`, `Helpers/` | Bộ lọc phân quyền và các hàm hỗ trợ |
| `Migrations/` | Lịch sử thay đổi cấu trúc cơ sở dữ liệu (EF Core) |
| `Views/` | Giao diện Razor (website khách hàng và khu vực Admin) |
| `wwwroot/` | CSS, JavaScript, thư viện giao diện, hình ảnh |
| `Database/` | File SQL chứa cấu trúc và dữ liệu mẫu |
| `PreviewImages/` | Ảnh chụp giao diện minh họa |

---

## 8. Xử lý sự cố thường gặp

| Hiện tượng | Nguyên nhân và cách xử lý |
|---|---|
| `A network-related or instance-specific error` / `Cannot open database` | Sai `Server=` trong `appsettings.json`, hoặc dịch vụ SQL Server chưa chạy. Mở `services.msc` và kiểm tra dịch vụ **SQL Server (SQLEXPRESS)** đang ở trạng thái Running. |
| `Invalid object name '...'` | Chưa tạo bảng. Import file `.sql` (Cách A) hoặc chạy ứng dụng / `Update-Database` (Cách B). |
| Import file `.sql` báo lỗi `Cannot drop database ... because it is currently in use` | Đóng các cửa sổ query khác đang dùng `LmsExpressDb`, tắt ứng dụng web đang chạy rồi chạy lại script. |
| Tiếng Việt hiện dấu `?` hoặc ký tự lạ sau khi import | Mở file `.sql` bằng SSMS qua **File → Open → File** (file đã lưu UTF-8 có BOM), không copy-paste từ trình soạn thảo khác. |
| `Chưa gửi được email...` | Xem mục 5. Mở cửa sổ console, tìm dòng `Không gửi được email`: `No such host is known` nghĩa là mạng/DNS; `5.7.8 Username and Password not accepted` nghĩa là sai mật khẩu ứng dụng; không có dòng nào thì kiểm tra `Email:Enabled` đã là `true` trong User Secrets. |
| Ảnh bị vỡ | Thiếu hoặc đổi tên thư mục `wwwroot/uploads/` hoặc `wwwroot/images/`, hoặc tên file ảnh không khớp đường dẫn trong cơ sở dữ liệu (xem mục 6). Nếu giải nén bằng công cụ lạ làm tên file bị mã hóa lại, hãy giải nén bằng Windows Explorer hoặc 7-Zip. |
| Cổng 7221 hoặc 5198 đã được sử dụng | Tắt phiên bản đang chạy trước đó, hoặc đổi cổng trong `Properties/launchSettings.json`. |

---

## 9. Lệnh Entity Framework hữu ích

Chạy trong **Package Manager Console**:

```powershell
Add-Migration TenMigration     # tạo migration mới sau khi sửa Model
Update-Database                # áp dụng migration vào cơ sở dữ liệu
```
