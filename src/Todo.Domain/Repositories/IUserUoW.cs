namespace Todo.Domain.Repositories;

public interface IUserUoW
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}