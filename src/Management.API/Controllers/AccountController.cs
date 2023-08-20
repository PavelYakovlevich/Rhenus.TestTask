using Microsoft.AspNetCore.Mvc;
using Models.Users;
using CreateUserModel = Users.Domain.Models.CreateUserModel;

namespace ManagementApp.API.Controllers;

[ApiController]
[Route("auth")]
public class AccountController : ControllerBase
{
    [HttpPost("registration")]
    public async Task<IActionResult> Register([FromBody] CreateUserModel user)
    {
        throw new NotImplementedException();
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel user)
    {
        throw new NotImplementedException();
    }

}