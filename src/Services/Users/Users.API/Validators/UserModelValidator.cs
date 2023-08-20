using FluentValidation;
using Models.Users;

namespace ManagementApp.API.Validators;

public class UserModelValidator : AbstractValidator<UserModel>
{
    public UserModelValidator()
    {
        RuleFor(user => user.Name).NotEmpty().WithMessage("Please specify a name");
        RuleFor(user => user.LastName).NotEmpty().WithMessage("Please specify a last name");
        RuleFor(user => user.Birthday).NotEmpty().WithMessage("Please specify a birthday")
            .LessThan(DateTime.UtcNow).WithMessage($"Birthday must be less ${DateTime.UtcNow:d}");
    }
}