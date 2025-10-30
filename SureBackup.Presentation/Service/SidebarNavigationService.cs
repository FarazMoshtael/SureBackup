
using Microsoft.Extensions.DependencyInjection;
using SureBackup.Presentation.Abstraction;
using SureBackup.Presentation.ViewModels;
using SureBackup.Presentation.Windows;
using System.Reflection;
using System.Windows.Controls;

namespace SureBackup.Presentation.Service;

public class SidebarNavigationService : ISidebarNavigationService
{
    private Action<UserControl>? _setContentAction;
    public void NavigateTab<ViewModel>() where ViewModel : BaseViewModel
    {

        string userControlTypeTitle = typeof(ViewModel).Name.Replace("ViewModel", "UserControl");
        Type? userControlType = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(item=>item.Name== userControlTypeTitle);
        if (userControlType is not null)
        {
            var userControl = App.AppHost!.Services.GetRequiredService(userControlType) as UserControl;
            _setContentAction?.Invoke(userControl!);
        }

    }

    public void SetupWindowContentSwitch(Action<UserControl>? setContentAction)
    {
        _setContentAction = setContentAction;
    }
}
