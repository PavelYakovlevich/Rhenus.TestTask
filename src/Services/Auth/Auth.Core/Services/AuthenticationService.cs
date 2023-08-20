using Auth.Contract.Services;
using AutoMapper;
using Exceptions;
using Microsoft.AspNetCore.Identity;
using Accounts.Contract.Services;
using Accounts.Domain.Models;

namespace Auth.Core.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<AccountModel> _userManager;
    private readonly SignInManager<AccountModel> _signInManager;
    private readonly IAccountService _accountService;
    private readonly IMapper _mapper;

    public AuthenticationService(
        UserManager<AccountModel> userManager, 
        SignInManager<AccountModel> signInManager, 
        IAccountService accountService,
        IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _accountService = accountService;
        _mapper = mapper;
    }
    
    public async Task<bool> RegisterAsync(AccountRegistrationModel accountModel)
    {
        var user = await _userManager.FindByEmailAsync(accountModel.Email);

        if (user is not null)
        {
            throw new AlreadyExistsException($"User with email '{accountModel.Email}' exists");
        }

        var newAccount = _mapper.Map<AccountModel>(accountModel);
        
        var result = await _userManager.CreateAsync(newAccount, accountModel.Password);

        if (result.Succeeded)
        {
            await _accountService.CreateAsync(newAccount);
        }

        return result.Succeeded;
    }

    public async Task LoginAsync(string email, string password)
    {
        var result =  await _signInManager.PasswordSignInAsync(email, password, false, false);

        if (!result.Succeeded)
        {
            throw new WrongCredentialsException("Invalid email or password");
        }
    }
}