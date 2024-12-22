using MediatR;
using Todo.Domain.Primitives;
using Todo.Domain.Repositories;

namespace Todo.Application.Todos.DeleteAll;

internal class DeleteAllTodosCommandHandler : IRequestHandler<DeleteAllTodosCommand, Result>
{
    private readonly ITodoRepository _todoRepository;
    private readonly ITodoUoW _uoW;

    public DeleteAllTodosCommandHandler(ITodoUoW uoW, ITodoRepository todoRepository)
    {
        _uoW = uoW;
        _todoRepository = todoRepository;
    }

    public async Task<Result> Handle(DeleteAllTodosCommand request, CancellationToken cancellationToken)
    {
        var res = await _todoRepository.DeleteAllByUserAsync(request.UserId, cancellationToken);

        if (!res) return Result.Failure(new Error("Todo.NotFound", $"Todos for user id {request.UserId} not found."));

        await _uoW.SaveChangesAsync(cancellationToken);

        return Result.Success(true);
    }
}
