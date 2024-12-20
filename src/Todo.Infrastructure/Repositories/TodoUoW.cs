using Todo.Domain.Repositories;
using Todo.Infrastructure.Persistence;

namespace Todo.Infrastructure.Repositories;

internal class TodoUoW : ITodoUoW
{
    private readonly TodoDbContext _context;

    public TodoUoW(TodoDbContext context)
    {
        _context = context;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}