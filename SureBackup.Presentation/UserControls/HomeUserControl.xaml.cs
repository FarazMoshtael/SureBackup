using SureBackup.Presentation.ViewModels.UserControls;
using System.Windows;
using System.Windows.Controls;

namespace SureBackup.Presentation.UserControls
{
    /// <summary>
    /// Interaction logic for HomeUserControl.xaml
    /// </summary>
    public partial class HomeUserControl : UserControl
    {
        private HomeViewModel _viewModel;
        public HomeUserControl(HomeViewModel viewModel)
        {
            InitializeComponent();
            DataContext = _viewModel = viewModel;
            viewModel.Initialize();
            IsVisibleChanged += OnVisibilityChanged;
        }

        private void OnVisibilityChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == true)
                _viewModel.Initialize();
        }
    }
}
