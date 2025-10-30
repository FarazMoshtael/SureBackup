

namespace SureBackup.Application.Service.Wrapper;

public interface IDirectoryWrapper
{
    bool Exists(string path);
    void CreateDirectory(string path);
}
