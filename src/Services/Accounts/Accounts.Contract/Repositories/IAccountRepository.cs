using Accounts.Domain.Models;

namespace Accounts.Contract.Repositories;

public interface IAccountRepository
{
    IAsyncEnumerable<AccountModel> ReadAsync(int skip, int count);

    Task<bool> DeleteAsync(Guid id);

    Task<bool> UpdateAsync(Guid id, AccountModel accountModel);

    Task<bool> CreateAsync(AccountModel accountModel);
}