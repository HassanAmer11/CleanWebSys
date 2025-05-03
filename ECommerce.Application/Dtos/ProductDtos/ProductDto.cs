using ECommerce.Application.Dtos.CategoryDtos;
using ECommerce.Application.Dtos.ImagesDtos;
using ECommerce.Application.Dtos.ProductLocationDtos;
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
