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
            if (dto.file != null)
            {
                categoryMapper.ImagePath = _manageImagesBL.GetImagePath(dto.file, "Categories");
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
            if (category.file != null)
            {
                entityMapper.ImagePath = _manageImagesBL.GetImagePath(category.file, "Categories");
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
    }
}


