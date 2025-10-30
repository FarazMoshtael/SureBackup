

using SureBackup.Application.Repository;
using SureBackup.Application.Service.Cryption;
using SureBackup.Domain.Entities;
using SureBackup.Infrastructure.Database;
using System.Linq.Expressions;

namespace SureBackup.Infrastructure.Repository;

public class DatabaseInfoRepository(AppDBContext context, ITextCryptionService textCryptionService) : BaseRepository<DatabaseInfo, int>(context), IDatabaseInfoRepository
{

    public override Expression<Func<DatabaseInfo, int>> GetKey()
    {
        return item => item.ID;
    }
    public List<DatabaseInfo> GetAllDatabases(bool? isActive=null)
    {
        var activeDatabases = EntityDBSet.Where(item => isActive == null || item.IsActive==isActive).ToList();
        activeDatabases.ForEach(database => database.ConnectionString = textCryptionService.Decrypt(database.EncryptedConnectionString));
        return activeDatabases;
    }

}
