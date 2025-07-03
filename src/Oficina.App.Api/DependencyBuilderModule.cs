using Oficina.App.Api.Shared;
using Microsoft.Extensions.DependencyInjection;
using Oficina.Infrastructure.Core;

namespace Oficina.App.Api;

public static class DependencyBuilderModule
{
    public static IServiceCollection AddApiModule(
        this IServiceCollection services
    )
    {
        services.AddUseCases();
        services.AddAuth();
        
        return services;
    }

    private static IServiceCollection AddAuth(this IServiceCollection services)
    {
        services.AddTransient<IAuthProvider, HttpAuthProvider>();
        
        return services;
    }

    private static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        var useCaseType = typeof(IUseCase<,>);
        var assembly = AssemblyInfo.GetApiAssembly();
        
        var types = assembly.GetTypes()
            .Where(t => t is { IsAbstract: false, IsInterface: false })
            .SelectMany(t => t.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == useCaseType)
                .Select(i => new { Interface = i, Implementation = t }))
            .ToList();

        foreach (var type in types)
        {
            services.AddTransient(type.Interface, type.Implementation);
        }

        return services;
    }
}