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

    public static Result<User> Create(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            return Result.Failure<User>(new Error("User.FirstNameNull", "First Name cannot be empty."));

        User user = new User(Guid.CreateVersion7(), firstName, lastName, DateTime.UtcNow);
        return Result.Success(user);
    }
}