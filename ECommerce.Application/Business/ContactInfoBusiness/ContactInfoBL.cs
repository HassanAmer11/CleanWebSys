using AutoMapper;
using ECommerce.Application.Dtos.ContactInfoDtos;
using ECommerce.Application.IBusiness.IContactInfoBusiness;
using ECommerce.Application.IUOW;
using ECommerce.Core.Common.Response;
using ECommerce.Core.Entities.Model;
using ECommerce.Core.Resources;
using Microsoft.Extensions.Localization;


namespace ECommerce.Application.Business.ContactInfoBusiness
{
    public class ContactInfoBL : ResponseHandler, IContactInfoBL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        public ContactInfoBL(IMapper mapper, IUnitOfWork unitOfWork, IStringLocalizer<SharedResources> localizer) : base(localizer)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<ResponseApp<IEnumerable<ContactInfoDto>>> GetAll()
        {
            var query = await _unitOfWork.contactInfoRepo.GetAllAsync();
            var result = _mapper.Map<IEnumerable<ContactInfoDto>>(query);
            return Success(result);
        }
        public async Task<ResponseApp<ContactInfoDto>> GetFirstContactInfo()
        {
            var entity = await _unitOfWork.contactInfoRepo.SingleOrDefaultAsync(x => true);
            if (entity == null) return NotFound<ContactInfoDto>(_localizer[LanguageKey.NotFound]);
            var result = _mapper.Map<ContactInfoDto>(entity);
            return Success(result);
        }
        public async Task<ResponseApp<ContactInfoDto>> GetById(int id)
        {
            var entity = await _unitOfWork.contactInfoRepo.GetByIdAsync(id);
            if (entity == null) return NotFound<ContactInfoDto>(_localizer[LanguageKey.NotFound]);
            var result = _mapper.Map<ContactInfoDto>(entity);
            return Success(result);
        }

        public async Task<ResponseApp<string>> AddNew(ContactInfoDto dto)
        {
            var count = await _unitOfWork.contactInfoRepo.Count();
            if (count > 0) return BadRequest<string>(_localizer[LanguageKey.OperationFailed]);
            else
            {
                var ContactInfoMapper = _mapper.Map<ContactInfo>(dto);
                var result = await _unitOfWork.contactInfoRepo.AddAsync(ContactInfoMapper);
                return Success<string>(_localizer[LanguageKey.AddSuccessfully]);
            }

        }
        public async Task<ResponseApp<string>> Update(ContactInfoDto ContactInfo)
        {
            var entity = await _unitOfWork.contactInfoRepo.GetByIdAsync(ContactInfo.Id);
            if (entity == null) return NotFound<string>(_localizer[LanguageKey.NotFound]);
            var entityMapper = _mapper.Map(ContactInfo, entity);
            await _unitOfWork.contactInfoRepo.UpdateAsync(entityMapper);
            return Success<string>(_localizer[LanguageKey.UpdateProcess]);

        }
        public async Task<ResponseApp<string>> Delete(int id)
        {
            var entity = await _unitOfWork.contactInfoRepo.GetByIdAsync(id);
            if (entity == null) return NotFound<string>(_localizer[LanguageKey.NotFound]);
            await _unitOfWork.contactInfoRepo.DeleteAsync(entity);
            return Deleted<string>(_localizer[LanguageKey.DeletedSuccessfully]);
        }
    }
}