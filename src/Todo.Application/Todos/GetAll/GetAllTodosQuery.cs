using MediatR;

namespace Todo.Application.Todos.GetAll;

public record GetAllTodosQuery(Guid UserId) : IRequest<List<Domain.Entities.Todo>>;