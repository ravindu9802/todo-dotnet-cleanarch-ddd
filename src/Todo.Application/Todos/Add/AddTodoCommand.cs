using MediatR;

namespace Todo.Application.Todos.Add;

public record AddTodoCommand(string Title, string Description, Guid UserId) : IRequest<Guid?>;