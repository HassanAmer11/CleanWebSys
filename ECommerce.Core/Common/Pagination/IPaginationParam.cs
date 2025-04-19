namespace ECommerce.Core.Common.Pagination;

public interface IPaginationParam
{
    int PageNumber { get; set; }
    int PageSize { get; set; }
    string FilterValue { get; set; }
    string FilterType { get; set; }
    string UserType { get; set; }
    string SortType { get; set; }
    int? AccountId { get; set; }
    List<int> BranchId { get; set; }
}