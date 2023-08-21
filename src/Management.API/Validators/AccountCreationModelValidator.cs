using FluentValidation;
using Models.Account;

namespace ManagementApp.API.Validators;

public class AccountCreationModelValidator : AccountValidator<CreateAccountModel>
{
    public AccountCreationModelValidator()
    {
        RuleFor(account => account.Email).EmailAddress();
        RuleFor(account => account.Password).NotEmpty();
    }
}