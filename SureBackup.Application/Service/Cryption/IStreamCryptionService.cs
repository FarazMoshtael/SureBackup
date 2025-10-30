namespace SureBackup.Application.Service.Cryption;

public interface IStreamCryptionService
{
    public void Encrypt(Stream sourceFile, Stream destinationFile, string key);
    public void Decrypt(Stream sourceFile, Stream destinationFile, string key);

}
