using FluentValidation;
using Models.Account;

namespace ManagementApp.API.Validators;

public class CreateAccountModelValidator : AbstractValidator<AccountCreationModel>
{
    public CreateAccountModelValidator()
    {
        RuleFor(account => account.Email).EmailAddress();
        RuleFor(account => account.Password).NotEmpty();
    }
}