using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NetworkApp.Application.Interfaces;
using NetworkApp.Domain.Entities;

namespace NetworkApp.Infrastructure;

public class TokenService (UserManager<User> userManager, IConfiguration config) : ITokenService
{
    public async Task<string> CreateToken(User user)
    {
        var role = await userManager.GetRolesAsync(user);

        List<Claim> claims = [
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Role, role.First())
        ];

        var tokenKey = config["tokenSettings:tokenKey"];
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var options = new JwtSecurityToken(
            issuer: null,
            audience: null,
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(options);
    }
}
