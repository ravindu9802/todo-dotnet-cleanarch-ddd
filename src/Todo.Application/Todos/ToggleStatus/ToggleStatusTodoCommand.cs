using MediatR;
using Todo.Domain.Primitives;

namespace Todo.Application.Todos.ToggleStatus;

public record ToggleStatusTodoCommand(Guid Id, bool IsCompleted) : IRequest<Result<bool>>;