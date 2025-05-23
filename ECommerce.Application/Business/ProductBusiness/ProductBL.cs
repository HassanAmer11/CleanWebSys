﻿using AutoMapper;
using ECommerce.Application.Behaviors;
using ECommerce.Application.Dtos.ImagesDtos;
using ECommerce.Application.Dtos.ProductDtos;
using ECommerce.Application.IBusiness.IManageImagesBusiness;
using ECommerce.Application.IBusiness.IProductBusiness;
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
    private readonly IStringLocalizer<SharedResources> _localizer;
    #endregion
    #region Constractor
    public ProductBL(IMapper mapper, IUnitOfWork unitOfWork, IManageImagesBL manageImagesBL, IStringLocalizer<SharedResources> localizer) : base(localizer)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _localizer = localizer;
        _manageImagesBL = manageImagesBL;


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
    public async Task<ResponseApp<List<ProductGetDto>>> GetServicesByLocationAsync(int locationId)
    {
        var query = _unitOfWork.ProductRepo.GetAll()
            .Where(s => s.ProductLocations.Any(sl => sl.GovernorateId == locationId))
            .AsNoTracking();

        var services = await _mapper.ProjectTo<ProductGetDto>(query).ToListAsync();

        if (services == null || !services.Any())
            return NotFound<List<ProductGetDto>>(_localizer[LanguageKey.NotFound]);

        return Success(services);
    }
    public async Task<ResponseApp<List<ProductGetDto>>> GetServicesByLocationAndCategoryAsync(int catId, int? locationId)
    {
        var query = _unitOfWork.ProductRepo.GetAll(s => s.CategoryId == catId &&
                    (!locationId.HasValue || s.ProductLocations.Any(sl => sl.GovernorateId == locationId)))
                    .AsNoTracking();

        var services = await _mapper.ProjectTo<ProductGetDto>(query).ToListAsync();

        if (services == null || !services.Any())
            return NotFound<List<ProductGetDto>>(_localizer[LanguageKey.NotFound]);

        return Success(services);
    }
    public async Task<ResponseApp<ProductGetDto>> GetProductById(int id)
    {
        var entity = await _unitOfWork.ProductRepo.GetByIdAsync(p => p.Id == id, i => i.Images, c => c.Category,l => l.ProductLocations);
        if (entity == null) return NotFound<ProductGetDto>(_localizer[LanguageKey.NotFound]);

        var result = _mapper.Map<ProductGetDto>(entity);
        result.LocationIds = entity.ProductLocations.Select(sl => sl.GovernorateId).ToList();
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
            if (category == null) return NotFound<string>(_localizer[LanguageKey.SorryCategoryNotExist]);
            else
            {
                product = _mapper.Map<Product>(dto);
                product.ProductLocations = dto.LocationIds.Select(id => new ProductLocation
                {
                    GovernorateId = id
                }).ToList();
                await _unitOfWork.ProductRepo.AddAsync(product);
                if (dto.Files.Any())
                {
                    await _manageImagesBL.UploadImagesAsync(dto.Files, product.Id, "Products");
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
        var entityQuery = await _unitOfWork.ProductRepo.GetByIdAsync(p => p.Id == productEdit.Id, i => i.Images, l => l.ProductLocations);
        // var images = entityQuery.Images;

        if (entityQuery == null) return NotFound<string>(_localizer[LanguageKey.NotFound]);
        // Update service locations
        entityQuery.ProductLocations.Clear();
        foreach (var locationId in productEdit.LocationIds)
        {
            entityQuery.ProductLocations.Add(new ProductLocation
            {
                ProductId = entityQuery.Id,
                GovernorateId = locationId
            });
        }
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

    public async Task<IEnumerable<GovernorateWithProductsDto>> GetGovernoratesWithProductsByCategoryAsync(int categoryId)
    {
        var data = await _unitOfWork.productLocationRepo.GetAll()
            .Include(pl => pl.Governorate)
            .Include(pl => pl.Product)
            .Where(pl => pl.Product.CategoryId == categoryId)
            .Select(pl => new
            {
                GovernorateId = pl.Governorate.Id,
                GovernorateNameAr = pl.Governorate.NameAr,
                ProductId = pl.Product.Id,
                ProductNameAr = pl.Product.NameAr
            })
            .ToListAsync();

        var grouped = data
            .GroupBy(x => new { x.GovernorateId, x.GovernorateNameAr })
            .Select(g => new GovernorateWithProductsDto
            {
                GovernorateId = g.Key.GovernorateId,
                GovernorateNameAr = g.Key.GovernorateNameAr,
                Services = g.Select(p => new ServiceDto
                {
                    Id = p.ProductId,
                    NameAr = p.ProductNameAr
                }).Distinct().ToList()
            });

        return grouped;
    }

    public async Task<IEnumerable<GovernorateWithProductsDto>> GetGovernoratesWithServicesDetailsByCategory(int categoryId)
    {
        var data = await _unitOfWork.productLocationRepo.GetAll()
            .Include(pl => pl.Governorate)
            .Include(pl => pl.Product)
            .Where(pl => pl.Product.CategoryId == categoryId)
            .Select(pl => new
            {
                GovernorateId = pl.Governorate.Id,
                GovernorateNameAr = pl.Governorate.NameAr,
                ProductId = pl.Product.Id,
                ProductNameAr = pl.Product.NameAr,
                Images = pl.Product.Images.Select(img => new ImagesDto
                {
                    Id = img.Id,
                    ImagePath = img.ImagePath,
                }).ToList()
            })
            .ToListAsync();

        var grouped = data
            .GroupBy(x => new { x.GovernorateId, x.GovernorateNameAr })
            .Select(g => new GovernorateWithProductsDto
            {
                GovernorateId = g.Key.GovernorateId,
                GovernorateNameAr = g.Key.GovernorateNameAr,
                Services = g.Select(p => new ServiceDto
                {
                    Id = p.ProductId,
                    NameAr = p.ProductNameAr,
                    Images = p.Images
                }).Distinct().ToList()
            });

        return grouped;
    }




    #endregion
}
