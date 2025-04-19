using ECommerce.Application.Dtos.GovernoratesDtos;
using ECommerce.Application.Dtos.MenuDtos;
using ECommerce.Core.Common.Response;

namespace ECommerce.Application.IBusiness.IMenuBusiness;
public interface IMenuBL
{
    public Task<ResponseApp<IEnumerable<MenuDto>>> GetAll();
    public Task<ResponseApp<MenuDto>> GetById(int id);
    Task<ResponseApp<string>> AddNew(MenuDto menuDto);
    Task<ResponseApp<string>> Update(MenuDto menuDto);
    Task<ResponseApp<string>> Delete(int id);
}
