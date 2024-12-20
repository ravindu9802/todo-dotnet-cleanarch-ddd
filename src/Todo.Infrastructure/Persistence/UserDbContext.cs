using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Todo.Domain.Entities;

namespace Todo.Infrastructure.Persistence;

public class UserDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public UserDbContext(DbContextOptions<UserDbContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(_configuration.GetSection("Schema:UserSchema").Value);
    }
}