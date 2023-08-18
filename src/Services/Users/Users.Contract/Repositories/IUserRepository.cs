using Users.Domain.Models;

namespace Users.Contract.Repositories;

public interface IUserRepository
{
    IAsyncEnumerable<User> ReadAsync(int skip, int count);

    Task DeleteAsync(Guid id);

    Task UpdateAsync(Guid id, User user);

    Task CreateAsync(User user);
}