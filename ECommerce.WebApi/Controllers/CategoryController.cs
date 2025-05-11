using ECommerce.Application.Dtos.CategoryDtos;
using ECommerce.Application.IBusiness.ICategoryBusiness;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace ECommerce.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class CategoryController : ControllerBase
{


    #region Fields
    private readonly ICategoryBL _repo;
    #endregion
    #region Constractor
    public CategoryController(ICategoryBL repo)
    {
        _repo = repo;
    }
    #endregion

    #region Handle Action
    [HttpGet("GetAllCategories")]
    public async Task<IActionResult> GetAllCategories()
    {
        var result = await _repo.GetAll();
        return Ok(result);
    }
    [HttpGet("GetCategoryById/{id}")]
    public async Task<IActionResult> GetCategoryById(int id)
    {
        var result = await _repo.GetById(id);
        return Ok(result);
    }

    [HttpPost("AddCategory")]
    public async Task<IActionResult> AddCategory([FromForm] CategoryEditDto category)
    {
        var result = await _repo.AddNew(category);
        return Ok(result);
    }


    [HttpPut("UpdateCategory")]
    public async Task<IActionResult> UpdateCategory([FromForm] CategoryEditDto category)
    {
        var result = await _repo.UpdateOneRow(category);
        return Ok(result);
    }
    [HttpDelete("DeleteCategory/{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var result = await _repo.DeleteOneRow(id);
        return Ok(result);
    }
    [HttpPut("AddCategoryToNavBar")]
    public async Task<IActionResult> AddCategoryToNavBar(NavCategoryDto dto)
    {
        var result = await _repo.AddCategoryToNavBar(dto);
        return Ok(result);
    }
    #endregion
}
