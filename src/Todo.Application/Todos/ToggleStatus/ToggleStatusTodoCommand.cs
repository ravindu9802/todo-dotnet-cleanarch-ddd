using MediatR;

namespace Todo.Application.Todos.ToggleStatus;

public record ToggleStatusTodoCommand(Guid Id, bool IsCompleted): IRequest<bool>;