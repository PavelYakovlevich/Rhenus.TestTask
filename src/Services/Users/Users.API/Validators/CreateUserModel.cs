using FluentValidation;
using Models.Users;

namespace ManagementApp.API.Validators;

public class CreateUserModelValidator : AbstractValidator<CreateUserModel>
{
    public CreateUserModelValidator()
    {
        RuleFor(user => user.Email).EmailAddress();
        RuleFor(user => user.Password).NotEmpty();
    }
}