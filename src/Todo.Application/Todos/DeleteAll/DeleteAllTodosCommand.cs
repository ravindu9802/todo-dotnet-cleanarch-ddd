using Todo.Application.Abstractions.Messaging;

namespace Todo.Application.Todos.DeleteAll;

public record DeleteAllTodosCommand(Guid UserId) : ICommand;
