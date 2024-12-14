using Todo.Domain.Entities;

namespace Todo.Domain.Repositories;

public interface IUserRepository
{
    Task<Guid?> CreateUserAsync(User user, CancellationToken cancellationToken = default);
}