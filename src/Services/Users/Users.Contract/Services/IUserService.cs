using Users.Domain.Models;

namespace Users.Contract.Services;

public interface IUserService
{
    IAsyncEnumerable<UserModel> ReadAsync(int skip, int count, CancellationToken token);

    Task DeleteAsync(Guid id, CancellationToken token);

    Task UpdateAsync(Guid id, UserModel user, CancellationToken token);

    Task<Guid> CreateAsync(UserModel user, CancellationToken token);
}