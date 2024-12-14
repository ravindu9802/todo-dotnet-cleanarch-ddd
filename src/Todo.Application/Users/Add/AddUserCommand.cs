using MediatR;

namespace Todo.Application.Users.Add;

public record AddUserCommand(string FirstName, string LastName) : IRequest<Guid?>;