using AutoMapper;
using ECommerce.Application.Behaviors;
using ECommerce.Application.Dtos.CategoryDtos;
using ECommerce.Application.IBusiness.ICategoryBusiness;
using ECommerce.Application.IBusiness.IManageImagesBusiness;
using ECommerce.Application.IUOW;
using ECommerce.Core.Common.Response;
using ECommerce.Core.Entities.Model;
using ECommerce.Core.Resources;
using Microsoft.Extensions.Localization;

namespace ECommerce.Application.Business.CategoryBusiness
{
    public class CategoryBL : ResponseHandler, ICategoryBL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IManageImagesBL _manageImagesBL;
        public CategoryBL(IMapper mapper, IUnitOfWork unitOfWork, IStringLocalizer<SharedResources> localizer, IManageImagesBL manageImagesBL) : base(localizer)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
            _manageImagesBL = manageImagesBL;
        }
        public async Task<ResponseApp<IEnumerable<CategoryGetDto>>> GetAll()
        {
            var query = await _unitOfWork.CategoryRepo.GetAllAsync();
            var result = _mapper.Map<IEnumerable<CategoryGetDto>>(query);
            return Success(result);
        }

        public async Task<ResponseApp<CategoryGetDto>> GetById(int id)
        {
            var entity = await _unitOfWork.CategoryRepo.GetByIdAsync(id);
            if (entity == null) return NotFound<CategoryGetDto>(_localizer[LanguageKey.NotFound]);
            var result = _mapper.Map<CategoryGetDto>(entity);
            return Success(result);
        }

        public async Task<ResponseApp<string>> AddNew(CategoryEditDto dto)
        {

            var ValidatErrors = ValidationUtility.ValidateCategory(dto);
            if (ValidatErrors != "") return UnprocessableEntity<string>(ValidatErrors);

            var categoryMapper = _mapper.Map<Category>(dto);
            if (dto.Imagefile != null)
            {
                categoryMapper.ImagePath = _manageImagesBL.GetImagePath(dto.Imagefile, "Categories");
            }
            if (dto.Iconfile != null)
            {
                categoryMapper.IconPath = _manageImagesBL.GetImagePath(dto.Iconfile, "Categories");
            }
            var result = await _unitOfWork.CategoryRepo.AddAsync(categoryMapper);
            return Success<string>(_localizer[LanguageKey.AddSuccessfully]);

        }
        public async Task<ResponseApp<string>> UpdateOneRow(CategoryEditDto category)
        {
            var entity = await _unitOfWork.CategoryRepo.GetByIdAsync(category.Id);
            if (entity == null) return NotFound<string>(_localizer[LanguageKey.NotFound]);

            var ValidatErrors = ValidationUtility.ValidateCategory(category);
            if (ValidatErrors != "") return UnprocessableEntity<string>(ValidatErrors);

            var entityMapper = _mapper.Map(category, entity);
            if (category.Imagefile != null)
            {
                entityMapper.ImagePath = _manageImagesBL.GetImagePath(category.Imagefile, "Categories");
            }
            if (category.Iconfile != null)
            {
                entityMapper.IconPath = _manageImagesBL.GetImagePath(category.Iconfile, "Categories");
            }
            await _unitOfWork.CategoryRepo.UpdateAsync(entityMapper);
            return Success<string>(_localizer[LanguageKey.UpdateProcess]);

        }
        public async Task<ResponseApp<string>> DeleteOneRow(int id)
        {
            var entity = await _unitOfWork.CategoryRepo.GetByIdAsync(id);
            if (entity == null) return NotFound<string>(_localizer[LanguageKey.NotFound]);
            await _unitOfWork.CategoryRepo.DeleteAsync(entity);
            return Deleted<string>(_localizer[LanguageKey.DeletedSuccessfully]);
        }

        public async Task<ResponseApp<string>> AddCategoryToNavBar(NavCategoryDto dto)
        {
            if (dto == null)
                return BadRequest<string>(_localizer[LanguageKey.BadRequest]);

            // إذا كان المستخدم يحاول التفعيل
            if (dto.ShowNavBar == true)
            {
                var count = await _unitOfWork.CategoryRepo.Count(c => c.ShowNavBar == true);
                if (count > 3)
                    return BadRequest<string>(_localizer[LanguageKey.OperationFailed]);
            }

            var entity = await _unitOfWork.CategoryRepo.GetByIdAsync(dto.CategoryId);
            if (entity == null)
                return NotFound<string>(_localizer[LanguageKey.NotFound]);

            entity.ShowNavBar = dto.ShowNavBar;
            await _unitOfWork.CategoryRepo.UpdateAsync(entity);
            return Success<string>(_localizer[LanguageKey.UpdateProcess]);
        }

    }
}


