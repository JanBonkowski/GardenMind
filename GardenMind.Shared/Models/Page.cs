namespace GardenMind.Shared.Models;

public sealed class Page<T> where T : class
{
    private Page(IEnumerable<T> items, int number, int size, int totalPages)
    {
        Items = items;
        PageNumber = number;
        PageSize = size;
        TotalPages = totalPages;
    }

    public IEnumerable<T> Items { get; private set; } = [];
    public int PageNumber { get; private set; }
    public int PageSize { get; private set; }
    public int TotalPages { get; private set; }

    public static Page<T> Empty() => new([], 0, 0, 0);

    public static Page<T> From(IEnumerable<T> items, PageRequest request, int totalNumberOfResults)
    {
        var totalPages = request.PageSize > 0 ? totalNumberOfResults / request.PageSize : 0;

        return new Page<T>(items, request.PageNumber, request.PageSize, totalPages);
    }
}