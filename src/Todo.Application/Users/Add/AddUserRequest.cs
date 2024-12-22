using Todo.Domain.Enums;

namespace Todo.Application.Users.Add;

public record AddUserRequest(string FirstName, string LastName, string Email, UserRole Role);