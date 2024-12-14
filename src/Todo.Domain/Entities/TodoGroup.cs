using Todo.Domain.Primitives;

namespace Todo.Domain.Entities;

public class TodoGroup : AggregateRoot
{
    private TodoGroup(Guid id) : base(id) { }
    private TodoGroup(Guid id, string groupTitle, List<Todo> todos, DateTime createdAtUtc) : base(id)
    {
        GroupTitle = groupTitle;
        Todos = todos;
        CreateAtUtc = createdAtUtc;
    }

    public string GroupTitle { get; private set; }
    public List<Todo> Todos { get; private set; }
    public DateTime CreateAtUtc { get; private set; }

    public static TodoGroup Create(string groupTitle, List<Todo> todos)
    {
        if (string.IsNullOrWhiteSpace(groupTitle)) throw new ArgumentException("Group Title cannot be empty.", nameof(groupTitle));
        if (todos.Any()) throw new ArgumentException("At least one Todo required.", nameof(todos));

        return new TodoGroup(Guid.CreateVersion7(), groupTitle, todos, DateTime.UtcNow);
    }

}
