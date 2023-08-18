using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Models.Users;

namespace ManagementApp.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateUser(UserModel user)
    {
        throw new NotImplementedException();
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        throw new NotImplementedException();
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateUser([Required] Guid id, UserModel model)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    public IAsyncEnumerable<UserModel> GetUsers([FromQuery] int skip, [FromQuery] int count)
    {
        throw new NotImplementedException();
    }
}