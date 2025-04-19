using ECommerce.Application.Dtos.GovernoratesDtos;
using ECommerce.Core.Common.Response;

namespace ECommerce.Application.IBusiness.IGovernorateBusiness;

public interface IGovernoratesBL
{
    public Task<ResponseApp<IEnumerable<GovernoratesDto>>> GetAllGovernorates();
    public Task<ResponseApp<GovernoratesDto>> GetGovernorateById(int id);
    Task<ResponseApp<string>> AddGovernorate(GovernoratesDto governorate);
    Task<ResponseApp<string>> UpdateGovernorate(GovernoratesDto governorate);
    Task<ResponseApp<string>> DeleteGovernorate(int id);
}
