using SureBackup.Domain.Pattern;

namespace SureBackup.Application.Service.BackgroundService;

public interface IIntervalBackgroundService
{

    void Start(int intervalMs, Action backgroundAction);
    void Stop();
    bool IsRunning();
}
