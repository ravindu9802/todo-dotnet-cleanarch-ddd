using Todo.Domain.Primitives;

namespace Todo.Domain.Entities;

public class User : Entity
{
    private User(Guid id) : base(id)
    {
    }

    private User(Guid id, string firstName, string lastName, DateTime createdAtUtc) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        CreatedAtUtc = createdAtUtc;
    }

    public string FirstName { get; private set; }
    public string? LastName { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }

    public static User Create(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First Name cannot be empty.", nameof(firstName));

        return new User(Guid.CreateVersion7(), firstName, lastName, DateTime.UtcNow);
    }
}