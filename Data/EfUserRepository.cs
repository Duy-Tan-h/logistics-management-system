using WebVanChuyenLogicstic.Models;

namespace WebVanChuyenLogicstic.Data;

public class EfUserRepository(AppDbContext db) : IUserRepository
{
    public UserAccount? FindByEmail(string email)
    {
        var e = email.Trim();
        return db.Users.FirstOrDefault(u => u.Email == e);
    }

    public UserAccount? FindById(int id) => db.Users.Find(id);

    public void Add(UserAccount account)
    {
        db.Users.Add(account);
        db.SaveChanges();
    }
    public void Update(UserAccount account)
    {
        db.Users.Update(account);
        db.SaveChanges();
    }
}