using MediatR;
using Todo.Domain.Repositories;

namespace Todo.Application.Todos.Add;

internal class AddTodoCommandHandler : IRequestHandler<AddTodoCommand, Guid?>
{
    private readonly ITodoRepository _repository;
    private readonly ITodoUoW _uoW;

    public AddTodoCommandHandler(ITodoUoW uoW, ITodoRepository repository)
    {
        _uoW = uoW;
        _repository = repository;
    }

    public async Task<Guid?> Handle(AddTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = Domain.Entities.Todo.Create(request.Title, request.Description, request.UserId);
        await _repository.CreateAsync(todo, cancellationToken);
        await _uoW.SaveChangesAsync(cancellationToken);
        return todo.Id;
    }
}