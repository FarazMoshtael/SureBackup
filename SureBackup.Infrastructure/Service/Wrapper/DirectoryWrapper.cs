

using SureBackup.Application.Service.Wrapper;

namespace SureBackup.Infrastructure.Service.Wrapper;

public class DirectoryWrapper : IDirectoryWrapper
{
    public void CreateDirectory(string path)=>Directory.CreateDirectory(path);
    public bool Exists(string path)=>Directory.Exists(path);
}
