namespace Todo.Domain.Repositories;

public interface ITodoUoW
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}