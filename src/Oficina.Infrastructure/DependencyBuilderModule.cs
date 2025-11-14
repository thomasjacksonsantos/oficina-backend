using Oficina.Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Oficina.Infrastructure.DomainImplementation;
using Oficina.Infrastructure.IO;
using Oficina.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Oficina.Infrastructure;

public static class DependencyBuilderModule
{
    public static IServiceCollection AddInfrastructureModule
    (
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext(configuration);
        services.AddRepositories();
        services.AddClients(configuration);
        services.AddConfigurations(configuration);
        services.AddIO();
        return services;
    }

    private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("Database")));


        return services;
    }

    private static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ApiConfig>(configuration.GetSection("AppSettings"));
        services.AddSingleton(c => c.GetService<IOptions<ApiConfig>>()!.Value);

        return services;
    }

    private static IServiceCollection AddClients(this IServiceCollection services, IConfiguration configuration)
    {
        var apiConfig = new ApiConfig();
        configuration.GetSection("AppSettings").Bind(apiConfig);

        services.AddHttpClient(apiConfig.Cep.ServiceName, c => c.BaseAddress = new Uri(
            apiConfig.Cep.Url
        ));

        services.AddHttpClient(apiConfig.Authentication.ServiceName, c => c.BaseAddress = new Uri(
            apiConfig.Authentication.Host
        ));

        return services;
    }
}