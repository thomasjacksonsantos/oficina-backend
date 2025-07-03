namespace Oficina.Domain.ValueObjects;

public class PagedResult<T>
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalRecords { get; set; }
    public int TotalPages { get => Convert.ToInt32(Math.Ceiling(TotalRecords / (double)PageSize)); }
    public IEnumerable<T> Records { get; set; } = new List<T>();

    public PagedResult<TTarget> MapTo<TTarget>(Func<T, TTarget> mapper)
    {
        return new PagedResult<TTarget> 
        {
            CurrentPage = this.CurrentPage,
            PageSize = this.PageSize,
            TotalRecords = this.TotalRecords,
            Records = this.Records.Select(mapper)
        };
    }
}