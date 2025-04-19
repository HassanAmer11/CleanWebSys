namespace ECommerce.Core.Common.Pagination;

public class PaginatedResult<T>
{

    public PaginatedResult(List<T> data)
    {
        Data = data;
    }
    public PaginatedResult(bool succeeded, List<T> data, List<string> messages = null, int count = 0, int page = 1, int pageSize = 10)
    {
        Succeeded = succeeded;
        Data = data;   // Default to an empty list if data is null
        Messages = messages; // Default to an empty list if messages are null
        Count = count;
        CurrentPage = page;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling((double)count / pageSize); // Calculate total pages
        TotalCount = count;

    }
    public static PaginatedResult<T> Success(List<T> data, int count, int page, int pageSize)
    {

        return new(true, data, null, count, page, pageSize);
    }
    public bool Succeeded { get; set; }        // Whether the operation was successful
    public List<string> Messages { get; set; }  // List of messages (e.g., error or informational messages)
    public List<T> Data { get; set; }  // The actual data for the current page
    public int Count { get; set; }              // Total number of items available
    public int CurrentPage { get; set; }               // Current page number
    public int PageSize { get; set; }           // Number of items per page
    public int TotalPages { get; set; }         // Total number of pages
    public int TotalCount { get; set; }
    public object Meta { get; set; }
    public bool HasPreviousPage => CurrentPage > 1;
    public bool HasNextPage => CurrentPage < TotalPages;
}
