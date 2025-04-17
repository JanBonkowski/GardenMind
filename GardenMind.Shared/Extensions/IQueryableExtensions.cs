using GardenMind.Shared.Models;

namespace GardenMind.Shared.Extensions;

public static class IQueryableExtensions
{
    public static IQueryable<T> Page<T>(this IQueryable<T> collection, PageRequest pageRequest)
    {
        if (pageRequest.PageSize == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(pageRequest.PageSize));
        }

        return collection.Skip(pageRequest.PageNumber * pageRequest.PageSize).Take(pageRequest.PageSize);
    }
}