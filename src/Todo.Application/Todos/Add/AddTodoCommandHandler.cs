using MassTransit;
using MediatR;
using Todo.Domain.Repositories;

namespace Todo.Application.Todos.Add;

internal class AddTodoCommandHandler : IRequestHandler<AddTodoCommand, Guid?>
{
    private readonly ITodoRepository _repository;
    private readonly ITodoUoW _uoW;
    private readonly IPublishEndpoint _publishEndpoint;

    public AddTodoCommandHandler(ITodoUoW uoW, ITodoRepository repository, IPublishEndpoint publishEndpoint)
    {
        _uoW = uoW;
        _repository = repository;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<Guid?> Handle(AddTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = Domain.Entities.Todo.Create(request.Title, request.Description, request.UserId);
        await _repository.CreateAsync(todo, cancellationToken);
        await _uoW.SaveChangesAsync(cancellationToken);
        await _publishEndpoint.Publish(new AddTodoEvent() { EventId = Guid.CreateVersion7(), TodoId = todo.Id, OccuredAtUtc = DateTime.UtcNow });
        return todo.Id;
    }
}