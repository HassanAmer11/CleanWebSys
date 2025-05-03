using AutoMapper;
using ECommerce.Application.Behaviors;
using ECommerce.Application.Dtos.ImagesDtos;
using ECommerce.Application.Dtos.ProductDtos;
using ECommerce.Application.IBusiness.IManageImagesBusiness;
using ECommerce.Application.IBusiness.IProductBusiness;
using ECommerce.Application.IBusiness.IProductLocationBusiness;
using ECommerce.Application.IUOW;
using ECommerce.Application.Wrappers;
using ECommerce.Core.Common;
using ECommerce.Core.Common.Response;
using ECommerce.Core.Entities.Model;
using ECommerce.Core.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;



namespace ECommerce.Application.Business.ProductBusiness;

public class ProductBL : ResponseHandler, IProductBL
{
    #region Fields
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IManageImagesBL _manageImagesBL;
    private readonly IProductLocationBL _productLocationBL;
    private readonly IStringLocalizer<SharedResources> _localizer;
    #endregion
    #region Constractor
    public ProductBL(IMapper mapper, IUnitOfWork unitOfWork, IManageImagesBL manageImagesBL, IStringLocalizer<SharedResources> localizer, IProductLocationBL productLocationBL) : base(localizer)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _localizer = localizer;
        _manageImagesBL = manageImagesBL;
        _productLocationBL = productLocationBL;


    }
    #endregion
    #region Handle Functions 

    public async Task<PaginatedResult<ProductGetDto>> GetAllProducts(Paginat Criteria)
    {

        var query = _unitOfWork.ProductRepo.GetAll().AsNoTracking();
        var ProductList = await _mapper.ProjectTo<ProductGetDto>(query).ToPaginatedListAsync(Criteria.PageNumber, Criteria.PageSize);
        return ProductList;
    }
    public async Task<PaginatedResult<ProductGetDto>> GetHomeScreenProducts(Paginat Criteria)
    {
        var query = _unitOfWork.ProductRepo.GetAll(s => s.ShowHome).AsNoTracking();
        var ProductList = await _mapper.ProjectTo<ProductGetDto>(query).ToPaginatedListAsync(Criteria.PageNumber, Criteria.PageSize);
        return ProductList;
    }
    public async Task<PaginatedResult<ProductGetDto>> GetProductsByCategoryId(Paginat Criteria, int catId)
    {
        var query = _unitOfWork.ProductRepo.GetAll(s => s.CategoryId == catId).AsNoTracking();
        var ProductList = await _mapper.ProjectTo<ProductGetDto>(query).ToPaginatedListAsync(Criteria.PageNumber, Criteria.PageSize);
        return ProductList;
    }

    public async Task<ResponseApp<ProductGetDto>> GetProductById(int id)
    {
        var entity = await _unitOfWork.ProductRepo.GetByIdAsync(p => p.Id == id, i => i.Images, c => c.Category);
        if (entity == null) return NotFound<ProductGetDto>(_localizer[LanguageKey.NotFound]);
        var result = _mapper.Map<ProductGetDto>(entity);
        return Success(result);
    }


    public async Task<ResponseApp<string>> AddProduct(ProductEditDto dto)
    {
        Product product = null;
        try
        {
            var category = await _unitOfWork.CategoryRepo.GetByIdAsync(dto.CategoryId);

            var ValidatErrors = ValidationUtility.ValidateProduct(dto);
            if (ValidatErrors != "") return UnprocessableEntity<string>(ValidatErrors);
            var Governorates = await _unitOfWork.GovernorateRepo.FindAsync(c => dto.LocationIds.Contains(c.Id));

            if (Governorates.Count() != dto.LocationIds.Count)
                return NotFound<string>(_localizer[LanguageKey.StateNotFound]);
            
            if (category == null) return NotFound<string>(_localizer[LanguageKey.SorryCategoryNotExist]);
            else
            {
                product = _mapper.Map<Product>(dto);
                await _unitOfWork.ProductRepo.AddAsync(product);
                if (dto.Files.Any())
                {
                    await _manageImagesBL.UploadImagesAsync(dto.Files, product.Id, "Products");
                }
                if (dto.LocationIds.Any())
                {
                    await _productLocationBL.AddNewServicLocationAsync(dto.LocationIds, product.Id);
                }
                return Success<string>(_localizer[LanguageKey.AddSuccessfully]);
            }
        }
        catch (Exception e)
        {
            if (dto.Files != null)
                foreach (var image in product.Images)
                {
                    await _manageImagesBL.DeleteImage(image.Id);
                }
            return Success(e.Message);
        }
    }


    public async Task<ResponseApp<string>> UpdateProduct(ProductEditDto productEdit)
    {
        var entityQuery = await _unitOfWork.ProductRepo.GetByIdAsync(p => p.Id == productEdit.Id, i => i.Images);
        // var images = entityQuery.Images;

        if (entityQuery == null) return NotFound<string>(_localizer[LanguageKey.NotFound]);

        var ValidatErrors = ValidationUtility.ValidateProduct(productEdit);
        if (ValidatErrors != "") return UnprocessableEntity<string>(ValidatErrors);

        var entityMapper = _mapper.Map(productEdit, entityQuery);
        await _unitOfWork.ProductRepo.UpdateAsync(entityMapper);
        if (productEdit.Files.Any()) await _manageImagesBL.UploadImagesAsync(productEdit.Files, entityQuery.Id, "Products");
        return Success<string>(_localizer[LanguageKey.UpdateProcess]);
    }

    public async Task<ResponseApp<string>> DeleteProduct(int entityId)
    {
        var entity = await _unitOfWork.ProductRepo.GetByIdAsync(p => p.Id == entityId, i => i.Images);
        if (entity == null) return NotFound<string>(_localizer[LanguageKey.NotFound]);
        bool IsImages = entity.Images.Any() ? true : false;
        await _unitOfWork.ProductRepo.DeleteAsync(entity);
        if (IsImages)
        {
            foreach (var image in entity.Images)
                await _manageImagesBL.DeleteImage(image.Id);
        }

        return Deleted<string>(_localizer[LanguageKey.DeletedSuccessfully]);
    }

    public async Task<ResponseApp<string>> DeleteImageProduct(DeleteImagesDto dto)
    {
        var entity = await _unitOfWork.ProductRepo.GetByIdAsync(p => p.Id == dto.ProductId, i => i.Images);
        if (entity == null) return NotFound<string>(_localizer[LanguageKey.NotFound]);
        if (entity.Images.Any())
        {
            await _manageImagesBL.DeleteImage(dto.ImageId);
            return Deleted<string>(_localizer[LanguageKey.DeletedSuccessfully]);
        }
        else return BadRequest<string>(_localizer[LanguageKey.BadRequest]);
    }

    #endregion
}
