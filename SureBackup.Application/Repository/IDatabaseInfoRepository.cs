

using SureBackup.Domain.Entities;

namespace SureBackup.Application.Repository;

public interface IDatabaseInfoRepository:IBaseRepository<DatabaseInfo>
{
    public List<DatabaseInfo> GetAllDatabases(bool? isActive=null);
}
