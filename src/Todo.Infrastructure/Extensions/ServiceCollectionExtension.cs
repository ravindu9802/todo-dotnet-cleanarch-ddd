using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Todo.Domain.Repositories;
using Todo.Infrastructure.Persistence;
using Todo.Infrastructure.Repositories;

namespace Todo.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection InfrastructureLayerExtension(this IServiceCollection services,
        IConfiguration configuration)
    {
        var dbConnectionString = configuration.GetConnectionString("TodoDb")!;
        var todoSchema = configuration.GetSection("Schema:TodoSchema").Value!;
        var userSchema = configuration.GetSection("Schema:UserSchema").Value!;

        services.AddNpgsql<TodoDbContext>(dbConnectionString,
            options => { options.MigrationsHistoryTable(HistoryRepository.DefaultTableName, todoSchema); });

        services.AddNpgsql<UserDbContext>(dbConnectionString,
            options => { options.MigrationsHistoryTable(HistoryRepository.DefaultTableName, userSchema); });

        services.AddScoped<ITodoRepository, TodoRepository>();
        services.AddScoped<ITodoUoW, TodoUoW>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserUoW, UserUoW>();

        return services;
    }
}