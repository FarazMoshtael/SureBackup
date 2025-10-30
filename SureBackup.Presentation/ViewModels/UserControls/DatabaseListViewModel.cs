

using CommunityToolkit.Mvvm.Input;
using MediatR;
using SureBackup.Application.Command.Database;
using SureBackup.Application.Query.BackupLog;
using SureBackup.Application.Query.Database;
using SureBackup.Domain.Entities;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SureBackup.Presentation.ViewModels.UserControls;

public class DatabaseListViewModel:BaseViewModel
{
    #region Properties
    public ObservableCollection<DatabaseInfo>? Databases { get; set; }
    public DatabaseInfo? SelectedDatabase { get; set; }
    #endregion

    #region Commands
    public ICommand? RowEditEndingCommand { get; set; }
    #endregion

    #region Services
    private IMediator? _mediator;
    #endregion

    public DatabaseListViewModel(IMediator mediator)
    {
        _mediator = mediator;
        RowEditEndingCommand = new AsyncRelayCommand<DatabaseInfo>(RowEditEnding);
        OnInitialized += async (sender, arg) =>
        {
            Databases = new ObservableCollection<DatabaseInfo>(await _mediator.Send(new GetDatabaseListQuery()));
        };

    }
    private async Task RowEditEnding(DatabaseInfo? databaseInfo)
    {
        if(databaseInfo is not null)
        await _mediator!.Send(new SaveDatabaseInfoCommand(databaseInfo!.ID, databaseInfo.Name, databaseInfo.Database, databaseInfo.ConnectionString!, databaseInfo.IsActive));
    }
}
