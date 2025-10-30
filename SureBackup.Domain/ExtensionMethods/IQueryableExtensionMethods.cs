
using System.Runtime.CompilerServices;

namespace SureBackup.Domain.ExtensionMethods;

public static class IQueryableExtensionMethods
{
    public static IQueryable<T> CheckPagination<T>(this IQueryable<T> query,int? PageSize,int? Page) where T : class
    {
        if(!PageSize.HasValue || !Page.HasValue) return query;
        return query.Skip(PageSize.Value * Page.Value).Take(PageSize.Value);
    }
}
