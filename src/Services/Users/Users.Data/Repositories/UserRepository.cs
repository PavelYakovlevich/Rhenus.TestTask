using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Users.Contract.Repositories;
using Users.Data.Context;
using Users.Data.Models;
using Users.Domain.Models;

namespace Users.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    
    public UserRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async IAsyncEnumerable<UserModel> ReadAsync(int skip, int count, CancellationToken? token)
    {
        var users = _context.Users.Skip(skip)
            .Take(count)
            .AsNoTracking();
        
        foreach (var user in users)
        {
            yield return _mapper.Map<UserModel>(user);
        }
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken? token)
    {
        var targetUser = await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
        if (targetUser is null)
        {
            return false;
        }

        _context.Users.Remove(targetUser);

        return await _context.SaveChangesAsync() != 0;
    }

    public async Task<bool> UpdateAsync(Guid id, UserModel userModel, CancellationToken? token)
    {
        var targetUser = await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
        if (targetUser is null)
        {
            return false;
        }

        _mapper.Map(userModel, targetUser);

        return await _context.SaveChangesAsync() != 0;
    }

    public async Task<bool> CreateAsync(UserModel userModel, CancellationToken? token)
    {
        var user = _mapper.Map<UserDbModel>(userModel);

        await _context.Users.AddAsync(user);

        return await _context.SaveChangesAsync() != 0;
    }
}