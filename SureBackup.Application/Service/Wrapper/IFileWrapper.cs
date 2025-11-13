

namespace SureBackup.Application.Service.Wrapper;

public interface IFileWrapper
{
    Stream OpenReadFile(string path);
    Stream CreateFile(string path);
    void DeleteFile(string path);
    string GetFileNameWithoutExtension(string path);
    string GetFileNameExtension(string path);
    bool Exists(string path);
    public long GetFileSize(string path);
}
