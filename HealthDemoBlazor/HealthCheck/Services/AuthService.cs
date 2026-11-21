using HealthCheck.Models;

namespace HealthCheck.Services;

/// <summary>
/// Lưu người dùng đang đăng nhập. Đăng ký Scoped => mỗi tab trình duyệt
/// (mỗi "circuit" Blazor Server) có 1 instance riêng.
/// Lưu ý: tải lại trang (F5) sẽ mất trạng thái đăng nhập – đây là bản demo.
/// </summary>
public class AuthService
{
    public User? CurrentUser { get; private set; }
    public bool IsLoggedIn => CurrentUser != null;

    public event Action? OnChange;

    public void Login(User user)
    {
        CurrentUser = user;
        OnChange?.Invoke();
    }

    public void Logout()
    {
        CurrentUser = null;
        OnChange?.Invoke();
    }
}
