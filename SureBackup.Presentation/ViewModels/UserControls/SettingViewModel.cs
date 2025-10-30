

using CommunityToolkit.Mvvm.Input;
using MediatR;
using Microsoft.Win32;
using SureBackup.Application.Command.BackupOption;
using SureBackup.Application.Query.BackupOption;
using SureBackup.Domain.Common;
using SureBackup.Domain.Entities;
using SureBackup.Domain.Enums;
using System.Windows.Input;

namespace SureBackup.Presentation.ViewModels.UserControls;

public class SettingViewModel : BaseViewModel
{
    #region Properties

    private BackupSetting? _backupSetting;
    public BackupSetting? BackupSetting
    {
        get => _backupSetting;
        set
        {
            _backupSetting = value;
            OnPropertyChanged();
        }
    }

    private IntervalUnit _intervalUnit = IntervalUnit.Hour;
    public IntervalUnit IntervalUnit
    {
        get => _intervalUnit;
        set
        {
            _intervalUnit = value;
            OnPropertyChanged();
            Interval = 1;
        }
    }
    private double _interval = Constants.Setting.DefaultInterval;
    public double Interval
    {
        get => _interval;
        set
        {
            _interval = value;
            OnPropertyChanged();
            if (BackupSetting is not null)
                BackupSetting.IntervalMiliseconds = IntervalUnit switch
                {
                    IntervalUnit.Minute => Convert.ToInt32(_interval * Constants.MinuteMilisecond),
                    IntervalUnit.Hour => Convert.ToInt32(_interval * Constants.HourMilisecond),
                    IntervalUnit.Day => Convert.ToInt32(_interval * Constants.DayMilisecond),
                    _ => Convert.ToInt32(_interval)
                };
        }
    }

    private bool _useEncryption;
    public bool UseEncryption
    {
        get => _useEncryption;
        set
        {
            _useEncryption = value;
            OnPropertyChanged();
            if (BackupSetting is not null)
                BackupSetting.EncryptionBackup = _useEncryption;
        }
    }

    private bool _ftpUpload;
    public bool FTPUpload
    {
        get => _ftpUpload;
        set
        {
            _ftpUpload = value;
            OnPropertyChanged();
            if (BackupSetting is not null)
                BackupSetting.FTPUpload = _ftpUpload;
        }
    }
    #endregion

    #region Commands
    public ICommand? BrowseDirectoryCommand { get; set; }
    public ICommand? SaveSettingCommand { get; set; }
    #endregion


    #region Services
    private IMediator? _mediator;
    #endregion


    public SettingViewModel(IMediator mediator)
    {
        _mediator = mediator;
        BrowseDirectoryCommand = new RelayCommand(BrowseDirectory);
        SaveSettingCommand = new AsyncRelayCommand(SaveSetting);
        OnInitialized += async (sender, arg) => await RetrieveSetting();

    }

    private void BrowseDirectory()
    {
        OpenFolderDialog openFolderDialog = new OpenFolderDialog
        {
            Title = "Select backup operation directory",
            InitialDirectory = Environment.CurrentDirectory,
            Multiselect = false
        };
        if (openFolderDialog?.ShowDialog() == true)
            BackupSetting!.BackupOperationPath = openFolderDialog.FolderName;
    }

    private async Task RetrieveSetting()
    {
        BackupSetting = await _mediator!.Send(new GetAvailableBackupSettingQuery());
        _intervalUnit = BackupSetting.IntervalMiliseconds switch
        {
            >= Constants.DayMilisecond * 2 => IntervalUnit.Day,
            < Constants.DayMilisecond * 2 and >= Constants.HourMilisecond * 2 => IntervalUnit.Hour,
            < Constants.HourMilisecond * 2 => IntervalUnit.Minute

        };
        _interval = IntervalUnit switch
        {
            IntervalUnit.Minute => BackupSetting.IntervalMiliseconds / Constants.MinuteMilisecond,
            IntervalUnit.Hour => BackupSetting.IntervalMiliseconds / Constants.HourMilisecond,
            IntervalUnit.Day => BackupSetting.IntervalMiliseconds / Constants.DayMilisecond,
            _ => BackupSetting.IntervalMiliseconds
        };
        _useEncryption = BackupSetting.EncryptionBackup;
        OnPropertyChanged(nameof(IntervalUnit));
        OnPropertyChanged(nameof(Interval));
        OnPropertyChanged(nameof(UseEncryption));


    }

    private async Task SaveSetting()
    {
        await _mediator!.Send(new SaveBackupSettingCommand(BackupSetting!.IntervalMiliseconds, BackupSetting.BackupOperationPath, BackupSetting.HostSizeInBytes,
            BackupSetting.FTPUpload, BackupSetting.FTPUrl, BackupSetting.FTPUsername, BackupSetting.FTPPassword, BackupSetting.BackupKey, BackupSetting.EncryptionBackup));
    }
}
