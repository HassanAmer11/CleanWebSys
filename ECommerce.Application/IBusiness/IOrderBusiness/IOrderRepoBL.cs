using ECommerce.Application.Dtos.OrdersDtos;
using ECommerce.Application.Helpers;
using ECommerce.Application.Wrappers;
using ECommerce.Core.Common;
using ECommerce.Core.Common.Response;

namespace ECommerce.Application.IBusiness.IOrderBusiness;

public interface IOrderRepoBL
{
    Task<ResponseApp<string>> CreateOrder(OrdersEditDto order);
    Task<PaginatedResult<OrdersGetDto>> GetAllOrders(Paginat Criteria);
    Task<PaginatedResult<OrdersGetDto>> GetOrdersAccordingSearchCriteria(Criteria criteria);
    Task<ResponseApp<OrdersGetDto>> GetOrderById(int id);
    Task<ResponseApp<string>> UpdateOrder(OrdersEditDto orderEdit);
    Task<ResponseApp<string>> UpdateOrderStatus(int oderId, int status);
    Task<ResponseApp<string>> DeleteOrder(int entityId);
}
