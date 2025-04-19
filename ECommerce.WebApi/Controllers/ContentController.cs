
using ECommerce.Application.Dtos.ContentDtos;
using ECommerce.Application.IBusiness.IContentBusiness;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class ContentController : ControllerBase
{


    #region Fields
    private readonly IContentBL _repo;
    #endregion
    #region Constractor
    public ContentController(IContentBL repo)
    {
        _repo = repo;
    }
    #endregion

    #region Handle Action
    [HttpGet("GetAllContents")]
    public async Task<IActionResult> GetAllContents()
    {
        var result = await _repo.GetAll();
        return Ok(result);
    }
    [HttpGet("GetContentById/{id}")]
    public async Task<IActionResult> GetContentById(int id)
    {
        var result = await _repo.GetById(id);
        return Ok(result);
    }

    [HttpPost("AddContent")]
    public async Task<IActionResult> AddContent([FromForm] ContentDto dto)
    {
        var result = await _repo.AddNew(dto);
        return Ok(result);
    }

    [HttpPut("UpdateContent")]
    public async Task<IActionResult> UpdateContent([FromForm] ContentDto dto)
    {
        var result = await _repo.Update(dto);
        return Ok(result);
    }
    [HttpDelete("DeleteContent/{id}")]
    public async Task<IActionResult> DeleteContent(int id)
    {
        var result = await _repo.Delete(id);
        return Ok(result);
    }

    #endregion
}
