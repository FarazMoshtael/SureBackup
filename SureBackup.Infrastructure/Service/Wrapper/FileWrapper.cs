
using SureBackup.Application.Service.Wrapper;

namespace SureBackup.Infrastructure.Service.Wrapper;

public class FileWrapper : IFileWrapper
{
    public Stream CreateFile(string path)=>File.Create(path);

    public void DeleteFile(string path)=>File.Delete(path);

    public string GetFileNameWithoutExtension(string path)=>Path.GetFileNameWithoutExtension(path);

    public Stream OpenReadFile(string path)=>File.OpenRead(path);
}
