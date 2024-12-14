namespace Todo.Domain.Repositories;

public interface ITodoRepository
{
    Task<Guid?> CreateAsync(Entities.Todo todo, CancellationToken cancellationToken = default);
    Task<List<Entities.Todo>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<List<Entities.Todo>> GetAllByUserAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<Entities.Todo?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ChangeTodoStatusAsync(Guid id, bool isCompleted, CancellationToken cancellationToken = default);

    //Task CreateTodoGroud();
}