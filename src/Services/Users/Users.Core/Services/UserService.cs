using Exceptions;
using Users.Contract.Repositories;
using Users.Contract.Services;
using Users.Domain.Models;

namespace Users.Core.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }
    
    public IAsyncEnumerable<UserModel> ReadAsync(int skip, int count, CancellationToken token)
    {
        return _repository.ReadAsync(skip, count, token);
    }

    public async Task DeleteAsync(Guid id, CancellationToken token)
    {
        if (!await _repository.DeleteAsync(id, token))
        {
            throw new NotFoundException($"User with id {id} was not found");
        }
    }

    public async Task UpdateAsync(Guid id, UserModel user, CancellationToken token)
    {
        if (!await _repository.UpdateAsync(id, user, token))
        {
            throw new NotFoundException($"User with id {id} was not found");
        }
    }

    public async Task<Guid> CreateAsync(UserModel user, CancellationToken token)
    {
        user.Id = Guid.NewGuid();
        
        await _repository.CreateAsync(user, token);

        return user.Id;
    }
}