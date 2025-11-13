using SureBackup.Presentation.ViewModels.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SureBackup.Presentation.Windows
{
    /// <summary>
    /// Interaction logic for MessageBoxWindow.xaml
    /// </summary>
    public partial class MessageBoxWindow : Window
    {
        private MessageBoxViewModel _viewModel;
        public MessageBoxWindow(MessageBoxViewModel viewModel)
        {
            InitializeComponent();
            Owner = App.Current.MainWindow;
            DataContext = _viewModel = viewModel;
            viewModel.ShowWindow = () =>
            {
                if (!_viewModel.WasShown)
                {
                    Show();
                    viewModel.WasShown = true;
                }
                viewModel.WindowVisibility = Visibility.Visible;
            };
            viewModel.Initialize();
        }
    }
}
