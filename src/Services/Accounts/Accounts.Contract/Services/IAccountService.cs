using Accounts.Domain.Models;

namespace Accounts.Contract.Services;

public interface IAccountService
{
    IAsyncEnumerable<AccountModel> ReadAsync(int skip, int count);

    Task DeleteAsync(Guid id);

    Task UpdateAsync(Guid id, AccountModel user);

    Task CreateAsync(AccountRegistrationModel accountRegistrationModelModel);
}