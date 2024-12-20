using Microsoft.EntityFrameworkCore;
using Todo.Infrastructure.Persistence;

namespace Todo.Api.Migrations;

public static class MigrationExtension
{
    public static void ApplyMigrations(this IApplicationBuilder applicationBuilder)
    {
        using IServiceScope scope = applicationBuilder.ApplicationServices.CreateAsyncScope();

        using TodoDbContext todoDbContext = scope.ServiceProvider.GetService<TodoDbContext>()!;
        todoDbContext.Database.Migrate();

        using UserDbContext userDbContext = scope.ServiceProvider.GetService<UserDbContext>()!;
        userDbContext.Database.Migrate();
    }
}