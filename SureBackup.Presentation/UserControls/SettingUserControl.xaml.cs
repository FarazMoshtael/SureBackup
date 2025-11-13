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
    /// Interaction logic for SettingUserControl.xaml
    /// </summary>
    public partial class SettingUserControl : UserControl
    {
        private SettingViewModel _viewModel;
        public SettingUserControl(SettingViewModel viewModel)
        {
            InitializeComponent();
            DataContext = _viewModel = viewModel;
            _viewModel.SetupPasswordInputs = (setting) =>
            {
                EncryptionKeyPasswordBox.Password = setting?.BackupKey;
                FTPUrlBox.Text = setting?.FTPUrl;
                FTPUsernamePasswordBox.Password = setting?.FTPUsername;
                FTPPasswordBox.Password = setting?.FTPPassword;
            };
            _viewModel.Initialize();
        }

        private void EncryptionKey_Changed(object sender, RoutedEventArgs e)
        {
            if (_viewModel.BackupSetting is not null)
                _viewModel.BackupSetting!.BackupKey = (sender as PasswordBox)!.Password;
        }

        private void FTPUrl_Changed(object sender, RoutedEventArgs e)
        {

        }
        private void FTPUsername_Changed(object sender, RoutedEventArgs e)
        {
            if (_viewModel.BackupSetting is not null)
                _viewModel.BackupSetting!.FTPUsername = (sender as PasswordBox)!.Password;
        }
        private void FTPPassword_Changed(object sender, RoutedEventArgs e)
        {
            if (_viewModel.BackupSetting is not null)
                _viewModel.BackupSetting!.FTPPassword = (sender as PasswordBox)!.Password;
        }
    }
}
