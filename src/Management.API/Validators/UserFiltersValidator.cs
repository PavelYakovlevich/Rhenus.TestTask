using FluentValidation;
using Models.Users;

namespace ManagementApp.API.Validators;

public class UserFiltersValidator : AbstractValidator<UserFilters>
{
    public UserFiltersValidator()
    {
        RuleFor(filters => filters.Count).GreaterThan(0)
            .WithMessage("count must be greater than 0");
        RuleFor(filters => filters.Skip).GreaterThan(0)
            .WithMessage("skip must be greater than 0");
    }
}