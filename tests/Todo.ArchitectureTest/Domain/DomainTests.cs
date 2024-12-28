using System.Reflection;
using FluentAssertions;
using NetArchTest.Rules;
using Todo.Domain.Abstractions;
using Todo.Domain.Primitives;
using Xunit;

namespace Todo.ArchitectureTests.Domain;

public class DomainTests : BaseTest
{
    [Fact]
    public void DomainEvents_Should_BeSealed()
    {
        //Arrange
        var assembly = DomainAssembly;

        //Act
        var testResult = Types
            .InAssembly(assembly)
            .That()
            .ImplementInterface(typeof(IDomainEvent))
            .Should()
            .BeSealed()
            .GetResult();

        //Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void DomainEvents_Should_HaveEventPostfix()
    {
        //Arrange
        var assembly = DomainAssembly;

        //Act
        var testResult = Types
            .InAssembly(assembly)
            .That()
            .ImplementInterface(typeof(IDomainEvent))
            .Should()
            .HaveNameEndingWith("Event")
            .GetResult();

        //Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Entities_Should_HavePrivateParameterlessConstructor() // This helps to serialize & deserialize entities for ORMs like EFCore
    {
        //Arrange
        var assembly = DomainAssembly;
        var entityTypes = Types.InAssembly(assembly)
            .That()
            .Inherit(typeof(Entity))
            .And()
            .DoNotHaveName("AggregateRoot")
            .GetTypes();

        //Act
        var failingTypes = new List<Type>();
        foreach (var type in entityTypes)
        {
            var constructors = type.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic); //BindingFlags.Instance - not static

            if (!constructors.Any(c => c.IsPrivate && c.GetParameters().Length == 0)) failingTypes.Add(type);
        }

        //Assert
        failingTypes.Should().BeEmpty();
    }

}
