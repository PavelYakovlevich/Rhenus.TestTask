using Accounts.Contract.Repositories;
using Accounts.Contract.Services;
using Exceptions;
using Accounts.Domain.Models;

namespace Accounts.Core.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _repository;

    public AccountService(IAccountRepository repository)
    {
        _repository = repository;
    }
    
    public IAsyncEnumerable<AccountModel> ReadAsync(int skip, int count)
    {
        return _repository.ReadAsync(skip, count);
    }

    public async Task DeleteAsync(Guid id)
    {
        if (!await _repository.DeleteAsync(id))
        {
            throw new NotFoundException($"User with id {id} was not found");
        }
    }

    public async Task UpdateAsync(Guid id, AccountModel user)
    {
        if (!await _repository.UpdateAsync(id, user))
        {
            throw new NotFoundException($"User with id {id} was not found");
        }
    }

    public async Task CreateAsync(AccountModel accountModel)
    {
        accountModel.Id = Guid.NewGuid();
        
        await _repository.CreateAsync(accountModel);
    }
}