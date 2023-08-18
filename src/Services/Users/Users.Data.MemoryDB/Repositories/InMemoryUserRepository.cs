using AutoMapper;
using Users.Contract.Repositories;
using Users.Data.Models;
using Users.Domain.Models;

namespace Users.Data.Repositories;

public class InMemoryUserRepository : IUserRepository
{
    private readonly IMapper _mapper;
    private readonly IDictionary<Guid, UserDbModel> _storage;
    
    public InMemoryUserRepository(IMapper mapper)
    {
        _mapper = mapper;
        _storage = new SortedDictionary<Guid, UserDbModel>();
    }
    
    public async IAsyncEnumerable<User> ReadAsync(int skip, int count, CancellationToken? token)
    {
        var users = _storage.Skip(skip)
            .Take(count);
        
        foreach (var userInfo in users)
        {
            yield return _mapper.Map<User>(userInfo.Value);
        }
    }

    public Task<bool> DeleteAsync(Guid id, CancellationToken? token)
    {
        return Task.FromResult(_storage.Remove(id));
    }

    public Task<bool> UpdateAsync(Guid id, User user, CancellationToken? token)
    {
        if (!_storage.ContainsKey(id))
        {
            return Task.FromResult(false);
        }

        _mapper.Map(user, _storage[id]);

        return Task.FromResult(true);
    }

    public Task<bool> CreateAsync(User user, CancellationToken? token)
    {
        var dbModel = _mapper.Map<UserDbModel>(user);

        return Task.FromResult(_storage.TryAdd(user.Id, dbModel));
    }
}