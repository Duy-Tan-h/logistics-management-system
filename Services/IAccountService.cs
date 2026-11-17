using WebVanChuyenLogicstic.Models;

namespace WebVanChuyenLogicstic.Services;

public interface IAccountService
{
    (bool Success, string Message) Register(RegisterViewModel model);
    (bool Success, string Message) ChangePassword(int userId, ChangePasswordViewModel model);
    UserAccount? Login(LoginViewModel model);
}
