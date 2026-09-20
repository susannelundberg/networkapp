using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NetworkApp.Application;
using NetworkApp.Domain;
using NetworkApp.Infrastructure.Data;

namespace NetworkApp.Api.Controllers;
    
    [Route("api/user")]
    [ApiController]
    public class UserController(AppDbContext context) : ControllerBase
    {

    [Authorize]
    [HttpPost("test")]
    public async Task<ActionResult> Test()
    {
        return Ok(new { Sucess = true, message = "Det funkade" });
    }

}


