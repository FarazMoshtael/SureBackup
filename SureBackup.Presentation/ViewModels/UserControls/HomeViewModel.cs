

using CommunityToolkit.Mvvm.Input;
using MediatR;
using SureBackup.Application.Query.BackupLog;
using SureBackup.Application.Query.BackupOption;
using SureBackup.Application.Service.BackgroundService;
using SureBackup.Application.Service.BackupProcess;
using SureBackup.Domain.Common;
using SureBackup.Domain.Entities;
using System.Windows;
using System.Windows.Input;

namespace SureBackup.Presentation.ViewModels.UserControls;

public class HomeViewModel : BaseViewModel
{
    #region Properties
    private bool _runButtonEnabled = true;
    public bool RunButtonEnabled
    {
        get => _runButtonEnabled;
        set
        {
            _runButtonEnabled = value;
            OnPropertyChanged();
          

        }
    }

    private bool _stopButtonEnabled = false;
    public bool StopButtonEnabled
    {
        get => _stopButtonEnabled;
        set
        {
            _stopButtonEnabled = value;
            OnPropertyChanged();
           

        }
    }



    public BackupSetting? BackupSetting{ get; set; }
    private Log? _recentLog;

    public string EncryptionStatus => BackupSetting?.EncryptionAvailable == true ? Constants.Available : Constants.NotAvailable;
    public string FTPStatus => BackupSetting?.FTPCredentialsAvailable == true ? Constants.Available : Constants.NotAvailable;
    public string LastBackup => _recentLog is null ? Constants.NotAvailable : $"Database => {_recentLog.DatabaseInfo!.Name} , Date => {_recentLog.Date.ToString(Constants.DefaultDateFormat)}\n{_recentLog.Message}";

    private string _status = string.Empty;
    public string Status
    {
        get => _status;
        set
        {
            _status = value;
            OnPropertyChanged();
        }
    }

    private double _progressValue;
    public double ProgressValue
    {
        get => _progressValue;
        set
        {
            _progressValue = value;
            OnPropertyChanged();
        }
    }
    private Visibility _backupProcessStatusVisibility = Visibility.Collapsed;
    public Visibility BackupProcessStatusVisibility
    {
        get => _backupProcessStatusVisibility;
        set
        {
            _backupProcessStatusVisibility = value;
            OnPropertyChanged();
        }
    }
    #endregion

    #region Commands
    public ICommand? RunBackupProcessCommand { get; set; }
    public ICommand? StopBackupProcessCommand { get; set; }
    #endregion

    #region Services
    private IMediator _mediator;
    private IIntervalBackgroundService _intervalBackgroundService;
    private IBackupRunnerService _backupRunnerService;
    #endregion

    public HomeViewModel(IMediator mediator, IIntervalBackgroundService intervalBackgroundService, IBackupRunnerService backupRunnerService)
    {
        _mediator = mediator;
        _intervalBackgroundService = intervalBackgroundService;
        _backupRunnerService = backupRunnerService;
        RunBackupProcessCommand = new RelayCommand(RunBackupProcess);
        StopBackupProcessCommand = new RelayCommand(StopBackupProcess);
        OnInitialized += async (sender, arg) =>
        {
            BackupSetting = await GetAvailableSetting();
            OnPropertyChanged(nameof(BackupSetting));
            _recentLog = await GetRecentLog();

        };
    }


    private async Task<BackupSetting> GetAvailableSetting() => await _mediator.Send(new GetAvailableBackupSettingQuery());
    private async Task<Log?> GetRecentLog() => await _mediator.Send(new GetRecentLogQuery());

    private void RunBackupProcess()
    {
        _intervalBackgroundService.Start(BackupSetting!.IntervalMiliseconds, () =>
        {
            _backupRunnerService.RunBackupProcess((progress) =>
            {
                ProgressValue = progress;
            }, (status) =>
            {
                Status = status;
            });
        });
        RunButtonEnabled = false;
        StopButtonEnabled = true;
        BackupProcessStatusVisibility = Visibility.Visible;
    }

    private void StopBackupProcess()
    {
        _intervalBackgroundService.Stop();
        RunButtonEnabled = true;
        StopButtonEnabled = false;
        BackupProcessStatusVisibility = Visibility.Collapsed;

    }

     
}
