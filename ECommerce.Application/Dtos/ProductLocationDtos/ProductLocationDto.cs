using ECommerce.Core.Entities.BaseModel;

namespace ECommerce.Application.Dtos.ProductLocationDtos
{
    public class ProductLocationDto: BaseId
    {
        public int ProductId { get; set; }
        public int GovernorateId { get; set; }

    }
}
