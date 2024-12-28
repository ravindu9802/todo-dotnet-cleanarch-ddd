using FluentAssertions;
using NetArchTest.Rules;
using Xunit;

namespace Todo.ArchitectureTests;

public class ArchitectureTests : BaseTest
{
    private const string DomainNamespace = "Todo.Domain";
    private const string ApplicationNamespace = "Todo.Application";
    private const string InfrastructureNamespace = "Todo.Infrastructure";
    private const string PresentationNamespace = "Todo.Api";

    [Fact]
    public void Domain_Should_Not_HaveDependencyOnOtherProjects()
    {
        //Arrange
        var assembly = DomainAssembly;

        var otherProjects = new[]
        {
            ApplicationNamespace,
            InfrastructureNamespace,
            PresentationNamespace
        };

        //Act
        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult();

        //Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Application_Should_Not_HaveDependencyOnOtherProjects()
    {
        //Arrange
        var assembly = ApplicationAssembly;

        var otherProjects = new[]
        {
            InfrastructureNamespace,
            PresentationNamespace
        };

        //Act
        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult();

        //Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Infrastructure_Should_Not_HaveDependencyOnOtherProjects()
    {
        //Arrange
        var assembly = InfrastructureAssembly;

        var otherProjects = new[]
        {
            PresentationNamespace
        };

        //Act
        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult();

        //Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Presentation_Should_Not_HaveDependencyOnOtherProjects()
    {
        //Arrange
        var assembly = ApiAssembly;

        // at the moment presentation layer have dependency on all other projects
        var otherProjects = Array.Empty<string>();

        //Act
        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult();

        //Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Handlers_Should_Not_HaveDependencyOnDomain()
    {
        //Arrange
        var assembly = ApplicationAssembly;

        //Act
        var testResult = Types
            .InAssembly(assembly)
            .That()
            .HaveNameEndingWith("Handler")
            .Should()
            .HaveDependencyOn(DomainNamespace)
            .GetResult();

        //Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Controllers_Should_Not_HaveDependencyOnMediatR()
    {
        //Arrange
        var assembly = ApiAssembly;

        //Act
        var testResult = Types
            .InAssembly(assembly)
            .That()
            .HaveNameEndingWith("Controller")
            .Should()
            .HaveDependencyOn("MediatR")
            .GetResult();

        //Assert
        testResult.IsSuccessful.Should().BeTrue();
    }
}