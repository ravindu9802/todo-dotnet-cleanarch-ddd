using MediatR;
using Todo.Domain.Primitives;

namespace Todo.Application.Users.Add;

public record AddUserCommand(string FirstName, string LastName) : IRequest<Result<Guid>>;