using ECommerce.Application.Dtos.AuthDtos;
using ECommerce.Application.IBusiness.IAuthBusiness;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuth _repo;

    public AuthController(IAuth repo)
    {
        _repo = repo;
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var result = await _repo.Login(dto);
        return Ok(result);
    }
}