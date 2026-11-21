using System.ComponentModel.DataAnnotations;

namespace HealthCheck.Models;

// ===================== Tài khoản =====================

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = "";
    public string PasswordHash { get; set; } = "";
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}

public class RegisterModel
{
    [Required(ErrorMessage = "Vui lòng nhập email")]
    [EmailAddress(ErrorMessage = "Email không hợp lệ")]
    public string Email { get; set; } = "";

    [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
    [MinLength(6, ErrorMessage = "Mật khẩu tối thiểu 6 ký tự")]
    public string Password { get; set; } = "";

    [Required(ErrorMessage = "Vui lòng nhập lại mật khẩu")]
    [Compare(nameof(Password), ErrorMessage = "Mật khẩu nhập lại không khớp")]
    public string ConfirmPassword { get; set; } = "";
}

public class LoginModel
{
    [Required(ErrorMessage = "Vui lòng nhập email")]
    [EmailAddress(ErrorMessage = "Email không hợp lệ")]
    public string Email { get; set; } = "";

    [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
    public string Password { get; set; } = "";
}

// ===================== Hồ sơ bệnh nhân =====================

public class PatientProfile
{
    public int UserId { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập họ tên")]
    [StringLength(100)]
    public string FullName { get; set; } = "";

    [Required(ErrorMessage = "Vui lòng chọn ngày sinh")]
    public DateOnly? DateOfBirth { get; set; }

    [Required(ErrorMessage = "Vui lòng chọn giới tính")]
    public string Gender { get; set; } = "";

    [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
    [RegularExpression(@"^0\d{9}$", ErrorMessage = "Số điện thoại gồm 10 số, bắt đầu bằng 0")]
    public string Phone { get; set; } = "";

    [Required(ErrorMessage = "Vui lòng nhập số CCCD")]
    [RegularExpression(@"^\d{12}$", ErrorMessage = "CCCD gồm 12 chữ số")]
    public string CitizenId { get; set; } = "";

    [Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
    public string Address { get; set; } = "";

    // Không bắt buộc
    public string? InsuranceNumber { get; set; }

    public string? MedicalHistory { get; set; }

    public PatientProfile Clone() => (PatientProfile)MemberwiseClone();
}

// ===================== Đặt lịch khám =====================

public record HealthPackage(string Id, string Name, decimal Price, string Description);

public class AppointmentForm
{
    [Required(ErrorMessage = "Vui lòng chọn gói khám")]
    public string PackageId { get; set; } = "";

    [Required(ErrorMessage = "Vui lòng chọn cơ sở khám")]
    public string Facility { get; set; } = "";

    [Required(ErrorMessage = "Vui lòng chọn ngày khám")]
    public DateOnly? Date { get; set; }

    [Required(ErrorMessage = "Vui lòng chọn khung giờ")]
    public string TimeSlot { get; set; } = "";

    [StringLength(500)]
    public string? Symptoms { get; set; }

    [Range(typeof(bool), "true", "true", ErrorMessage = "Bạn cần đồng ý với điều khoản")]
    public bool AgreeTerms { get; set; }
}

public enum AppointmentStatus { Confirmed, Cancelled }

public class Appointment
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public HealthPackage Package { get; set; } = default!;
    public string Facility { get; set; } = "";
    public DateOnly Date { get; set; }
    public string TimeSlot { get; set; } = "";
    public string? Symptoms { get; set; }
    public AppointmentStatus Status { get; set; } = AppointmentStatus.Confirmed;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string Code => $"HC{Id:D5}";
}

public static class FormatExtensions
{
    private static readonly System.Globalization.CultureInfo Vn = new("vi-VN");

    // 1200000 -> "1.200.000 đ"
    public static string ToVnd(this decimal amount) => amount.ToString("N0", Vn) + " đ";
}
