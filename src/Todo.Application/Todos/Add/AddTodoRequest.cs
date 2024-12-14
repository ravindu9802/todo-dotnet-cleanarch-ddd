namespace Todo.Application.Todos.Add;

public record AddTodoRequest(string Title, string Description, Guid UserId);