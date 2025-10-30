
using SureBackup.Presentation.ViewModels;
using System.Windows.Controls;

namespace SureBackup.Presentation.Abstraction;

public interface ISidebarNavigationService
{
    void SetupWindowContentSwitch(Action<UserControl>? setContentAction);
    void NavigateTab<ViewModel>() where ViewModel : BaseViewModel;
}
