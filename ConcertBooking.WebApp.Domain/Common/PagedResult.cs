namespace ConcertBooking.WebApp.Domain.Common;

/// <summary>
/// Represents a paginated result set.
/// </summary>
/// <typeparam name="TResult">The type of the items in the result set.</typeparam>
/// <param name="items">The list of items in the result set.</param>
/// <param name="totalCount">The total number of items in the result set.</param>
/// <param name="page">The current page number.</param>
/// <param name="pageSize">The size of each page.</param>
public sealed class PagedResult<TResult>(List<TResult> items, int totalCount, int page, int pageSize)
{
    public List<TResult> Items { get; } = items;
    public int TotalCount { get; } = totalCount;
    public int Page { get; } = page;
    public int PageSize { get; } = pageSize;
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    public bool HasNextPage => Page < TotalPages;
    public bool HasPreviousPage => Page > 1;
}