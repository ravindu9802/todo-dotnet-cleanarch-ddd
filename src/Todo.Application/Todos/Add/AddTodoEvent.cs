namespace Todo.Application.Todos.Add;

public record AddTodoEvent
{
    public Guid EventId { get; init; }
    public Guid TodoId { get; init; }
    public DateTime OccuredAtUtc { get; init; } = DateTime.UtcNow;
}