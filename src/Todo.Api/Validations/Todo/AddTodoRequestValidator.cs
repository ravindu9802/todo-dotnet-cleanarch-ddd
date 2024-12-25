using FluentValidation;
using Todo.Application.Todos.Add;

namespace Todo.Api.Validations.Todo;

internal sealed class AddTodoRequestValidator : AbstractValidator<AddTodoRequest>
{
    public AddTodoRequestValidator()
    {
        RuleFor(t => t.Title)
            .NotEmpty().WithMessage("Title is required.");

        RuleFor(t => t.UserId)
            .NotEmpty().WithMessage("User id is required.");
    }
}
