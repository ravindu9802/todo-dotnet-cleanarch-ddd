using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Todo.Domain.Abstractions;
using Todo.Domain.Repositories;
using Todo.Infrastructure.Authentication;
using Todo.Infrastructure.Persistence;
using Todo.Infrastructure.Repositories;

namespace Todo.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection InfrastructureLayerExtension(this IServiceCollection services,
        IConfiguration configuration)
    {
        var dbConnectionString = configuration.GetConnectionString("TodoDb")!;
        var redisCacheConnectionString = configuration.GetConnectionString("Redis")!;
        var todoSchema = configuration.GetSection("Schema:TodoSchema").Value!;
        var userSchema = configuration.GetSection("Schema:UserSchema").Value!;

        services.AddNpgsql<TodoDbContext>(dbConnectionString,
            options => { options.MigrationsHistoryTable(HistoryRepository.DefaultTableName, todoSchema); });

        services.AddNpgsql<UserDbContext>(dbConnectionString,
            options => { options.MigrationsHistoryTable(HistoryRepository.DefaultTableName, userSchema); });

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisCacheConnectionString;
        });

        services.AddSingleton(typeof(IHybridCacheSerializer<>), typeof(NewtonsoftHybridCacheSerializer<>));

#pragma warning disable EXTEXP0018
        services.AddHybridCache(options =>
        {
            options.DefaultEntryOptions = new HybridCacheEntryOptions()
            {
                LocalCacheExpiration = TimeSpan.FromMinutes(2),
                Expiration = TimeSpan.FromMinutes(5)
            };
        });
#pragma warning restore EXTEXP0018

        services.AddScoped<TodoRepository>();
        services.AddScoped<ITodoRepository, CachedTodoRepository>();
        services.AddScoped<ITodoUoW, TodoUoW>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserUoW, UserUoW>();
        services.AddScoped<IJwtProvider, JwtProvider>();

        return services;
    }
}