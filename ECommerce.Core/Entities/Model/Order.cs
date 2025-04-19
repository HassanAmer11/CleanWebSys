using ECommerce.Core.Entities.BaseModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Core.Entities.Model;

public class Order : MainBaseEntity
{
    public string ClientName { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string whatsApp { get; set; }
    public int? OrderStatus { get; set; }

    [ForeignKey("ProductId")]
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int GovernorateId { get; set; }
    public Governorate Governorates { get; set; }
    public string Notes { get; set; }
    public decimal TotalOrderPrice { get; set; }

}
