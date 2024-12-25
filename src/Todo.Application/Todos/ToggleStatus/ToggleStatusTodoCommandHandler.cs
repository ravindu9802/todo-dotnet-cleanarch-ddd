using Todo.Application.Abstractions.Messaging;
using Todo.Domain.Primitives;
using Todo.Domain.Repositories;

namespace Todo.Application.Todos.ToggleStatus;

internal class ToggleStatusTodoCommandHandler : ICommandHandler<ToggleStatusTodoCommand, bool>
{
    private readonly ITodoRepository _todoRepository;
    private readonly ITodoUoW _uoW;

    public ToggleStatusTodoCommandHandler(ITodoUoW uoW, ITodoRepository todoRepository)
    {
        _uoW = uoW;
        _todoRepository = todoRepository;
    }

    public async Task<Result<bool>> Handle(ToggleStatusTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = await _todoRepository.GetByIdAsync(request.Id, cancellationToken);
        if (todo is null)
            return Result.Failure<bool>(new Error("Todo.NotFound", $"Todo for id {request.Id} not found."));

        todo.ChangeStatus(request.IsCompleted);
        await _uoW.SaveChangesAsync(cancellationToken);

        return Result.Success(true);
    }
}