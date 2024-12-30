using Microsoft.Extensions.Caching.Hybrid;
using Todo.Domain.Repositories;

namespace Todo.Infrastructure.Repositories;

internal class CachedTodoRepository : ITodoRepository
{
    private readonly HybridCache _cache;
    private readonly TodoRepository _decoratedTodoRepository;

    public CachedTodoRepository(TodoRepository decoratedTodoRepository, HybridCache cache)
    {
        _decoratedTodoRepository = decoratedTodoRepository;
        _cache = cache;
    }

    public async Task<Guid?> CreateAsync(Domain.Entities.Todo todo, CancellationToken cancellationToken = default)
    {
        await _cache.RemoveByTagAsync($"todosOf{todo.UserId}", cancellationToken);
        return await _decoratedTodoRepository.CreateAsync(todo, cancellationToken);
    }

    public async Task<List<Domain.Entities.Todo>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _decoratedTodoRepository.GetAllAsync(cancellationToken);
    }

    public async Task<List<Domain.Entities.Todo>> GetAllByUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var cacheKey = $"todos-{userId}";

        return await _cache.GetOrCreateAsync(cacheKey,
           async token => await _decoratedTodoRepository.GetAllByUserAsync(userId, token),
           tags: [$"todosOf{userId}"],
           cancellationToken: cancellationToken);
    }

    public async Task<Domain.Entities.Todo?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _decoratedTodoRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<bool> ChangeTodoStatusAsync(Guid id, bool isCompleted, CancellationToken cancellationToken = default)
    {
        return await _decoratedTodoRepository.ChangeTodoStatusAsync(id, isCompleted, cancellationToken);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _decoratedTodoRepository.DeleteAsync(id, cancellationToken);
    }

    public async Task<bool> DeleteAllByUserAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _decoratedTodoRepository.DeleteAllByUserAsync(userId, cancellationToken);
    }
}
