using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Models.Users;
using Users.Contract.Services;
using Users.Domain.Models;

namespace ManagementApp.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _service;
    private readonly IMapper _mapper;
    private readonly CancellationToken _tokenMock;

    public UsersController(IUserService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;

        using var tokenSource = new CancellationTokenSource();
        _tokenMock = tokenSource.Token;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateUser(UserModel user)
    {
        var model = _mapper.Map<User>(user);

        var id = await _service.CreateAsync(model, _tokenMock);
        
        return CreatedAtAction(nameof(CreateUser), id);
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        await _service.DeleteAsync(id, _tokenMock);

        return NoContent();
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateUser([Required] Guid id, UserModel user)
    {
        var model = _mapper.Map<User>(user);
        
        await _service.UpdateAsync(id, model, _tokenMock);

        return NoContent();
    }

    [HttpGet]
    public async IAsyncEnumerable<UserModel> GetUsers([FromQuery] int skip, [FromQuery] int count)
    {
        await foreach (var user in _service.ReadAsync(skip, count, _tokenMock))
        {
            yield return _mapper.Map<UserModel>(user);
        }
    }
}