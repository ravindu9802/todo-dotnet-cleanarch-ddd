using Todo.Domain.Repositories;
using Todo.Infrastructure.Persistence;

namespace Todo.Infrastructure.Repositories;

internal class UserUoW : IUserUoW
{
    private readonly UserDbContext _context;

    public UserUoW(UserDbContext context)
    {
        _context = context;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}