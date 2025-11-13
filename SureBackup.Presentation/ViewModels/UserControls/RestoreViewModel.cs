
using CommunityToolkit.Mvvm.Input;
using MediatR;
using Microsoft.Win32;
using SureBackup.Application.Command.FileCryption;
using SureBackup.Domain.Enums;
using SureBackup.Domain.Pattern;
using SureBackup.Presentation.Abstraction;
using SureBackup.Presentation.Service;
using System.Windows.Input;

namespace SureBackup.Presentation.ViewModels.UserControls;

public class RestoreViewModel : BaseViewModel
{
    #region Properties
    private string? _sourceFile;
    public string? SourceFile { get => _sourceFile;
        set
        {
            _sourceFile = value;
            OnPropertyChanged();
        }
    }
    private string? _destinationPath;
    public string? DestinationPath { get => _destinationPath;
        set
        {
            _destinationPath = value;
            OnPropertyChanged();
        }
    }
    public Database DatabaseType { get; set; }
    #endregion

    #region Commands
    public ICommand? BrowseSourceFileCommand { get; set; }
    public ICommand? BrowseDestinationCommand { get; set; }
    public ICommand? RestoreCommand { get; set; }
    #endregion

    private IWindowNavigationService? _windowNavigationService;
    private IMediator _mediator;
    public RestoreViewModel(IMediator mediator, IWindowNavigationService windowNavigationService)
    {
        _mediator = mediator;
        _windowNavigationService = windowNavigationService;
        BrowseSourceFileCommand = new RelayCommand(BrowseSourceFile);
        BrowseDestinationCommand = new RelayCommand(BrowseDestination);
        RestoreCommand=new AsyncRelayCommand(Restore);
    }

    private void BrowseSourceFile()
    {
        OpenFileDialog openFileDialog = new OpenFileDialog()
        {
            Title = "Select backup file",
            InitialDirectory = Environment.CurrentDirectory,
            Multiselect = false
        };
        if (openFileDialog.ShowDialog() == true)
            SourceFile = openFileDialog.FileName;
    }

    private void BrowseDestination()
    {
        OpenFolderDialog openFolderDialog = new OpenFolderDialog()
        {
            Title = "Select destination directory",
            Multiselect = false
        };
        if (openFolderDialog.ShowDialog() == true)
            DestinationPath = openFolderDialog.FolderName;
    }

    private async Task Restore()
    {
        Result result = await _mediator.Send(new FileDecryptionCommand(SourceFile,DestinationPath,DatabaseType));
        if (result.Success)
        {
            SourceFile = string.Empty;
            DestinationPath = string.Empty;
        }
        _windowNavigationService?.ShowMessageDialog(result.Message!);

    }
}
