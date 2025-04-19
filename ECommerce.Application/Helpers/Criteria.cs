using ECommerce.Core.Common;

namespace ECommerce.Application.Helpers;

public class Criteria : Paginat
{
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public int? OrderStatus { get; set; }
}
