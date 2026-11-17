using WebVanChuyenLogicstic.Models;

namespace WebVanChuyenLogicstic.Data;

public interface IUserRepository
{
    UserAccount? FindByEmail(string email);
    UserAccount? FindById(int id);
    void Add(UserAccount account);
    void Update(UserAccount account);
}