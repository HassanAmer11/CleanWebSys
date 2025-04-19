using ECommerce.Application.Dtos.ContentDtos;
using ECommerce.Application.Dtos.MenuDtos;
using ECommerce.Core.Common.Response;

namespace ECommerce.Application.IBusiness.IContentBusiness
{
    public interface IContentBL
    {
        public Task<ResponseApp<IEnumerable<ContentDto>>> GetAll();
        public Task<ResponseApp<ContentDto>> GetById(int id);
        Task<ResponseApp<string>> AddNew(ContentDto contentDto);
        Task<ResponseApp<string>> Update(ContentDto contentDto);
        Task<ResponseApp<string>> Delete(int id);
    }
}
