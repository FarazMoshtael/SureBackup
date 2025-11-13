
namespace SureBackup.Application.Repository;

public interface IBaseRepository<TModel> where TModel : class
{
    public Task<TModel> AddAsync(TModel item);
    public Task<TModel> UpdateAsync(TModel item);
    public Task<TModel> DeleteAsync(TModel item);
    public IQueryable<TModel> QueryableItems(int? pageSize = null, int? page = null,bool noTracking=false);
    public Task<TModel> SaveItemAsync(TModel item);
}
