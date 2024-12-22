using MediatR;
using Todo.Domain.Primitives;

namespace Todo.Application.Todos.Add;

public record AddTodoCommand(string Title, string Description, Guid UserId) : IRequest<Result<Guid>>;