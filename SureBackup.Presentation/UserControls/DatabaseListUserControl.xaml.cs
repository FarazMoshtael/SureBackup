using SureBackup.Domain.Entities;
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
        private DatabaseListViewModel _viewModel;
        public DatabaseListUserControl(DatabaseListViewModel viewModel)
        {
            InitializeComponent();
            DataContext = _viewModel = viewModel;
            viewModel.Initialize();
        }

        private async void DatabaseGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                var selectedItem = (sender as DataGrid)!.SelectedItem;
               await _viewModel.DeleteDatabase(selectedItem as DatabaseInfo);
            }
        }
    }
}
