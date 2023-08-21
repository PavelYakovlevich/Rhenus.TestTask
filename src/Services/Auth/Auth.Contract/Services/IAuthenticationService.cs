using Accounts.Domain.Models;

namespace Auth.Contract.Services;

public interface IAuthenticationService
{
    Task<bool> RegisterAsync(AccountRegistrationModel userModel);
}