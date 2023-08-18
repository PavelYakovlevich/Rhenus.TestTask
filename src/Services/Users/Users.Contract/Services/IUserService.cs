using Users.Domain.Models;

namespace Users.Contract.Services;

public interface IUserService
{
    IAsyncEnumerable<User> ReadAsync(int skip, int count);

    Task DeleteAsync(Guid id);

    Task UpdateAsync(Guid id, User user);

    Task CreateAsync(User user);
}