using Accounts.Contract.Repositories;
using Accounts.Contract.Services;
using Exceptions;
using Accounts.Domain.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace Accounts.Core.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _repository;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IMapper _mapper;

    public AccountService(IAccountRepository repository, UserManager<IdentityUser> userManager, IMapper mapper)
    {
        _repository = repository;
        _userManager = userManager;
        _mapper = mapper;
    }
    
    public IAsyncEnumerable<AccountModel> ReadAsync(int skip, int count)
    {
        return _repository.ReadAsync(skip, count);
    }

    public async Task DeleteAsync(Guid id)
    {
        await DeleteIdentityAsync(id.ToString());

        await _repository.DeleteAsync(id);
    }

    public async Task UpdateAsync(Guid id, AccountModel user)
    {
        // Fix later
        user.Id = id;
        
        if (!await _repository.UpdateAsync(id, user))
        {
            throw new NotFoundException($"User with id {id} was not found");
        }
    }

    public async Task CreateAsync(AccountRegistrationModel accountRegistrationModelModel)
    {
        var newUser = await CreateIdentityAsync(accountRegistrationModelModel);
        
        var newAccount = _mapper.Map<AccountModel>(accountRegistrationModelModel);

        var userId = Guid.Parse(newUser.Id);
        newAccount.Id = userId;

        try
        {
            await _repository.CreateAsync(newAccount);
        }
        catch
        {
            await DeleteIdentityAsync(newUser.Id);
        }
    }

    private async Task DeleteIdentityAsync(string id)
    {
        var identity = await _userManager.FindByIdAsync(id);

        if (identity is null)
        {
            throw new NotFoundException($"User with id '{id}' was not found.");
        }
        
        var result = await _userManager.DeleteAsync(identity);

        if (!result.Succeeded)
        {
            throw new OperationFailedException(
                $"Identity deletion failed with message: {result.Errors.FirstOrDefault()}");
        }
    }

    private async Task<IdentityUser> CreateIdentityAsync(AccountRegistrationModel accountRegistrationModelModel)
    {
        var user = await _userManager.FindByEmailAsync(accountRegistrationModelModel.Email);

        if (user is not null)
        {
            throw new AlreadyExistsException($"User with email '{accountRegistrationModelModel.Email}' exists");
        }

        var newUser = _mapper.Map<IdentityUser>(accountRegistrationModelModel);
        newUser.UserName = accountRegistrationModelModel.Email;
        
        var result = await _userManager.CreateAsync(newUser, accountRegistrationModelModel.Password);

        if (!result.Succeeded)
        {
            throw new RegistrationFailedException(result.Errors.FirstOrDefault()?.Description ?? string.Empty);
        }

        return newUser;
    }
}