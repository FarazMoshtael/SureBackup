

using Microsoft.Extensions.Logging;
using SureBackup.Application.Service.BackgroundService;

namespace SureBackup.Infrastructure.Service.BackgroundService;

public class IntervalBackgroundService(ILogger<IntervalBackgroundService> logger) : IIntervalBackgroundService
{

    private CancellationTokenSource? cancellationTokenSource;
    private Task? worker;
    public void Start(int intervalMiliseconds, Action backgroundAction)
    {
        cancellationTokenSource = new();
        worker = RunAsync(intervalMiliseconds, backgroundAction,cancellationTokenSource.Token);
    }

    private async Task RunAsync(int intervalMiliseconds, Action backgroundAction, CancellationToken ct)
    {
        using var timer = new PeriodicTimer(TimeSpan.FromMilliseconds(intervalMiliseconds));
        while (await timer.WaitForNextTickAsync(ct))
        {
            try
            {
                backgroundAction?.Invoke();
                logger.LogInformation("Background task completed.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Background task error: {ex.Message}");
            }
        }
    }
    public bool IsRunning() => worker?.IsCompleted??false;
    public void Stop() => cancellationTokenSource?.Cancel();
}
