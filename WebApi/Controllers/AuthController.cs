using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Application.LogicInterfaces;
using Domain.DTOs;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly IAuthLogic _authLogic;

    public AuthController(IConfiguration config, IAuthLogic authLogic)
    {
        _config = config;
        _authLogic = authLogic;
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult> Login([FromBody] UserLoginDto userLoginDto)
    {
        try
        {
            var user = await _authLogic.ValidateUser(userLoginDto.Username, userLoginDto.Password);
            var token = GenerateJwt(user);
            return Ok(token);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    private IEnumerable<Claim> GenerateClaims(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, _config["Jwt:Subject"]),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim(ClaimTypes.Name, user.Username)
        };
        
        return claims.ToList();
    }

    private string GenerateJwt(User user)
    {
        var claims = GenerateClaims(user);

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var header = new JwtHeader(signIn);

        var payload = new JwtPayload(
            _config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            null,
            DateTime.UtcNow.AddMinutes(60));
        
        var token = new JwtSecurityToken(header, payload);
        var serializedToken = new JwtSecurityTokenHandler().WriteToken(token);
        return serializedToken;
    }
}