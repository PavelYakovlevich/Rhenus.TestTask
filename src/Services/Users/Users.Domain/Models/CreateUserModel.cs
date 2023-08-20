namespace Users.Domain.Models;

public class CreateUserModel : UserModel
{
    public string Email { get; set; }
    
    public string Password { get; set; }
}