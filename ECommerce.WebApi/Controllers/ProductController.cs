using ECommerce.Application.Dtos.ImagesDtos;
using ECommerce.Application.Dtos.ProductDtos;
using ECommerce.Application.IBusiness.IProductBusiness;
using ECommerce.Core.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ECommerce.WebApi.Controllers;

//[Route("api/[controller]")]
[Route("api/Service")]
[ApiController]
[AllowAnonymous]
public class ProductController : ControllerBase
{
    #region Fields
    private readonly IProductBL _repo;

    #endregion

    #region Constractor
    public ProductController(IProductBL repo)
    {
        _repo = repo;

    }
    #endregion
    #region Handle Action
    [AllowAnonymous]
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll([FromQuery] Paginat Criteria)
    {
        var result = await _repo.GetAllProducts(Criteria);
        return Ok(result);
    }
    [AllowAnonymous]
    [HttpGet("GetHomeScreenProducts")]
    public async Task<IActionResult> GetHomeScreenProducts([FromQuery] Paginat Criteria)
    {
        var result = await _repo.GetHomeScreenProducts(Criteria);
        return Ok(result);
    }
    [AllowAnonymous]
    [HttpGet("GetByCategoryId/{catId}")]
    public async Task<IActionResult> GetByCategoryId([FromQuery] Paginat Criteria, int catId)
    {
        var result = await _repo.GetProductsByCategoryId(Criteria, catId);
        return Ok(result);
    }
    [AllowAnonymous]
    [HttpGet("GetServiceById/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _repo.GetProductById(id);
        return Ok(result);
    }
    [AllowAnonymous]
    [HttpPost("AddService")]
    public async Task<IActionResult> Add([FromForm] ProductEditDto product)
    {
        var result = await _repo.AddProduct(product);
        return Ok(result);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromForm] ProductEditDto product)
    {
        var result = await _repo.UpdateProduct(product);
        return Ok(result);
    }
    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> DeleteById(int id)
    {
        var result = await _repo.DeleteProduct(id);
        return Ok(result);
    }
    [HttpPost("DeleteImageService")]
    public async Task<IActionResult> DeleteImageProduct(DeleteImagesDto dto)
    {
        var result = await _repo.DeleteImageProduct(dto);
        return Ok(result);
    }
    #endregion
}
