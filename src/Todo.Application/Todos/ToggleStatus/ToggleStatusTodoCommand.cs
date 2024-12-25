using Todo.Application.Abstractions.Messaging;

namespace Todo.Application.Todos.ToggleStatus;

public record ToggleStatusTodoCommand(Guid Id, bool IsCompleted) : ICommand<bool>;