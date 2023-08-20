namespace Models.Account;

public class AccountCreationModel : AccountModel
{
    public string Email { get; set; }
    
    public string Password { get; set; }
}