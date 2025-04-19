using ECommerce.Application.Dtos.MenuDtos;
using ECommerce.Application.IBusiness.IMenuBusiness;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenusController : ControllerBase
    {
        #region Fields
        private readonly IMenuBL _repo;
        #endregion
        #region Constractor
        public MenusController(IMenuBL repo)
        {
            _repo = repo;
        }
        #endregion

        #region Handle Action
        [HttpGet("GetAllMenus")]
        public async Task<IActionResult> GetAllMenus()
        {
            var result = await _repo.GetAll();
            return Ok(result);
        }
        [HttpGet("GetMenuById/{id}")]
        public async Task<IActionResult> GetMenuById(int id)
        {
            var result = await _repo.GetById(id);
            return Ok(result);
        }

        [HttpPost("AddMenu")]
        public async Task<IActionResult> AddMenu([FromForm] MenuDto dto)
        {
            var result = await _repo.AddNew(dto);
            return Ok(result);
        }


        [HttpPut("UpdateMenu")]
        public async Task<IActionResult> UpdateMenu([FromForm] MenuDto dto)
        {
            var result = await _repo.Update(dto);
            return Ok(result);
        }
        [HttpDelete("DeleteMenu/{id}")]
        public async Task<IActionResult> DeleteMenu(int id)
        {
            var result = await _repo.Delete(id);
            return Ok(result);
        }

        #endregion
    }
}
