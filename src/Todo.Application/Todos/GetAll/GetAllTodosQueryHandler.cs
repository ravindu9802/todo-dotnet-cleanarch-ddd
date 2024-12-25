using Todo.Application.Abstractions.Messaging;
using Todo.Domain.Primitives;
using Todo.Domain.Repositories;

namespace Todo.Application.Todos.GetAll;

internal class GetAllTodosQueryHandler : IQueryHandler<GetAllTodosQuery, List<Domain.Entities.Todo>>
{
    private readonly ITodoRepository _todoRepository;

    public GetAllTodosQueryHandler(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public async Task<Result<List<Domain.Entities.Todo>>> Handle(GetAllTodosQuery request, CancellationToken cancellationToken)
    {
        var results = await _todoRepository.GetAllByUserAsync(request.UserId, cancellationToken);
        return Result.Success(results);
    }
}