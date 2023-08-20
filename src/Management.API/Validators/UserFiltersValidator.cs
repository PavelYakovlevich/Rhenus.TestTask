using FluentValidation;
using Models.Account;

namespace ManagementApp.API.Validators;

public class AccountFiltersValidator : AbstractValidator<AccountFilters>
{
    public AccountFiltersValidator()
    {
        RuleFor(filters => filters.Count).GreaterThan(0)
            .WithMessage("count must be greater than 0");
        RuleFor(filters => filters.Skip).GreaterThan(0)
            .WithMessage("skip must be greater than 0");
    }
}