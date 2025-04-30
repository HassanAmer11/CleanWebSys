using ECommerce.Application.Dtos.OrdersDtos;
using ECommerce.Application.Helpers;
using ECommerce.Application.IBusiness.IOrderBusiness;
using ECommerce.Core.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = true)] // This hides the controller from Swagger
    public class OrderController : ControllerBase
    {
        #region Fields
        private readonly IOrderRepoBL _repo;

        #endregion

        #region Constractor
        public OrderController(IOrderRepoBL repo)
        {
            _repo = repo;

        }
        #endregion
        #region Handle Action
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] Paginat Criteria)
        {
            var result = await _repo.GetAllOrders(Criteria);
            return Ok(result);
        }
        [HttpGet("GetOrdersAccordingCriteria")]
        public async Task<IActionResult> GetOrdersAccordingCriteria([FromQuery] Criteria Criteria)
        {
            var result = await _repo.GetOrdersAccordingSearchCriteria(Criteria);
            return Ok(result);
        }
        [HttpGet("GetOrderById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _repo.GetOrderById(id);
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] OrdersEditDto order)
        {
            var result = await _repo.CreateOrder(order);
            return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] OrdersEditDto order)
        {
            var result = await _repo.UpdateOrder(order);
            return Ok(result);
        }
        [HttpPut("UpdateOrderStatus")]
        public async Task<IActionResult> UpdateOrderStatus(int oderId, int status)
        {
            var result = await _repo.UpdateOrderStatus(oderId, status);
            return Ok(result);
        }
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var result = await _repo.DeleteOrder(id);
            return Ok(result);
        }

        #endregion
    }
}
