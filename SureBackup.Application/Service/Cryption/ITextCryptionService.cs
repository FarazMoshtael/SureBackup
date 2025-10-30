namespace SureBackup.Application.Service.Cryption;

public interface ITextCryptionService
{
    public string Encrypt(string plainText);
    public string Decrypt(string cipherText);

}
