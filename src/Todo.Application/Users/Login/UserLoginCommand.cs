using MediatR;
using Todo.Domain.Primitives;

namespace Todo.Application.Users.Login;

public record UserLoginCommand(string Email): IRequest<Result<string>>;