using Todo.Application.Abstractions.Messaging;
using Todo.Domain.Enums;

namespace Todo.Application.Users.Add;

public record AddUserCommand(string FirstName, string LastName, string Email, UserRole Role) : ICommand<Guid>;