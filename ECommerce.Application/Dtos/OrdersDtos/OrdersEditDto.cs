using ECommerce.Core.Entities.BaseModel;

namespace ECommerce.Application.Dtos.OrdersDtos;

public class OrdersEditDto : BaseId
{
    public string ClientName { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string whatsApp { get; set; }
    public int ProductId { get; set; }
    public int GovernorateId { get; set; }
    public int? OrderStatus { get; set; }
    public string Notes { get; set; }
    public decimal TotalOrderPrice { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
}
