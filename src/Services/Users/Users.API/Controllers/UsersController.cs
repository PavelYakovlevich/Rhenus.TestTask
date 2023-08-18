using Microsoft.AspNetCore.Mvc;

namespace ManagementApp.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    [HttpPost]
    public async Task<Guid> CreateUser()
    {
        throw new NotImplementedException();
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        throw new NotImplementedException();
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateUser(Guid id)
    {
        throw new NotImplementedException();
    }
}