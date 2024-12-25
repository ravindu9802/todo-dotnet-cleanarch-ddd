using Todo.Domain.Entities;

namespace Todo.Domain.Abstractions;

public interface IJwtProvider
{
    string Generate(User user);
}