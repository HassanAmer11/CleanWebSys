using AutoMapper;
using ECommerce.Application.Dtos.ProductLocationDtos;
using ECommerce.Application.IBusiness.IProductLocationBusiness;
using ECommerce.Application.IUOW;
using ECommerce.Core.Common.Response;
using ECommerce.Core.Entities.Model;
using ECommerce.Core.Resources;
using Microsoft.Extensions.Localization;

namespace ECommerce.Application.Business.ProductLocationBusiness
{

    public class ProductLocationBL : ResponseHandler, IProductLocationBL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        public ProductLocationBL(IMapper mapper, IUnitOfWork unitOfWork, IStringLocalizer<SharedResources> localizer) : base(localizer)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<ResponseApp<IEnumerable<ProductLocationDto>>> GetAll()
        {
            var query = await _unitOfWork.productLocationRepo.GetAllAsync();
            var result = _mapper.Map<IEnumerable<ProductLocationDto>>(query);
            return Success(result);
        }

        public async Task<ResponseApp<ProductLocationDto>> GetById(int id)
        {
            var entity = await _unitOfWork.productLocationRepo.GetByIdAsync(id);
            if (entity == null) return NotFound<ProductLocationDto>(_localizer[LanguageKey.NotFound]);
            var result = _mapper.Map<ProductLocationDto>(entity);
            return Success(result);
        }

        public async Task<ResponseApp<string>> AddNew(ProductLocationDto dto)
        {
            var productLocationMapper = _mapper.Map<ProductLocation>(dto);
            var result = await _unitOfWork.productLocationRepo.AddAsync(productLocationMapper);
            return Success<string>(_localizer[LanguageKey.AddSuccessfully]);

        }
        public async Task<ResponseApp<string>> Update(ProductLocationDto dto)
        {
            var entity = await _unitOfWork.productLocationRepo.GetByIdAsync(dto.Id);
            if (entity == null) return NotFound<string>(_localizer[LanguageKey.NotFound]);
            var entityMapper = _mapper.Map(dto, entity);
            await _unitOfWork.productLocationRepo.UpdateAsync(entityMapper);
            return Success<string>(_localizer[LanguageKey.UpdateProcess]);

        }
        public async Task<ResponseApp<string>> Delete(int id)
        {
            var entity = await _unitOfWork.productLocationRepo.GetByIdAsync(id);
            if (entity == null) return NotFound<string>(_localizer[LanguageKey.NotFound]);
            await _unitOfWork.productLocationRepo.DeleteAsync(entity);
            return Deleted<string>(_localizer[LanguageKey.DeletedSuccessfully]);
        }


        public async Task<ResponseApp<string>> AddNewServicLocationAsync(List<int> Locations, int productId)
        {
            List<int> Ids = new List<int>();
            foreach (var LocationId in Locations)
            {
                ProductLocationDto dto = new ProductLocationDto()
                {
                    GovernorateId = LocationId,
                    ProductId = productId,
                };
                var response = await AddNew(dto);
                if (!response.Succeeded)
                {
                    return response;
                }
            }
            return Success<string>(_localizer[LanguageKey.AddSuccessfully]);
        }
    }
}
