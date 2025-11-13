

using Microsoft.Extensions.DependencyInjection;
using SureBackup.Presentation.Abstraction;
using SureBackup.Presentation.ViewModels.Windows;
using SureBackup.Presentation.Windows;

namespace SureBackup.Presentation.Service;

public class WindowNavigationService : IWindowNavigationService
{
    public void ShowMessageDialog(string message, string? title = null)
    {

        var messageBoxWindow = App.AppHost!.Services.GetRequiredService<MessageBoxWindow>();

        if (messageBoxWindow is not null)
        {
            var messageBoxViewModel = App.AppHost!.Services.GetRequiredService<MessageBoxViewModel>();
            messageBoxViewModel.Title = title;
            messageBoxViewModel.Message = message;
            messageBoxViewModel.ShowWindow?.Invoke();
        }


    }
}
