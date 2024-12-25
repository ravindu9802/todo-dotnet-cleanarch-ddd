using Todo.Application.Abstractions.Messaging;

namespace Todo.Application.Todos.GetAll;

public record GetAllTodosQuery(Guid UserId) : IQuery<List<Domain.Entities.Todo>>;