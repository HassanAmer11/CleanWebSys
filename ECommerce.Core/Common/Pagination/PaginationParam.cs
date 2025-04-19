namespace ECommerce.Core.Common.Pagination;

public class PaginationParam : IPaginationParam
{
    private const int MaxPageSize = 100000;
    public int PageNumber { get; set; } = 1;
    private int pageSize = 10;
    public int PageSize
    {
        get { return pageSize; }
        set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
    }

    public string FilterValue { get; set; } = "";
    public string FilterType { get; set; }

    public string UserType { get; set; }
    public string SortType { get; set; }
    public int? AccountId { get; set; }
    public List<int> BranchId { get; set; }

}
