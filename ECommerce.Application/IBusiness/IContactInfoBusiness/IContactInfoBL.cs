using ECommerce.Application.Dtos.ContactInfoDtos;
using ECommerce.Core.Common.Response;

namespace ECommerce.Application.IBusiness.IContactInfoBusiness
{
    public interface IContactInfoBL
    {
        public Task<ResponseApp<IEnumerable<ContactInfoDto>>> GetAll();
        public Task<ResponseApp<ContactInfoDto>> GetById(int id);
        Task<ResponseApp<string>> AddNew(ContactInfoDto contactInfoDto);
        Task<ResponseApp<string>> Update(ContactInfoDto contactInfoDto);
        Task<ResponseApp<string>> Delete(int id);
    }
}
