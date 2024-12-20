using MediatR;
using Todo.Domain.Repositories;

namespace Todo.Application.Todos.GetAll;

internal class GetAllTodosQueryHandler : IRequestHandler<GetAllTodosQuery, List<Domain.Entities.Todo>>
{
    private readonly ITodoRepository _todoRepository;

    public GetAllTodosQueryHandler(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public async Task<List<Domain.Entities.Todo>> Handle(GetAllTodosQuery request, CancellationToken cancellationToken)
    {
        return await _todoRepository.GetAllByUserAsync(request.UserId, cancellationToken);
    }
}