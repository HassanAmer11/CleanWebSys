using ECommerce.Application.Dtos.GovernoratesDtos;
using ECommerce.Application.IBusiness.IGovernorateBusiness;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class GovernoratesController : ControllerBase
{
    #region Fields
    private readonly IGovernoratesBL _repo;

    #endregion
    #region Constractor
    public GovernoratesController(IGovernoratesBL repo)
    {
        _repo = repo;

    }
    #endregion

    #region Handle Action
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _repo.GetAllGovernorates();
        return Ok(result);
    }
    [HttpGet("GetById/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _repo.GetGovernorateById(id);
        return Ok(result);
    }

    [HttpPost("Add")]
    public async Task<IActionResult> Add(GovernoratesDto governorate)
    {
        var result = await _repo.AddGovernorate(governorate);
        return Ok(result);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update(GovernoratesDto governorate)
    {
        var result = await _repo.UpdateGovernorate(governorate);
        return Ok(result);
    }
    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> DeleteById(int id)
    {
        var result = await _repo.DeleteGovernorate(id);
        return Ok(result);
    }
    [AllowAnonymous]
    [HttpGet("GetGovernoratesWithServicesAsync")]
    public async Task<IActionResult> GetGovernoratesWithProductsAsync()
    {
        var result = await _repo.GetGovernoratesWithProductsAsync();
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet("GetGovernoratesWithServicesByCategoryAsync/{categoryId}")]
    public async Task<IActionResult> GetGovernoratesWithProductsByCategoryAsync(int categoryId)
    {
        var result = await _repo.GetGovernoratesWithProductsByCategoryAsync(categoryId);
        return Ok(result);
    }

    #endregion
}
