namespace Models.Users;

public class UserModel
{
    public Guid? Id { get; set; }

    public string Name { get; set; }

    public string LastName { get; set; }

    public DateTime Birthday { get; set; }
}