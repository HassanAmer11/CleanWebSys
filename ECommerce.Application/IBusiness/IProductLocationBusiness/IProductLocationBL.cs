using ECommerce.Application.Dtos.ProductLocationDtos;
using ECommerce.Core.Common.Response;

namespace ECommerce.Application.IBusiness.IProductLocationBusiness
{
    public interface IProductLocationBL
    {
        public Task<ResponseApp<IEnumerable<ProductLocationDto>>> GetAll();
        public Task<ResponseApp<ProductLocationDto>> GetById(int id);
        Task<ResponseApp<string>> AddNew(ProductLocationDto productLocation);
        Task<ResponseApp<string>> Update(ProductLocationDto productLocation);
        Task<ResponseApp<string>> Delete(int id);
        Task<ResponseApp<string>> AddNewServicLocationAsync(List<int> Locations, int productId);
    }
}
