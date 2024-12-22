using Todo.Domain.Entities;

namespace Todo.Domain.Repositories;

public interface IUserRepository
{
    Task<Guid?> CreateUserAsync(User user, CancellationToken cancellationToken = default);
    Task<User?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);
}