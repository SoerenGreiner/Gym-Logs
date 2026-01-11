using System.Security.Cryptography;
using System.Text;

public static class SecurityHelper
{
    public static string GenerateSalt(int size = 16)
    {
        var rng = RandomNumberGenerator.Create();
        var buffer = new byte[size];
        rng.GetBytes(buffer);
        return Convert.ToBase64String(buffer);
    }

    public static string HashPassword(string password, string salt)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password + salt);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }

    public static bool VerifyPassword(string password, string salt, string hash)
    {
        var newHash = HashPassword(password, salt);
        return newHash == hash;
    }
}