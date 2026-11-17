using Microsoft.EntityFrameworkCore;
using WebVanChuyenLogicstic.Data;
using WebVanChuyenLogicstic.Models;

namespace WebVanChuyenLogicstic.Services;

public class AccountService(IUserRepository repository) : IAccountService
{
    public (bool Success, string Message) Register(RegisterViewModel model)
    {
        if (repository.FindByEmail(model.Email) is not null)
            return (false, "Email này đã được sử dụng.");

        var (hash, salt) = PasswordHasher.Create(model.Password);
        try
        {
            repository.Add(new UserAccount
            {
                FullName = model.FullName.Trim(),
                Email = model.Email.Trim(),
                Phone = model.Phone.Trim(),
                PasswordHash = hash,
                PasswordSalt = salt,
                Role = "Customer"
            });
        }
        catch (DbUpdateException)
        {
            return (false, "Email này đã được sử dụng.");
        }
        return (true, "Đăng ký thành công. Bạn có thể đăng nhập ngay.");
    }
    public (bool Success, string Message) ChangePassword(int userId, ChangePasswordViewModel model)
    {
        var account = repository.FindById(userId);
        if (account is null)
            return (false, "Không tìm thấy tài khoản.");

        if (!PasswordHasher.Verify(model.CurrentPassword, account.PasswordHash, account.PasswordSalt))
            return (false, "Mật khẩu hiện tại không đúng.");

        if (model.CurrentPassword == model.NewPassword)
            return (false, "Mật khẩu mới phải khác mật khẩu hiện tại.");

        var (hash, salt) = PasswordHasher.Create(model.NewPassword);
        account.PasswordHash = hash;
        account.PasswordSalt = salt;
        repository.Update(account);
        return (true, "Đã đổi mật khẩu thành công.");
    }

    public UserAccount? Login(LoginViewModel model)
    {
        var account = repository.FindByEmail(model.Email);
        if (account is null) return null;
        return PasswordHasher.Verify(model.Password, account.PasswordHash, account.PasswordSalt) ? account : null;
    }
}