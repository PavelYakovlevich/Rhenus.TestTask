using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Accounts.Contract.Services;
using Microsoft.AspNetCore.Authorization;
using Models.Account;
using AccountModel = Accounts.Domain.Models.AccountModel;

namespace ManagementApp.API.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
[ApiController]
[Route("[controller]")]
public class AccountsController : ControllerBase
{
    private readonly IAccountService _service;
    private readonly IMapper _mapper;

    public AccountsController(IAccountService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
    {
        await _service.DeleteAsync(id);

        return NoContent();
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateUser([FromRoute] Guid id, Models.Account.AccountModel user)
    {
        var model = _mapper.Map<AccountModel>(user);
        
        await _service.UpdateAsync(id, model);

        return NoContent();
    }

    [HttpGet]
    public async IAsyncEnumerable<Models.Account.AccountModel> GetUsers([FromQuery] AccountFilters filters)
    {
        await foreach (var user in _service.ReadAsync(filters.Skip, filters.Count))
        {
            yield return _mapper.Map<Models.Account.AccountModel>(user);
        }
    }
}