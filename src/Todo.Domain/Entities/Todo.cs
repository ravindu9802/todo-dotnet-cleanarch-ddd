using Todo.Domain.Primitives;

namespace Todo.Domain.Entities;

public class Todo : Entity
{
    private Todo(Guid id) : base(id)
    {
    }

    private Todo(Guid id, string title, string description, bool isCompleted, Guid userId,
        DateTime createdAtUtc) : base(id)
    {
        Title = title;
        Description = description;
        IsCompleted = isCompleted;
        UserId = userId;
        CreatedAtUtc = createdAtUtc;
    }

    public string Title { get; private set; }
    public string? Description { get; private set; }
    public bool IsCompleted { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }

    public static Result<Todo> Create(string title, string description, Guid userId)
    {
        if (string.IsNullOrWhiteSpace(title))
            return Result.Failure<Todo>(new Error("Todo.EmptyTitle", "Title cannot be empty."));

        var todo = new Todo(Guid.CreateVersion7(), title, description, false, userId, DateTime.UtcNow);
        return Result.Success(todo);
    }

    public void ChangeStatus(bool isCompleted)
    {
        IsCompleted = isCompleted;
    }
}