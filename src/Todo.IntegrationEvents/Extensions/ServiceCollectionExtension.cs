using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Todo.IntegrationEvents.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection IntegrationEventExtension(this IServiceCollection services,
        IConfiguration configuration)
    {
        return services;
    }
}