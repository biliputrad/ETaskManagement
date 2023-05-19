namespace ETaskManagement.Contract.Common.Filter;

public class Filter
{
    public int? Page { get; set; } = null;
    public int? Limit { get; set; } = null;
    public string? Sort { get; set; } = null;
    public string? Field { get; set; } = null;
}