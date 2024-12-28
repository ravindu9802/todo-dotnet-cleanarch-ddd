using System.Reflection;
using Todo.Application.Abstractions.Messaging;
using Todo.Domain.Entities;
using Todo.Infrastructure.Persistence;

namespace Todo.ArchitectureTests;

public abstract class BaseTest
{
    protected static readonly Assembly DomainAssembly = typeof(User).Assembly;
    protected static readonly Assembly ApplicationAssembly = typeof(ICommand).Assembly;
    protected static readonly Assembly InfrastructureAssembly = typeof(UserDbContext).Assembly;
    protected static readonly Assembly ApiAssembly = typeof(Api.AssemblyReference).Assembly;
}
