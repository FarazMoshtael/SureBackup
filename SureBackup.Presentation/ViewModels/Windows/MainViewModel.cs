
using CommunityToolkit.Mvvm.Input;
using SureBackup.Presentation.Abstraction;
using SureBackup.Presentation.ViewModels.UserControls;
using System.Windows.Controls;
using System.Windows.Input;

namespace SureBackup.Presentation.ViewModels.Windows;

public class MainViewModel : BaseViewModel
{
    private ISidebarNavigationService _sidebarNavigationService;
    public Action<UserControl>? SetupSidebarWindowContentSwitch;

    public ICommand? MainViewCommand { get; set; }
    public ICommand? SettingViewCommand { get; set; }
    public ICommand? DatabaseListViewCommand { get; set; }
    public ICommand? LogListViewCommand { get; set; }
    public ICommand? RestoreViewCommand { get; set; }

    public MainViewModel(ISidebarNavigationService sidebarNavigationService)
    {
        _sidebarNavigationService = sidebarNavigationService;
        OnInitialized += (sender,arg)=>
        {
            _sidebarNavigationService.SetupWindowContentSwitch(SetupSidebarWindowContentSwitch);
            _sidebarNavigationService.NavigateTab<HomeViewModel>();
            MainViewCommand = new RelayCommand(MainViewNavigation);
            SettingViewCommand = new RelayCommand(SettingViewNavigation);
            DatabaseListViewCommand = new RelayCommand(DatabaseListViewNavigation);
            LogListViewCommand = new RelayCommand(LogListViewNavigation);
            RestoreViewCommand = new RelayCommand(RestoreViewNavigation);

        };
        
    }

    private void MainViewNavigation()
    {
        _sidebarNavigationService.NavigateTab<HomeViewModel>();
    }

    private void SettingViewNavigation()
    {
        _sidebarNavigationService.NavigateTab<SettingViewModel>();

    }

    private void DatabaseListViewNavigation()
    {
        _sidebarNavigationService.NavigateTab<DatabaseListViewModel>();

    }
    private void LogListViewNavigation()
    {
        _sidebarNavigationService.NavigateTab<LogListViewModel>();
    }
    private void RestoreViewNavigation()
    {
        _sidebarNavigationService.NavigateTab<RestoreViewModel>();
    }
}
