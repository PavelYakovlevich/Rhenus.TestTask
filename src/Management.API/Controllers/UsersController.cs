using Accounts.Contract.Services;
using Accounts.Domain.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Models.Account;

namespace ManagementApp.API.Controllers;

[ApiController]
[Route("auth")]
public class UsersController : ControllerBase
{
    private readonly IAccountService _service;
    private readonly IMapper _mapper;

    public UsersController(IAccountService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }
    
    [HttpPost("registration")]
    public async Task<IActionResult> Register([FromBody] CreateAccountModel user)
    {
        var model = _mapper.Map<AccountRegistrationModel>(user);

        await _service.CreateAsync(model);
        
        return CreatedAtAction(nameof(Register), null);
    }
}