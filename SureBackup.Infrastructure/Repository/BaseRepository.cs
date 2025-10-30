

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

    public IQueryable<TModel> QueryableItems(int? PageSize = null, int? Page = null)
    {
        return EntityDBSet.OrderBy(GetKey()).CheckPagination(PageSize, Page);
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
