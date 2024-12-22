using MassTransit;
using MediatR;
using Todo.Domain.Primitives;
using Todo.Domain.Repositories;

namespace Todo.Application.Todos.Add;

internal class AddTodoCommandHandler : IRequestHandler<AddTodoCommand, Result<Guid>>
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

    public async Task<Result<Guid>> Handle(AddTodoCommand request, CancellationToken cancellationToken)
    {
        var result = Domain.Entities.Todo.Create(request.Title, request.Description, request.UserId);

        if (result.IsFailure) return Result.Failure<Guid>(result.Error);

        await _repository.CreateAsync(result.Value, cancellationToken);
        await _uoW.SaveChangesAsync(cancellationToken);
        await _publishEndpoint.Publish(new AddTodoEvent() { EventId = Guid.CreateVersion7(), TodoId = result.Value.Id, OccuredAtUtc = DateTime.UtcNow });

        return Result.Success(result.Value.Id);
    }
}