using Todo.Domain.Enums;
using Todo.Domain.Primitives;

namespace Todo.Domain.Entities;

public class User : Entity
{
    private User() { }

    private User(Guid id, string firstName, string lastName, string email, UserRole role,
        DateTime createdAtUtc) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Role = role;
        CreatedAtUtc = createdAtUtc;
    }

    public string FirstName { get; private set; }
    public string? LastName { get; private set; }
    public string Email { get; }
    public UserRole Role { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }

    public static Result<User> Create(string firstName, string lastName, string email, UserRole role)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            return Result.Failure<User>(new Error("User.NullFirstName", "First Name cannot be empty."));

        if (string.IsNullOrWhiteSpace(email))
            return Result.Failure<User>(new Error("User.NullEmail", "Email cannot be empty."));

        var user = new User(Guid.CreateVersion7(), firstName, lastName, email, role, DateTime.UtcNow);
        return Result.Success(user);
    }

    public bool Login(string email)
    {
        //TODO: Implement login logic
        return Email == email;
    }
}