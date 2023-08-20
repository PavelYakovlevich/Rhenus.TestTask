using Users.Domain.Models;

namespace Users.Contract.Repositories;

public interface IUserRepository
{
    IAsyncEnumerable<UserModel> ReadAsync(int skip, int count, CancellationToken? token);

    Task<bool> DeleteAsync(Guid id, CancellationToken? token);

    Task<bool> UpdateAsync(Guid id, UserModel user, CancellationToken? token);

    Task<bool> CreateAsync(UserModel user, CancellationToken? token);
}