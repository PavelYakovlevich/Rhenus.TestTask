using Accounts.Domain.Models;
using Auth.Contract.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Models.Account;

namespace ManagementApp.API.Controllers;

[ApiController]
[Route("auth")]
public class UsersController : ControllerBase
{
    private readonly IAuthenticationService _service;
    private readonly IMapper _mapper;

    public UsersController(IAuthenticationService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }
    
    [HttpPost("registration")]
    public async Task<IActionResult> Register([FromBody] AccountCreationModel user)
    {
        var model = _mapper.Map<AccountRegistrationModel>(user);

        await _service.RegisterAsync(model);
        
        return CreatedAtAction(nameof(Register), null);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel user)
    {
        throw new NotImplementedException();
    }

}