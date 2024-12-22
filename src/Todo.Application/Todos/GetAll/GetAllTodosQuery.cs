using MediatR;
using Todo.Domain.Primitives;

namespace Todo.Application.Todos.GetAll;

public record GetAllTodosQuery(Guid UserId) : IRequest<Result<List<Domain.Entities.Todo>>>;