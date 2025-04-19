using ECommerce.Application.Dtos.AuthDtos;
using ECommerce.Application.Helpers;
using ECommerce.Application.IBusiness.IAuthBusiness;
using ECommerce.Core.Common.Response;
using ECommerce.Core.Entities;
using ECommerce.Core.Resources;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ECommerce.Application.Business.AuthBusiness;

public class Auth : ResponseHandler, IAuth
{
    private readonly IStringLocalizer<SharedResources> _localizer;
    private readonly JwtOptions _jwtSettings;
    private readonly UserManager<ApplicationUser> _userManager;
    public Auth(UserManager<ApplicationUser> userManager, IStringLocalizer<SharedResources> localizer, JwtOptions jwtSettings) : base(localizer)
    {
        _userManager = userManager;
        _localizer = localizer;
        _jwtSettings = jwtSettings;

    }
    public async Task<ResponseApp<string>> Login(LoginDto dto)
    {
        var user = await _userManager.FindByNameAsync(dto.Username);
        var userPassword = await _userManager.CheckPasswordAsync(user, dto.Password);

        if (user == null || !userPassword) return BadRequest<string>("User Name Or Password May be Wrong !");
        else
        {
            var claims = new[]
            {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SigningKey));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwtSettings.TokenExpire),
                signingCredentials: signingCredentials
            );
            var Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return Success(Token);

        }
    }
}
