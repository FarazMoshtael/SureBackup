

using Azure;
using Microsoft.EntityFrameworkCore;
using SureBackup.Application.Repository;
using SureBackup.Domain.Common;
using SureBackup.Domain.ExtensionMethods;
using SureBackup.Infrastructure.Database;

namespace SureBackup.Infrastructure.Repository;

public abstract class BaseRepository<TModel, TKey> : IBaseRepository<TModel> where TModel : Entity
{
    protected BaseRepository(AppDBContext context)
    {
        Context = context;
        EntityDBSet = Context.Set<TModel>();
    }
    protected AppDBContext Context { get; set; }
    protected DbSet<TModel> EntityDBSet { get; set; }
    public abstract System.Linq.Expressions.Expression<Func<TModel, TKey>> GetKey();
    public async Task<TModel> AddAsync(TModel item)
    {
        EntityDBSet.Add(item);
        await Context.SaveChangesAsync();
        return item;
    }

    public async Task<IEnumerable<TModel>> AddRangeAsync(IEnumerable<TModel> items)
    {
        EntityDBSet.AddRange(items);
        await Context.SaveChangesAsync();
        return items;
    }

    public async Task<TModel> DeleteAsync(TModel item)
    {
        EntityDBSet.Remove(item);
        await Context.SaveChangesAsync();
        return item;
    }

    public IQueryable<TModel> QueryableItems(int? pageSize = null, int? page = null,bool noTracking=false)
    {
        IQueryable<TModel> query = EntityDBSet.AsQueryable();
        if(noTracking)
            query=query.AsNoTracking();
        return CheckOrderPagination(query, pageSize, page);
    }

    private IQueryable<TModel> CheckOrderPagination(IQueryable<TModel> query,  int? pageSize = null, int? page = null)
    {
        if (pageSize.HasValue && page.HasValue)
            return query.OrderBy(GetKey()).CheckPagination(pageSize, page);

        return query;
    }

    public async Task<TModel> UpdateAsync(TModel item)
    {
        await Context.SaveChangesAsync();
        return item;
    }

    public async Task<TModel> SaveItemAsync(TModel item)
    {
        if (item.ID == default)
            EntityDBSet.Add(item);

        await Context.SaveChangesAsync();
        return item;
    }
}
