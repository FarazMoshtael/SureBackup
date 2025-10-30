using SureBackup.Application.Service.Cryption;
using SureBackup.Domain.Common;
using System.Security.Cryptography;
using System.Text;

namespace SureBackup.Infrastructure.Service.Cryption;

public class StreamCryptionService : IStreamCryptionService
{

    public void Encrypt(Stream sourceFile, Stream destinationFile, string key)
    {
        byte[] iv = new byte[Constants.KeySize];
        using var aes = Aes.Create();
        var bytes = Encoding.UTF8.GetBytes(key);
        aes.Key = bytes;
        aes.IV = iv;

        ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        using CryptoStream encryptionStream = new CryptoStream(destinationFile, encryptor, CryptoStreamMode.Write, leaveOpen: true);

        byte[] buffer = new byte[sourceFile.Length];
        int read;
        while ((read = sourceFile.Read(buffer, 0, buffer.Length)) > 0)
        {
            encryptionStream.Write(buffer, 0, read);
        }

    }
    public void Decrypt(Stream sourceFile, Stream destinationFile, string key)
    {
        byte[] iv = new byte[Constants.KeySize];
        using var aes = Aes.Create();
        var bytes = Encoding.UTF8.GetBytes(key);
        aes.Key = bytes;
        aes.IV = iv;

        ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        using CryptoStream decryptionStream = new CryptoStream(destinationFile, decryptor, CryptoStreamMode.Write,leaveOpen:true);

        byte[] buffer = new byte[sourceFile.Length];
        int read;
        while ((read = sourceFile.Read(buffer, 0, buffer.Length)) > 0)
        {
            decryptionStream.Write(buffer, 0, read);
        }

    }

}
