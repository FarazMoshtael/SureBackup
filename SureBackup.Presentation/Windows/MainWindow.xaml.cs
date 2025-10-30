using SureBackup.Presentation.ViewModels.Windows;
using System.Windows;
using System.Windows.Controls;


namespace SureBackup.Presentation.Windows;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow(MainViewModel viewModel)
    {
        InitializeComponent();
        DataContext=viewModel;

        viewModel.SetupSidebarWindowContentSwitch= (uc) =>
        {
            ContentPresenter.Content = uc;
        };
        viewModel.Initialize();
    }
}