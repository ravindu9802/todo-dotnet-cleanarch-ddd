using Todo.Application.Abstractions.Messaging;

namespace Todo.Application.Todos.Delete;

public record DeleteTodoCommand(Guid Id) : ICommand;