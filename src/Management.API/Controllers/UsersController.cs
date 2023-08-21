using Accounts.Data.Context;
using Accounts.Domain.Models;
using Auth.Contract.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Account;

namespace ManagementApp.API.Controllers;

[ApiController]
[Route("auth")]
public class UsersController : ControllerBase
{
    private readonly IAuthenticationService _service;
    private readonly IMapper _mapper;
    private readonly AppDbContext _context;

    public UsersController(IAuthenticationService service, IMapper mapper, AppDbContext context)
    {
        _service = service;
        _mapper = mapper;
        _context = context;
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
        await _service.LoginAsync(user.Email, user.Password);

        return Ok();
    }
    
    [HttpGet("all/users")]
    public IAsyncEnumerable<object> GetAll()
    {
        return _context.Users.AsAsyncEnumerable();
    }
    
    [HttpGet("all/accounts")]
    [Authorize]
    public IAsyncEnumerable<object> GetAllAccounts()
    {
        return _context.Accounts
            .AsAsyncEnumerable();
    }
}