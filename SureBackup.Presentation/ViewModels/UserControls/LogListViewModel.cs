
using MediatR;
using SureBackup.Application.Query.BackupLog;
using SureBackup.Domain.Entities;
using System.Collections.ObjectModel;

namespace SureBackup.Presentation.ViewModels.UserControls;

public class LogListViewModel : BaseViewModel
{
    public ObservableCollection<Log>? Logs { get; set; }

    private IMediator _mediator;

    public LogListViewModel(IMediator mediator)
    {
        _mediator = mediator;
        OnInitialized += async (s, e) =>
        {
            await FetchLogs();
        };
    }

    private async Task FetchLogs()
    {
        Logs = new ObservableCollection<Log>(await _mediator.Send(new GetLogListQuery()));
        OnPropertyChanged(nameof(Logs));
    }
}
