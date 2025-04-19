using AutoMapper;
using ECommerce.Application.Behaviors;
using ECommerce.Application.Dtos.OrdersDtos;
using ECommerce.Application.Helpers;
using ECommerce.Application.IBusiness.IOrderBusiness;
using ECommerce.Application.IUOW;
using ECommerce.Application.Wrappers;
using ECommerce.Core.Common;
using ECommerce.Core.Common.Response;
using ECommerce.Core.Entities.Model;
using ECommerce.Core.Enums;
using ECommerce.Core.Resources;
using ECommerce.Infrastructure.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;
using System.Data.Entity;

namespace ECommerce.Application.Business.OrderBusiness;
[Authorize]
public class OrderRepoBL : ResponseHandler, IOrderRepoBL
{
    #region Fields
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<SharedResources> _localizer;
    #endregion
    #region Constractor
    public OrderRepoBL(IMapper mapper, IUnitOfWork unitOfWork, IStringLocalizer<SharedResources> localizer) : base(localizer)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }
    #endregion
    #region Handle Functions 
    [AllowAnonymous]
    public async Task<ResponseApp<string>> CreateOrder(OrdersEditDto dto)
    {
        try
        {
            var product = await _unitOfWork.ProductRepo.GetByIdAsync(dto.ProductId);
            var ValidatErrors = ValidationUtility.ValidateOrder(dto);
            if (ValidatErrors != "") return UnprocessableEntity<string>(ValidatErrors);

            if (product == null) return NotFound<string>(_localizer[LanguageKey.NotFound]);
            else
            {
                var orderMapper = _mapper.Map<Order>(dto);
                orderMapper.Created = DateTime.Now;
                await _unitOfWork.OrderRepo.AddAsync(orderMapper);
                return Success<string>(_localizer[LanguageKey.AddSuccessfully]);
            }
        }
        catch (Exception e)
        {
            return BadRequest<string>(e.Message);
        }
    }
    public async Task<PaginatedResult<OrdersGetDto>> GetAllOrders(Paginat Criteria)
    {

        var query = _unitOfWork.OrderRepo.GetAll().AsNoTracking();
        var OrderList = await _mapper.ProjectTo<OrdersGetDto>(query).ToPaginatedListAsync(Criteria.PageNumber, Criteria.PageSize);
        var orderStatus = CommonExtenion.GetEnumList<OrderEnum>();
        if (orderStatus.Any()) OrderList.Meta = new { OrderStatus = orderStatus };
        return OrderList;
    }
    public async Task<PaginatedResult<OrdersGetDto>> GetOrdersAccordingSearchCriteria(Criteria criteria)
    {
        DateTime fromDate = criteria.FromDate ?? DateTime.Now;
        DateTime toDate = criteria.ToDate ?? DateTime.Now;
        //toDate = toDate.AddDays(1);
        int status = criteria.OrderStatus ?? 2;

        var query = _unitOfWork.OrderRepo.GetAll(o => o.Created >= fromDate && o.Created <= toDate && o.OrderStatus == status).AsNoTracking();
        var OrderList = await _mapper.ProjectTo<OrdersGetDto>(query).ToPaginatedListAsync(criteria.PageNumber, criteria.PageSize);
        var orderStatus = CommonExtenion.GetEnumList<OrderEnum>();
        if (orderStatus.Any()) OrderList.Meta = new { OrderStatus = orderStatus };
        return OrderList;
    }
    public async Task<ResponseApp<OrdersGetDto>> GetOrderById(int id)
    {
        var entity = await _unitOfWork.OrderRepo.GetByIdAsync(o => o.Id == id, p => p.Product, g => g.Governorates);
        if (entity == null) return NotFound<OrdersGetDto>(_localizer[LanguageKey.NotFound]);
        var entityMapper = _mapper.Map<OrdersGetDto>(entity);
        var result = Success(entityMapper);
        var orderStatus = CommonExtenion.GetEnumList<OrderEnum>();
        if (orderStatus.Any()) result.Meta = new { OrderStatus = orderStatus };
        return result;
    }

    public async Task<ResponseApp<string>> UpdateOrder(OrdersEditDto orderEdit)
    {
        var entityQuery = await _unitOfWork.OrderRepo.GetByIdAsync(p => p.Id == orderEdit.Id);
        if (entityQuery == null) return NotFound<string>(_localizer[LanguageKey.NotFound]);

        var ValidatErrors = ValidationUtility.ValidateOrder(orderEdit);
        if (ValidatErrors != "") return UnprocessableEntity<string>(ValidatErrors);

        var entityMapper = _mapper.Map(orderEdit, entityQuery);
        entityMapper.Created = entityQuery.Created;
        entityMapper.ModifiedDate = DateTime.Now;
        await _unitOfWork.OrderRepo.UpdateAsync(entityMapper);
        return Success<string>(_localizer[LanguageKey.UpdateProcess]);
    }

    public async Task<ResponseApp<string>> UpdateOrderStatus(int oderId, int status)
    {

        var entity = await _unitOfWork.OrderRepo.GetByIdAsync(p => p.Id == oderId);
        if (entity == null) return NotFound<string>(_localizer[LanguageKey.NotFound]);
        entity.OrderStatus = status;
        entity.ModifiedDate = DateTime.Now;
        await _unitOfWork.OrderRepo.UpdateAsync(entity);
        return Success<string>(_localizer[LanguageKey.UpdateProcess]);

    }

    public async Task<ResponseApp<string>> DeleteOrder(int entityId)
    {
        var entity = await _unitOfWork.OrderRepo.GetByIdAsync(p => p.Id == entityId);
        if (entity == null) return NotFound<string>(_localizer[LanguageKey.NotFound]);
        await _unitOfWork.OrderRepo.DeleteAsync(entity);
        return Deleted<string>(_localizer[LanguageKey.DeletedSuccessfully]);
    }

    #endregion

}
