using MediatR;
using Todo.Domain.Enums;
using Todo.Domain.Primitives;

namespace Todo.Application.Users.Add;

public record AddUserCommand(string FirstName, string LastName, string Email, UserRole Role) : IRequest<Result<Guid>>;