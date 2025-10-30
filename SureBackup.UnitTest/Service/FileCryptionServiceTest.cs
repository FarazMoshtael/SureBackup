
using FluentAssertions;
using SureBackup.Infrastructure.Service.Cryption;
using System.Security.Cryptography;
using System.Text;

namespace SureBackup.UnitTest.Service;

public class FileCryptionServiceTest
{
    [Theory]
    [InlineData("123456789", "SampleKeyTesting")]
    public void EncryptAndDecrypt_ShouldReturnOriginalData(string content, string key)
    {
        //Arrange
        StreamCryptionService service = new StreamCryptionService();

        byte[] originalBytes = Encoding.UTF8.GetBytes(content);

        using MemoryStream input = new MemoryStream(originalBytes);
        using MemoryStream encryptedDestination = new MemoryStream();
        using MemoryStream decryptedDestination = new MemoryStream();
        //Act
        service.Encrypt(input, encryptedDestination, key);
        encryptedDestination.Position = 0;
        service.Decrypt(encryptedDestination, decryptedDestination, key);

        //Assert
        byte[] decryptedBytes = decryptedDestination.ToArray();
        string decryptedText = Encoding.UTF8.GetString(decryptedBytes);

        decryptedText.Should().Be(content);

    }

    [Theory]
    [InlineData("123456789", "SampleKeyTesting")]
    public void DecryptByWrongKey_ShouldThrowException(string content, string key)
    {
        //Arrange
        StreamCryptionService service = new();
        byte[] contentBytes = Encoding.UTF8.GetBytes(content);
        using MemoryStream input = new MemoryStream(contentBytes);
        using MemoryStream encrypted = new MemoryStream();
        using MemoryStream decrypted = new MemoryStream();

        //Act
        service.Encrypt(input, encrypted, key);
        encrypted.Position = 0;
        Action decryption = () => service.Decrypt(encrypted, decrypted, "WrongKeyTesting");

        //Assert
        decryption.Should().Throw<CryptographicException>();

    }

}
