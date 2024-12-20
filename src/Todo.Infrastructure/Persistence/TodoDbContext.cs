using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Todo.Domain.Entities;

namespace Todo.Infrastructure.Persistence;

public class TodoDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public TodoDbContext(DbContextOptions<TodoDbContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    public DbSet<Domain.Entities.Todo> Todos { get; set; }
    public DbSet<TodoGroup> TodoGroups { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(_configuration.GetSection("Schema:TodoSchema").Value);
    }
}