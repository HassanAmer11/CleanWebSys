

using AutoMapper;
using ECommerce.Application.Dtos.ContentDtos;
using ECommerce.Application.IBusiness.IContentBusiness;
using ECommerce.Application.IBusiness.IManageImagesBusiness;
using ECommerce.Application.IUOW;
using ECommerce.Core.Common.Response;
using ECommerce.Core.Entities.Model;
using ECommerce.Core.Resources;
using Microsoft.Extensions.Localization;

namespace ECommerce.Application.Business.ContentBusiness
{
    public class ContentBL : ResponseHandler, IContentBL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IManageImagesBL _manageImagesBL;
        public ContentBL(IMapper mapper, IUnitOfWork unitOfWork, IStringLocalizer<SharedResources> localizer, IManageImagesBL manageImagesBL) : base(localizer)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
            _manageImagesBL = manageImagesBL;
        }

        public async Task<ResponseApp<IEnumerable<ContentDto>>> GetAll()
        {
            var query = await _unitOfWork.ContentRepo.GetAllAsync();
            var result = _mapper.Map<IEnumerable<ContentDto>>(query);
            return Success(result);
        }

        public async Task<ResponseApp<ContentDto>> GetById(int id)
        {
            var entity = await _unitOfWork.ContentRepo.GetByIdAsync(id);
            if (entity == null) return NotFound<ContentDto>(_localizer[LanguageKey.NotFound]);
            var result = _mapper.Map<ContentDto>(entity);
            return Success(result);
        }

        public async Task<ResponseApp<string>> AddNew(ContentDto dto)
        {
            var contentMapper = _mapper.Map<Content>(dto);
            var result = await _unitOfWork.ContentRepo.AddAsync(contentMapper);
            return Success<string>(_localizer[LanguageKey.AddSuccessfully]);

        }
        public async Task<ResponseApp<string>> Update(ContentDto content)
        {
            var entity = await _unitOfWork.ContentRepo.GetByIdAsync(content.Id);
            if (entity == null) return NotFound<string>(_localizer[LanguageKey.NotFound]);
            var entityMapper = _mapper.Map(content, entity);
            await _unitOfWork.ContentRepo.UpdateAsync(entityMapper);
            return Success<string>(_localizer[LanguageKey.UpdateProcess]);

        }
        public async Task<ResponseApp<string>> Delete(int id)
        {
            var entity = await _unitOfWork.ContentRepo.GetByIdAsync(id);
            if (entity == null) return NotFound<string>(_localizer[LanguageKey.NotFound]);
            await _unitOfWork.ContentRepo.DeleteAsync(entity);
            return Deleted<string>(_localizer[LanguageKey.DeletedSuccessfully]);
        }
    }
}
