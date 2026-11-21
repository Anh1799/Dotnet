# HealthCheck – Đăng ký khám sức khỏe (Blazor, .NET 10)

## Chức năng
- Đăng ký tài khoản (mật khẩu được mã hóa PBKDF2)
- Đăng nhập / đăng xuất
- Điền hồ sơ cá nhân: họ tên, ngày sinh, giới tính, SĐT, CCCD, địa chỉ, BHYT, tiền sử bệnh
- Đặt lịch khám: chọn gói, cơ sở, ngày, khung giờ (mỗi khung tối đa 3 người)
- Xem và hủy lịch khám

## Cài đặt

### 1. Tạo dự án Blazor (tên phải là HealthCheck)
```bash
dotnet new blazor -n HealthCheck --interactivity Server --all-interactive
```

### 2. Chép các file trong thư mục này vào dự án
Ghi đè nếu trùng tên:
```
Models/Models.cs
Services/HealthDataStore.cs
Services/AuthService.cs
Components/Shared/RequireLogin.razor
Components/Layout/NavMenu.razor        (ghi đè)
Components/Pages/Home.razor            (ghi đè)
Components/Pages/Register.razor
Components/Pages/Login.razor
Components/Pages/Profile.razor
Components/Pages/Book.razor
Components/Pages/MyAppointments.razor
```
Có thể xóa `Counter.razor` và `Weather.razor` vì không dùng tới.

### 3. Sửa Program.cs
Thêm 2 dòng ngay sau `var builder = WebApplication.CreateBuilder(args);`:
```csharp
builder.Services.AddSingleton<HealthCheck.Services.HealthDataStore>();
builder.Services.AddScoped<HealthCheck.Services.AuthService>();
```

### 4. Chạy
```bash
cd HealthCheck
dotnet watch
```

## Các trang
| URL          | Trang                |
|--------------|----------------------|
| `/`          | Trang chủ, gói khám  |
| `/dang-ky`   | Đăng ký tài khoản    |
| `/dang-nhap` | Đăng nhập            |
| `/ho-so`     | Hồ sơ cá nhân        |
| `/dat-lich`  | Đặt lịch khám        |
| `/lich-kham` | Lịch khám của tôi    |

## Liên quan tới thread & lock
`HealthDataStore` là **Singleton**: mọi người dùng dùng chung 1 instance, các request
chạy trên nhiều thread cùng lúc. Vì vậy mọi thao tác đọc/ghi đều nằm trong `lock (_sync)`.
Ví dụ trong `Book()`: kiểm tra "còn chỗ không" và "thêm lịch" nằm trong CÙNG một lock,
nên dù 10 người bấm đặt cùng lúc, mỗi khung giờ cũng không bao giờ vượt quá 3 người.

## Giới hạn của bản demo
- Dữ liệu lưu trong bộ nhớ: tắt app là mất. Bước tiếp theo: thay `HealthDataStore`
  bằng Entity Framework Core + SQL Server/SQLite.
- Đăng nhập lưu theo tab trình duyệt: nhấn F5 sẽ phải đăng nhập lại.
  Dùng cho thật thì nên dùng ASP.NET Core Identity
  (`dotnet new blazor --auth Individual`).
