using AutoMapper;
using ECommerce.Application.Behaviors;
using ECommerce.Application.Dtos.GovernoratesDtos;
using ECommerce.Application.IBusiness.IGovernorateBusiness;
using ECommerce.Application.IUOW;
using ECommerce.Core.Common.Response;
using ECommerce.Core.Entities.Model;
using ECommerce.Core.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;


namespace ECommerce.Application.Business.GovernoratesBL;

public class GovernoratesBL : ResponseHandler, IGovernoratesBL
{
    #region Fields
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<SharedResources> _localizer;
    #endregion
    #region Constractor
    public GovernoratesBL(IMapper mapper, IUnitOfWork unitOfWork, IStringLocalizer<SharedResources> localizer) : base(localizer)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }
    #endregion
    #region Handle Functions
    public async Task<ResponseApp<GovernoratesDto>> GetGovernorateById(int id)
    {
        var entity = await _unitOfWork.GovernorateRepo.GetByIdAsync(id);
        if (entity == null) return NotFound<GovernoratesDto>(_localizer[LanguageKey.NotFound]);
        var result = _mapper.Map<GovernoratesDto>(entity);
        return Success(result);
    }
    public async Task<ResponseApp<IEnumerable<GovernoratesDto>>> GetAllGovernorates()
    {
        var query = await _unitOfWork.GovernorateRepo.GetAllAsync();
        var result = _mapper.Map<IEnumerable<GovernoratesDto>>(query);
        return Success(result);
    }
    public async Task<ResponseApp<string>> AddGovernorate(GovernoratesDto dto)
    {

        var ValidatErrors = ValidationUtility.ValidateGovernorate(dto);
        if (ValidatErrors != "") return UnprocessableEntity<string>(ValidatErrors);

        var governorateMapper = _mapper.Map<Governorate>(dto);
        await _unitOfWork.GovernorateRepo.AddAsync(governorateMapper);

        return Success<string>(_localizer[LanguageKey.AddSuccessfully]);

    }

    public async Task<ResponseApp<string>> UpdateGovernorate(GovernoratesDto governorate)
    {
        var entity = await _unitOfWork.GovernorateRepo.GetByIdAsync(governorate.Id);
        if (entity == null) return NotFound<string>(_localizer[LanguageKey.NotFound]);

        var ValidatErrors = ValidationUtility.ValidateGovernorate(governorate);
        if (ValidatErrors != "") return UnprocessableEntity<string>(ValidatErrors);

        var entityMapper = _mapper.Map(governorate, entity);
        await _unitOfWork.GovernorateRepo.UpdateAsync(entityMapper);
        return Success<string>(_localizer[LanguageKey.UpdateProcess]);
    }

    public async Task<ResponseApp<string>> DeleteGovernorate(int id)
    {
        var entity = await _unitOfWork.GovernorateRepo.GetByIdAsync(id);
        if (entity == null) return NotFound<string>(_localizer[LanguageKey.NotFound]);
        await _unitOfWork.GovernorateRepo.DeleteAsync(entity);
        return Deleted<string>(_localizer[LanguageKey.DeletedSuccessfully]);
    }

    public async Task<IEnumerable<GovernoratesDto>> GetGovernoratesWithProductsAsync()
    {
        return await _unitOfWork.productLocationRepo.GetAll()
            .Include(pl => pl.Governorate)
            .Select(pl => new GovernoratesDto
            {
                Id = pl.Governorate.Id,
                NameAr = pl.Governorate.NameAr
            })
            .Distinct()
            .ToListAsync();
    }

    public async Task<IEnumerable<GovernoratesDto>> GetGovernoratesWithProductsByCategoryAsync(int categoryId)
    {
        return await _unitOfWork.productLocationRepo.GetAll()
            .Include(pl => pl.Governorate)
            .Include(pl => pl.Product)
            .Where(pl => pl.Product.CategoryId == categoryId)
            .Select(pl => new GovernoratesDto
            {
                Id = pl.Governorate.Id,
                NameAr = pl.Governorate.NameAr
            })
            .Distinct()
            .ToListAsync();
    }
    #endregion
}
