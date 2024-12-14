using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Todo.Application.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection ApplicationLayerExtension(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(typeof(ServiceCollectionExtension).Assembly);
        });

        return services;
    }
}