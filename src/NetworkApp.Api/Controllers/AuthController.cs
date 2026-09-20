using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetworkApp.Application;
using NetworkApp.Application.Interfaces;
using NetworkApp.Domain.Entities;
using NetworkApp.Infrastructure.Data;

namespace NetworkApp.Api;

[Authorize]
[Route("api/auth")]
[ApiController]
public class AuthController(AppDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ITokenService tokenService) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult> RegisterUser(RegisterUserDto model)
    {
        try
        {
            User user = new()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email,
                Title = model.Title,
                Employer = model.Employer,
                Duties = model.Duties,
                Interests = model.Interests
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return StatusCode(400, result.Errors);
            }

            await userManager.AddToRoleAsync(user, "User");
            
            return StatusCode(201, new{Success = true, message = $"Du har nu skapat ett konto för {user.Email}"});
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message); // Endast under utveckling
        }
        
    }

    [AllowAnonymous]
    // [HttpPost("login")] KOLLA PÅ DETTA
    [Route("api/login")]
    public async Task<ActionResult> LoginUser (LoginUserDto model)
    {
        var user = await userManager.FindByNameAsync(model.Email);

        if (user is null || !await userManager.CheckPasswordAsync(user, model.Password))
        {
            return Unauthorized(new {Success = false, message = "Felaktig inloggning, kontrollera användarnamn och lösenord."});
        }

        var token = await tokenService.CreateToken(user);

        return Ok(new {Success = true, user.Email, token });
    }

    [HttpPost("test")]
    public async Task<ActionResult> Test ()
    {
        return Ok(new {Sucess = true, message = "Det funkade"});
    }
}