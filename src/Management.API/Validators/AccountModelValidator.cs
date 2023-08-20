using FluentValidation;
using Models.Account;

namespace ManagementApp.API.Validators;

public class AccountModelValidator : AbstractValidator<AccountModel>
{
    public AccountModelValidator()
    {
        RuleFor(account => account.Name).NotEmpty().WithMessage("Please specify a name");
        RuleFor(account => account.LastName).NotEmpty().WithMessage("Please specify a last name");
        RuleFor(account => account.Birthday).NotEmpty().WithMessage("Please specify a birthday")
            .LessThan(DateTime.UtcNow).WithMessage($"Birthday must be less ${DateTime.UtcNow:d}");
    }
}