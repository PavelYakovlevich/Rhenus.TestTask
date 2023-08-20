using Microsoft.AspNetCore.Identity;

namespace Users.Data.Models;

public class UserDbModel : IdentityUser<Guid>
{
    public string Name { get; set; }

    public string LastName { get; set; }

    public DateTime Birthday { get; set; }
}