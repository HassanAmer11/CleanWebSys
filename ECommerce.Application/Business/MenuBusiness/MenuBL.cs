using AutoMapper;
using ECommerce.Application.Dtos.ContentDtos;
using ECommerce.Application.Dtos.MenuDtos;
using ECommerce.Application.IBusiness.IContentBusiness;
using ECommerce.Application.IBusiness.IManageImagesBusiness;
using ECommerce.Application.IBusiness.IMenuBusiness;
using ECommerce.Application.IUOW;
using ECommerce.Core.Common.Response;
using ECommerce.Core.Entities.Model;
using ECommerce.Core.Resources;
using Microsoft.Extensions.Localization;

namespace ECommerce.Application.Business.MenuBusiness
{
    public class MenuBL : ResponseHandler, IMenuBL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        public MenuBL(IMapper mapper, IUnitOfWork unitOfWork, IStringLocalizer<SharedResources> localizer) : base(localizer)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<ResponseApp<IEnumerable<MenuDto>>> GetAll()
        {
            var query = await _unitOfWork.MenuRepo.GetAllAsync();
            var result = _mapper.Map<IEnumerable<MenuDto>>(query);
            return Success(result);
        }

        public async Task<ResponseApp<MenuDto>> GetById(int id)
        {
            var entity = await _unitOfWork.MenuRepo.GetByIdAsync(id);
            if (entity == null) return NotFound<MenuDto>(_localizer[LanguageKey.NotFound]);
            var result = _mapper.Map<MenuDto>(entity);
            return Success(result);
        }

        public async Task<ResponseApp<string>> AddNew(MenuDto dto)
        {
            var menuMapper = _mapper.Map<Menu>(dto);
            var result = await _unitOfWork.MenuRepo.AddAsync(menuMapper);
            return Success<string>(_localizer[LanguageKey.AddSuccessfully]);

        }
        public async Task<ResponseApp<string>> Update(MenuDto menu)
        {
            var entity = await _unitOfWork.MenuRepo.GetByIdAsync(menu.Id);
            if (entity == null) return NotFound<string>(_localizer[LanguageKey.NotFound]);
            var entityMapper = _mapper.Map(menu, entity);
            await _unitOfWork.MenuRepo.UpdateAsync(entityMapper);
            return Success<string>(_localizer[LanguageKey.UpdateProcess]);

        }
        public async Task<ResponseApp<string>> Delete(int id)
        {
            var entity = await _unitOfWork.MenuRepo.GetByIdAsync(id);
            if (entity == null) return NotFound<string>(_localizer[LanguageKey.NotFound]);
            await _unitOfWork.MenuRepo.DeleteAsync(entity);
            return Deleted<string>(_localizer[LanguageKey.DeletedSuccessfully]);
        }
    }
}
