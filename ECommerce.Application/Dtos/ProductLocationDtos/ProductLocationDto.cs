using ECommerce.Application.Dtos.CategoryDtos;
using ECommerce.Application.Dtos.GovernoratesDtos;
using ECommerce.Application.Dtos.ProductDtos;
using ECommerce.Core.Entities.BaseModel;
using ECommerce.Core.Entities.Model;

namespace ECommerce.Application.Dtos.ProductLocationDtos
{
    public class ProductLocationDto
    {
        public int ProductId { get; set; }
        public int GovernorateId { get; set; }
        //public ProductBase Product { get; set; }
        //public GovernoratesDto Governorate { get; set; }


    }
    public class LocationsIds 
    {
        public int GovernorateId { get; set; }
    }

    public class LocationBriefDTO
    {
        public int GovernorateId { get; set; }
        public string GovernorateName { get; set; } // e.g., "City - Area"
    }
}
