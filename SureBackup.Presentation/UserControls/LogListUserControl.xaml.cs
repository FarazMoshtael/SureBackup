using SureBackup.Presentation.ViewModels.UserControls;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SureBackup.Presentation.UserControls
{
    /// <summary>
    /// Interaction logic for LogListUserControl.xaml
    /// </summary>
    public partial class LogListUserControl : UserControl
    {
        private LogListViewModel _viewModel;
        public LogListUserControl(LogListViewModel viewModel)
        {
            InitializeComponent();
            DataContext= _viewModel=viewModel;
            _viewModel.Initialize();
            IsVisibleChanged += OnVisibilityChanged;

        }
        private void OnVisibilityChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == true)
                _viewModel.Initialize();
        }
    }
}
