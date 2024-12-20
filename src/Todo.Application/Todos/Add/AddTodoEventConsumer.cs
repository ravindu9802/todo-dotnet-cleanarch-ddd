using MassTransit;
using Microsoft.Extensions.Logging;

namespace Todo.Application.Todos.Add;

public class AddTodoEventConsumer : IConsumer<AddTodoEvent>
{
    private readonly ILogger<AddTodoEventConsumer> _logger;

    public AddTodoEventConsumer(ILogger<AddTodoEventConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<AddTodoEvent> context)
    {
        _logger.LogInformation("Event occured 2. {Event}, {EventId}, {OccuredAtUtc}, {ConsumedAtUtc}",
            nameof(AddTodoEvent),
            context.Message.EventId,
            context.Message.OccuredAtUtc,
            DateTime.UtcNow);

        return Task.CompletedTask;
    }
}