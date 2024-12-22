using MediatR;
using Todo.Domain.Primitives;

namespace Todo.Application.Todos.DeleteAll;

public record DeleteAllTodosCommand(Guid UserId) : IRequest<Result>;
