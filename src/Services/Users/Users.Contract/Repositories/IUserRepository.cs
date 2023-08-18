using Users.Domain.Models;

namespace Users.Contract.Repositories;

public interface IUserRepository
{
    IAsyncEnumerable<User> ReadAsync(int skip, int count, CancellationToken? token);

    Task DeleteAsync(Guid id, CancellationToken? token);

    Task<bool> UpdateAsync(Guid id, User user, CancellationToken? token);

    Task<bool> CreateAsync(User user, CancellationToken? token);
}