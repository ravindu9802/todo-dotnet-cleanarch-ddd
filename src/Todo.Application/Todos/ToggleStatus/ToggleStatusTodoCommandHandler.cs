using MediatR;
using Todo.Domain.Repositories;

namespace Todo.Application.Todos.ToggleStatus;

internal class ToggleStatusTodoCommandHandler : IRequestHandler<ToggleStatusTodoCommand, bool>
{
    private readonly ITodoRepository _todoRepository;
    private readonly ITodoUoW _uoW;

    public ToggleStatusTodoCommandHandler(ITodoUoW uoW, ITodoRepository todoRepository)
    {
        _uoW = uoW;
        _todoRepository = todoRepository;
    }

    public async Task<bool> Handle(ToggleStatusTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = await _todoRepository.GetByIdAsync(request.Id, cancellationToken);
        if (todo is null) return false;

        todo.ChangeStatus(request.IsCompleted);
        await _uoW.SaveChangesAsync(cancellationToken);
        return true;
    }
}