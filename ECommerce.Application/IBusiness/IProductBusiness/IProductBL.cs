using ECommerce.Application.Dtos.ImagesDtos;
using ECommerce.Application.Dtos.ProductDtos;
using ECommerce.Application.Wrappers;
using ECommerce.Core.Common;
using ECommerce.Core.Common.Response;

namespace ECommerce.Application.IBusiness.IProductBusiness;

public interface IProductBL
{
    public Task<PaginatedResult<ProductGetDto>> GetHomeScreenProducts(Paginat Criteria);
    public Task<PaginatedResult<ProductGetDto>> GetAllProducts(Paginat Criteria);
    public Task<PaginatedResult<ProductGetDto>> GetProductsByCategoryId(Paginat Criteria, int catId);
    public Task<ResponseApp<ProductGetDto>> GetProductById(int id);
    Task<ResponseApp<string>> AddProduct(ProductEditDto product);
    Task<ResponseApp<string>> UpdateProduct(ProductEditDto product);
    Task<ResponseApp<string>> DeleteProduct(int id);
    Task<ResponseApp<string>> DeleteImageProduct(DeleteImagesDto dto);
}
