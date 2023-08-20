namespace Accounts.Domain.Models;

public class AccountModel
{
    public Guid Id { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }
    
    public string Name { get; set; }

    public string LastName { get; set; }

    public DateTime Birthday { get; set; }

    public Guid UserId { get; set; }
}