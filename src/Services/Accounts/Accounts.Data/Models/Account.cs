using Microsoft.AspNetCore.Identity;

namespace Accounts.Data.Models;

public class Account
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string LastName { get; set; }

    public DateTime Birthday { get; set; }
    
    public string UserId { get; set; }
    
    public virtual IdentityUser<Guid> User { get; set; }
}