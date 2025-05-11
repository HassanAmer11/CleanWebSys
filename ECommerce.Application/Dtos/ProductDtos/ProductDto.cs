using ECommerce.Application.Dtos.CategoryDtos;
using ECommerce.Application.Dtos.ImagesDtos;
using ECommerce.Application.Dtos.ProductLocationDtos;
using ECommerce.Core.Entities.BaseModel;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Application.Dtos.ProductDtos;

public class ProductGetDto : ProductBase
{
    public List<ImagesDto> Images { get; set; }
    public CategoryBaseDto Category { get; set; }
    public List<ProductLocationDto> ProductLocations { get; set; }
    public List<int> LocationIds { get; set; } = new();
}
public class ProductEditDto : ProductBase
{
    public List<IFormFile> Files { get; set; } = new();
    public List<int> LocationIds { get; set; } = new();
}
public class ServiceDto
{
    public int Id { get; set; }
    public string NameAr { get; set; }
}

public class GovernorateWithProductsDto
{
    public int GovernorateId { get; set; }
    public string GovernorateNameAr { get; set; }
    public List<ServiceDto> Services { get; set; }
}


