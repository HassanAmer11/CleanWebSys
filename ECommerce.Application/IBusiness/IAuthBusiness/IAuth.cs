using ECommerce.Application.Dtos.AuthDtos;
using ECommerce.Core.Common.Response;

namespace ECommerce.Application.IBusiness.IAuthBusiness;

public interface IAuth
{
    Task<ResponseApp<string>> Login(LoginDto dto);
}
