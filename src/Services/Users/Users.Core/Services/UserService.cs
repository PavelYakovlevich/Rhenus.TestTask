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
    
    public IAsyncEnumerable<User> ReadAsync(int skip, int count)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Guid id, User user)
    {
        throw new NotImplementedException();
    }

    public Task CreateAsync(User user)
    {
        throw new NotImplementedException();
    }
}