using MediatR;
using Todo.Domain.Primitives;

namespace Todo.Application.Todos.Delete;

public record DeleteTodoCommand(Guid Id): IRequest<Result>;