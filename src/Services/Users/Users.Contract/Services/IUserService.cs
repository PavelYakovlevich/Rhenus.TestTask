using Users.Domain.Models;

namespace Users.Contract.Services;

public interface IUserService
{
    IAsyncEnumerable<User> ReadAsync(int skip, int count, CancellationToken token);

    Task DeleteAsync(Guid id, CancellationToken token);

    Task UpdateAsync(Guid id, User user, CancellationToken token);

    Task<Guid> CreateAsync(User user, CancellationToken token);
}