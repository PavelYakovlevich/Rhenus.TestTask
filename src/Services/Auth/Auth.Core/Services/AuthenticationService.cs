using Auth.Contract.Services;
using AutoMapper;
using Exceptions;
using Microsoft.AspNetCore.Identity;
using Accounts.Contract.Services;
using Accounts.Domain.Models;

namespace Auth.Core.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IAccountService _accountService;
    private readonly IMapper _mapper;

    public AuthenticationService(
        UserManager<IdentityUser> userManager, 
        SignInManager<IdentityUser> signInManager, 
        IAccountService accountService,
        IMapper mapper)
    {
        _userManager = userManager;
        _accountService = accountService;
        _mapper = mapper;
    }
    
    public async Task<bool> RegisterAsync(AccountRegistrationModel accountRegistrationModelModel)
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
        
        var newAccount = _mapper.Map<AccountModel>(accountRegistrationModelModel);
        newAccount.UserId = Guid.Parse(newUser.Id);
        
        await _accountService.CreateAsync(newAccount);
        
        return result.Succeeded;
    }
}