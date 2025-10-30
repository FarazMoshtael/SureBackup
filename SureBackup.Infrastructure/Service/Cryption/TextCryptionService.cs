using SureBackup.Application.Service.Cryption;
using System.Security.Cryptography;
using System.Text;

namespace SureBackup.Infrastructure.Service.Cryption;

public class TextCryptionService : ITextCryptionService
{
    public string Encrypt(string plainText)
    {
#pragma warning disable CA1416 // Validate platform compatibility
        byte[] encrypted = ProtectedData.Protect(Encoding.UTF8.GetBytes(plainText), null, DataProtectionScope.LocalMachine);
        return Convert.ToBase64String(encrypted);
    }
    public string Decrypt(string cipherText)
    {
        byte[] textBytes = Convert.FromBase64String(cipherText);
        byte[] decrypted = ProtectedData.Unprotect(textBytes, null, DataProtectionScope.LocalMachine);
        return Encoding.UTF8.GetString(decrypted);
    }

}
