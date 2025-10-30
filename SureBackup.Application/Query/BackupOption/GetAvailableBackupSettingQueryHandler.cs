
using MediatR;
using SureBackup.Application.Repository;
using SureBackup.Domain.Entities;

namespace SureBackup.Application.Query.BackupOption;

public class GetAvailableBackupSettingQueryHandler(IBackupSettingRepository backupSettingRepository) : IRequestHandler<GetAvailableBackupSettingQuery, BackupSetting>
{
    public Task<BackupSetting> Handle(GetAvailableBackupSettingQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(backupSettingRepository.GetAvailableBackupSetting());
    }
}
