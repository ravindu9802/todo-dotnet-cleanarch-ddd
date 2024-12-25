using Todo.Application.Abstractions.Messaging;

namespace Todo.Application.Todos.Add;

public record AddTodoCommand(string Title, string Description, Guid UserId) : ICommand<Guid>;