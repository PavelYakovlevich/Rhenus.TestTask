using Accounts.Contract.Repositories;
using Accounts.Data.Context;
using Accounts.Data.Models;
using Accounts.Domain.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Accounts.Data.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    
    public AccountRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async IAsyncEnumerable<AccountModel> ReadAsync(int skip, int count)
    {
        var users = _context.Users.Skip(skip)
            .Take(count)
            .AsNoTracking();
        
        foreach (var user in users)
        {
            yield return _mapper.Map<AccountModel>(user);
        }
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var idString = id.ToString();
        
        var targetUser = await _context.Users.FirstOrDefaultAsync(user => user.Id == idString);
        if (targetUser is null)
        {
            return false;
        }

        _context.Users.Remove(targetUser);

        return await _context.SaveChangesAsync() != 0;
    }

    public async Task<bool> UpdateAsync(Guid id, AccountModel userModel)
    {
        var idString = id.ToString();

        var targetUser = await _context.Users.FirstOrDefaultAsync(user => user.Id == idString);
        if (targetUser is null)
        {
            return false;
        }

        _mapper.Map(userModel, targetUser);

        return await _context.SaveChangesAsync() != 0;
    }

    public async Task<bool> CreateAsync(AccountModel accountModel)
    {
        var account = _mapper.Map<Account>(accountModel);

        await _context.Accounts.AddAsync(account);
        
        return await _context.SaveChangesAsync() != 0;
    }
}