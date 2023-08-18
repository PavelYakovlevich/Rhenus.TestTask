namespace Users.Data.Models;

public class UserDbModel
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string LastName { get; set; }

    public DateTime Birthday { get; set; }
}