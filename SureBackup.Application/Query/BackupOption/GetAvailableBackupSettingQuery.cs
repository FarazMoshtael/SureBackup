

using MediatR;
using SureBackup.Domain.Entities;

namespace SureBackup.Application.Query.BackupOption;

public record GetAvailableBackupSettingQuery:IRequest<BackupSetting>;
