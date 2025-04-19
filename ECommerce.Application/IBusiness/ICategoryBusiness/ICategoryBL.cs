using ECommerce.Application.Dtos.CategoryDtos;
using ECommerce.Core.Common.Response;

namespace ECommerce.Application.IBusiness.ICategoryBusiness
{
    public interface ICategoryBL
    {
        public Task<ResponseApp<IEnumerable<CategoryGetDto>>> GetAll();
        public Task<ResponseApp<CategoryGetDto>> GetById(int id);
        Task<ResponseApp<string>> AddNew(CategoryEditDto category);
        Task<ResponseApp<string>> UpdateOneRow(CategoryEditDto category);
        Task<ResponseApp<string>> DeleteOneRow(int id);
    }
}
