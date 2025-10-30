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
    /// Interaction logic for DatabaseListUserControl.xaml
    /// </summary>
    public partial class DatabaseListUserControl : UserControl
    {

        public DatabaseListUserControl(DatabaseListViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            viewModel.Initialize();
        }
    }
}
