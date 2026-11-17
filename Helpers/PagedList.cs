namespace WebVanChuyenLogicstic.Helpers;

public interface IPagedList
{
    int PageIndex { get; }
    int PageSize { get; }
    int TotalCount { get; }
    int TotalPages { get; }
    string PageParam { get; }
    string? Fragment { get; }
}

public class PagedList<T> : List<T>, IPagedList
{
    public int PageIndex { get; }
    public int PageSize { get; }
    public int TotalCount { get; }
    public int TotalPages => Math.Max(1, (int)Math.Ceiling(TotalCount / (double)PageSize));
    public string PageParam { get; }
    public string? Fragment { get; }

    public PagedList(IEnumerable<T> items, int pageIndex, int pageSize, int totalCount, string pageParam, string? fragment)
        : base(items)
    {
        PageIndex = pageIndex; PageSize = pageSize; TotalCount = totalCount;
        PageParam = pageParam; Fragment = fragment;
    }
}

public static class PagedListExtensions
{
    public const int DefaultPageSize = 10;

    public static PagedList<T> ToPagedList<T>(this IQueryable<T> source, int? page,
        int pageSize = DefaultPageSize, string pageParam = "page", string? fragment = null)
    {
        var total = source.Count();
        var index = Clamp(page, total, pageSize);
        var items = source.Skip((index - 1) * pageSize).Take(pageSize).ToList();
        return new PagedList<T>(items, index, pageSize, total, pageParam, fragment);
    }

    public static PagedList<T> ToPagedList<T>(this IEnumerable<T> source, int? page,
        int pageSize = DefaultPageSize, string pageParam = "page", string? fragment = null)
    {
        var all = source as IList<T> ?? source.ToList();
        var index = Clamp(page, all.Count, pageSize);
        return new PagedList<T>(all.Skip((index - 1) * pageSize).Take(pageSize), index, pageSize, all.Count, pageParam, fragment);
    }

    private static int Clamp(int? page, int total, int pageSize)
    {
        var pages = Math.Max(1, (int)Math.Ceiling(total / (double)pageSize));
        return Math.Clamp(page ?? 1, 1, pages);
    }
}