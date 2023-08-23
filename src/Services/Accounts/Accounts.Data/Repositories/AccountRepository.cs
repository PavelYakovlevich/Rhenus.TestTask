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

    public async Task<AccountModel?> ReadByIdAsync(Guid id)
    {
        var targetUser = await _context.Accounts.FirstOrDefaultAsync(user => user.Id == id);

        return _mapper.Map<AccountModel>(targetUser);
    }

    public async IAsyncEnumerable<AccountModel> ReadAsync(int skip, int count)
    {
        var users = _context.Accounts.OrderBy(account => account.Id)
            .Skip(skip)
            .Take(count)
            .AsNoTracking();
        
        foreach (var user in users)
        {
            yield return _mapper.Map<AccountModel>(user);
        }
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var targetUser = await _context.Accounts.FirstOrDefaultAsync(user => user.Id == id);
        if (targetUser is null)
        {
            return false;
        }

        _context.Accounts.Remove(targetUser);

        return await _context.SaveChangesAsync() != 0;
    }

    public async Task<bool> UpdateAsync(Guid id, AccountModel userModel)
    {
        var targetUser = await _context.Accounts.FirstOrDefaultAsync(user => user.Id == id);
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