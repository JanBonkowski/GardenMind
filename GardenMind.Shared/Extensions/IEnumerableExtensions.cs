using GardenMind.Shared.Models;

namespace GardenMind.Shared.Extensions;

public static class IEnumerableExtensions
{
    public static IEnumerable<T> Page<T>(this IEnumerable<T> collection, PageRequest pageRequest)
    {
        if (pageRequest.PageSize == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(pageRequest.PageSize));
        }

        return collection.Skip(pageRequest.PageNumber * pageRequest.PageSize).Take(pageRequest.PageSize);
    }
}