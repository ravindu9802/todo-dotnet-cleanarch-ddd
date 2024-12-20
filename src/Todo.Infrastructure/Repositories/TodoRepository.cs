using Microsoft.EntityFrameworkCore;
using Todo.Domain.Repositories;
using Todo.Infrastructure.Persistence;

namespace Todo.Infrastructure.Repositories;

internal class TodoRepository : ITodoRepository
{
    private readonly TodoDbContext _context;

    public TodoRepository(TodoDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ChangeTodoStatusAsync(Guid id, bool isCompleted,
        CancellationToken cancellationToken = default)
    {
        var todo = await _context.Todos
            .FindAsync(id, cancellationToken);
        if (todo is null) return false;

        todo.ChangeStatus(isCompleted);
        return true;
    }

    public async Task<Guid?> CreateAsync(Domain.Entities.Todo todo, CancellationToken cancellationToken = default)
    {
        await _context.Todos
            .AddAsync(todo, cancellationToken);
        return todo.Id;
    }

    public async Task<List<Domain.Entities.Todo>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Todos
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Domain.Entities.Todo>> GetAllByUserAsync(Guid userId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Todos
            .AsNoTracking()
            .Where(t => t.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<Domain.Entities.Todo?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Todos
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }
}