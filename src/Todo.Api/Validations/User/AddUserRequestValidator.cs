using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Todo.Application.Users.Add;

namespace Todo.Api.Validations.User;

internal sealed class AddUserRequestValidator : AbstractValidator<AddUserRequest>
{
    public AddUserRequestValidator()
    {
        RuleFor(u => u.FirstName)
            .NotEmpty().WithMessage("First Name is required.");

        RuleFor(u => u.LastName)
            .NotEmpty().WithMessage("Last Name is required.");

        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format");

        RuleFor(u => u.Role)
            .NotEmpty().WithMessage("Role is required.");

    }
}
