using ECommerce.Core.Entities.BaseModel;

namespace ECommerce.Application.Dtos.GovernoratesDtos;

public class GovernoratesDto : BaseId
{
    public string NameAr { get; set; }
    public string NameEn { get; set; }
    public bool IsFreeDelivery { get; set; }
    public decimal DeliverdFees { get; set; }
}
