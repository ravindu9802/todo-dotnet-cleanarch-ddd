using Todo.Domain.Entities;
using Todo.Domain.Repositories;
using Todo.Infrastructure.Persistence;

namespace Todo.Infrastructure.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly UserDbContext _context;

    public UserRepository(UserDbContext context)
    {
        _context = context;
    }

    public async Task<Guid?> CreateUserAsync(User user, CancellationToken cancellationToken = default)
    {
        await _context.Users.AddAsync(user, cancellationToken);
        return user.Id;
    }
}