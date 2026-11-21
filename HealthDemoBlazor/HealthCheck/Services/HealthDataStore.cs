using System.Security.Cryptography;
using HealthCheck.Models;

namespace HealthCheck.Services;

/// <summary>
/// "Database" lưu trong bộ nhớ (mất khi tắt app).
/// Đăng ký là Singleton nên MỌI người dùng dùng chung 1 instance,
/// nhiều request chạy trên nhiều thread cùng lúc => mọi thao tác đều nằm trong lock.
/// </summary>
public class HealthDataStore
{
    public const int MaxPerSlot = 3; // mỗi khung giờ / cơ sở chỉ nhận tối đa 3 người

    private readonly object _sync = new();
    private readonly List<User> _users = new();
    private readonly Dictionary<int, PatientProfile> _profiles = new();
    private readonly List<Appointment> _appointments = new();
    private int _nextUserId = 1;
    private int _nextAppointmentId = 1;

    public IReadOnlyList<HealthPackage> Packages { get; } = new List<HealthPackage>
    {
        new("basic",    "Khám tổng quát cơ bản",  500_000m,   "Khám nội, đo huyết áp, xét nghiệm máu, nước tiểu"),
        new("standard", "Khám tổng quát tiêu chuẩn", 1_200_000m, "Gói cơ bản + siêu âm bụng, X-quang phổi, điện tim"),
        new("premium",  "Khám tổng quát cao cấp",  3_500_000m, "Gói tiêu chuẩn + tầm soát ung thư, nội soi dạ dày"),
        new("kid",      "Khám sức khỏe trẻ em",    400_000m,   "Khám nhi, đánh giá dinh dưỡng, tư vấn tiêm chủng"),
    };

    public IReadOnlyList<string> Facilities { get; } = new List<string>
    {
        "Cơ sở 1 - Quận 1, TP.HCM",
        "Cơ sở 2 - Cầu Giấy, Hà Nội",
        "Cơ sở 3 - Hải Châu, Đà Nẵng",
    };

    public IReadOnlyList<string> TimeSlots { get; } = new List<string>
    {
        "07:30 - 08:30", "08:30 - 09:30", "09:30 - 10:30",
        "13:30 - 14:30", "14:30 - 15:30", "15:30 - 16:30",
    };

    // ---------------- Tài khoản ----------------

    public (bool ok, string? error) Register(string email, string password)
    {
        email = email.Trim().ToLowerInvariant();
        lock (_sync)
        {
            // Kiểm tra trùng + thêm mới phải trong CÙNG 1 lock,
            // nếu không 2 người đăng ký cùng email cùng lúc có thể đều thành công.
            if (_users.Any(u => u.Email == email))
                return (false, "Email này đã được đăng ký");

            _users.Add(new User { Id = _nextUserId++, Email = email, PasswordHash = HashPassword(password) });
            return (true, null);
        }
    }

    public User? ValidateLogin(string email, string password)
    {
        email = email.Trim().ToLowerInvariant();
        User? user;
        lock (_sync) user = _users.FirstOrDefault(u => u.Email == email);
        return user != null && VerifyPassword(password, user.PasswordHash) ? user : null;
    }

    // ---------------- Hồ sơ ----------------

    public PatientProfile? GetProfile(int userId)
    {
        lock (_sync) return _profiles.TryGetValue(userId, out var p) ? p.Clone() : null;
    }

    public void SaveProfile(PatientProfile profile)
    {
        lock (_sync) _profiles[profile.UserId] = profile.Clone();
    }

    // ---------------- Lịch khám ----------------

    public int CountBooked(string facility, DateOnly date, string slot)
    {
        lock (_sync) return CountBookedUnlocked(facility, date, slot);
    }

    public (Appointment? appointment, string? error) Book(int userId, AppointmentForm form)
    {
        var package = Packages.FirstOrDefault(p => p.Id == form.PackageId);
        if (package == null) return (null, "Gói khám không tồn tại");
        if (form.Date is not DateOnly date) return (null, "Chưa chọn ngày");
        if (date <= DateOnly.FromDateTime(DateTime.Today)) return (null, "Chỉ đặt lịch từ ngày mai trở đi");
        if (date.DayOfWeek == DayOfWeek.Sunday) return (null, "Chủ nhật không làm việc");

        lock (_sync)
        {
            if (!_profiles.ContainsKey(userId))
                return (null, "Bạn cần hoàn thành hồ sơ trước khi đặt lịch");

            // Một người không đặt 2 lịch cùng ngày
            if (_appointments.Any(a => a.UserId == userId && a.Date == date && a.Status == AppointmentStatus.Confirmed))
                return (null, "Bạn đã có lịch khám trong ngày này");

            // Kiểm tra còn chỗ + thêm lịch trong CÙNG 1 lock => không bao giờ vượt quá MaxPerSlot
            if (CountBookedUnlocked(form.Facility, date, form.TimeSlot) >= MaxPerSlot)
                return (null, "Khung giờ này đã đủ người, vui lòng chọn giờ khác");

            var appt = new Appointment
            {
                Id = _nextAppointmentId++,
                UserId = userId,
                Package = package,
                Facility = form.Facility,
                Date = date,
                TimeSlot = form.TimeSlot,
                Symptoms = form.Symptoms,
            };
            _appointments.Add(appt);
            return (appt, null);
        }
    }

    public List<Appointment> GetAppointments(int userId)
    {
        lock (_sync)
            return _appointments.Where(a => a.UserId == userId)
                                .OrderByDescending(a => a.Date).ThenBy(a => a.TimeSlot)
                                .ToList(); // trả về bản sao danh sách, không trả list gốc ra ngoài lock
    }

    public bool Cancel(int userId, int appointmentId)
    {
        lock (_sync)
        {
            var a = _appointments.FirstOrDefault(x => x.Id == appointmentId && x.UserId == userId);
            if (a == null || a.Status == AppointmentStatus.Cancelled) return false;
            a.Status = AppointmentStatus.Cancelled;
            return true;
        }
    }

    private int CountBookedUnlocked(string facility, DateOnly date, string slot) =>
        _appointments.Count(a => a.Facility == facility && a.Date == date &&
                                 a.TimeSlot == slot && a.Status == AppointmentStatus.Confirmed);

    // ---------------- Mã hóa mật khẩu (PBKDF2) ----------------

    private static string HashPassword(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(16);
        var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, 100_000, HashAlgorithmName.SHA256, 32);
        return $"{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
    }

    private static bool VerifyPassword(string password, string stored)
    {
        var parts = stored.Split('.');
        var salt = Convert.FromBase64String(parts[0]);
        var expected = Convert.FromBase64String(parts[1]);
        var actual = Rfc2898DeriveBytes.Pbkdf2(password, salt, 100_000, HashAlgorithmName.SHA256, 32);
        return CryptographicOperations.FixedTimeEquals(actual, expected);
    }
}
