namespace ETaskManagement.Domain.Common.PageList;

public class PagedList<T> : List<T>
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int Limit { get; set; }
    public int TotalCount { get; set; }
    public List<T> Data { get; set; }
    public bool HasPreviousPage => CurrentPage > 1;
    public bool HasNextPage => CurrentPage < TotalPages;
    public int? NextPageNumber => HasNextPage ? CurrentPage + 1 : (int?)null;
    public int? PreviousPageNumber => HasPreviousPage ? CurrentPage - 1 : (int?)null;

    public PagedList(List<T> items, int count, int page, int limit)
    {
        TotalCount = count;
        Limit = limit;
        CurrentPage = page;
        TotalPages = (int)Math.Ceiling(count / (double)limit); 
        Data = items;
    }

    public static PagedList<T> Create(IEnumerable<T> source, int page, int limit)
    {
        page = page == 0 ? 1 : page;
        limit = limit == 0 ? 10 : limit;
        
        var count = source.Count();
        var items = source.Skip((page - 1) * limit).Take(limit).ToList();

        return new PagedList<T>(items, count, page, limit);
    }
}