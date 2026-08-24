using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NetworkApp.Domain;
using NetworkApp.Infrastructure.Data;

namespace NetworkApp.Api;

    [Route("api/user")]
    [ApiController]
    public class UserController(AppDbContext context) : ControllerBase
    {
        [HttpPost()]
        public async Task<ActionResult> RegisterUser (User user)
    {
        User user1 = new()
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email
        };

        context.Users.Add(user1);
        await context.SaveChangesAsync();
        return Ok("Ny användare tillagd");
    }
    }


