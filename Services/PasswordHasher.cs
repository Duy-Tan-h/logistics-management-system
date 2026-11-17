using System.Security.Cryptography;

namespace WebVanChuyenLogicstic.Services;

public static class PasswordHasher
{
    private const int Iterations = 100_000;

    public static (string Hash, string Salt) Create(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(16);
        var hash = Compute(password, salt);
        return (Convert.ToBase64String(hash), Convert.ToBase64String(salt));
    }

    public static bool Verify(string password, string hash, string salt)
    {
        var actual = Compute(password, Convert.FromBase64String(salt));
        return CryptographicOperations.FixedTimeEquals(actual, Convert.FromBase64String(hash));
    }

    private static byte[] Compute(string password, byte[] salt) =>
        Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithmName.SHA256, 32);
}