namespace Models.Account;

public class CreateAccountModel : AccountModel
{
    public string Email { get; set; }
    
    public string Password { get; set; }
}