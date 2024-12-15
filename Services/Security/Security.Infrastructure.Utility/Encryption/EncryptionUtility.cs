using System.Security.Cryptography;
using System.Text;

namespace Security.Infrastructure.Utility.Encryption;

public class EncryptionUtility
{
    public string GetNewSalt()
    {
        return Guid.NewGuid().ToString();
    }
    public string GetSHA256(string password, string salt)
    {
        using(var sha256 = SHA256.Create())
        {
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
            var hash = BitConverter.ToString(bytes).Replace("-","").ToLower();
            return hash;
        }
    }
}
