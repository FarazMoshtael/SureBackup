

namespace SureBackup.Domain.HelperModels;

public record FTPFileInfo
{
    public required string Name { get; init; }
    public long Size { get; init; }
    public DateTime Modified { get; init; }
}
