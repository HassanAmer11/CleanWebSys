using ECommerce.Core.Entities.BaseModel;

namespace ECommerce.Application.Dtos.ProductLocationDtos
{
    public class ProductLocationDto
    {
        public int ProductId { get; set; }
        public int GovernorateId { get; set; }

    }
    public class LocationsIds 
    {
        public int GovernorateId { get; set; }
    }
}
