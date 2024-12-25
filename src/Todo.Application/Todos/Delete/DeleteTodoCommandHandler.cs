using Todo.Application.Abstractions.Messaging;
using Todo.Domain.Primitives;
using Todo.Domain.Repositories;

namespace Todo.Application.Todos.Delete;

internal class DeleteTodoCommandHandler : ICommandHandler<DeleteTodoCommand>
{
    private readonly ITodoRepository _todoRepository;
    private readonly ITodoUoW _uoW;

    public DeleteTodoCommandHandler(ITodoRepository todoRepository, ITodoUoW uoW)
    {
        _todoRepository = todoRepository;
        _uoW = uoW;
    }

    public async Task<Result> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
    {
        var res = await _todoRepository.DeleteAsync(request.Id, cancellationToken);

        if (!res) return Result.Failure(new Error("Todo.NotFound", $"Todo for id {request.Id} not found."));

        await _uoW.SaveChangesAsync(cancellationToken);

        return Result.Success(true);
    }
}
