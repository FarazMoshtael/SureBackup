
using SureBackup.Application.Service.Wrapper;

namespace SureBackup.Infrastructure.Service.Wrapper;

public class FileWrapper : IFileWrapper
{
    public Stream CreateFile(string path)=>File.Create(path);

    public void DeleteFile(string path)=>File.Delete(path);

    public bool Exists(string path)=>File.Exists(path);

    public string GetFileNameExtension(string path)=>Path.GetExtension(path);
    public string GetFileNameWithoutExtension(string path)=>Path.GetFileNameWithoutExtension(path);

    public Stream OpenReadFile(string path)=>File.OpenRead(path);
    public long GetFileSize(string path) =>new FileInfo(path).Length;
}
