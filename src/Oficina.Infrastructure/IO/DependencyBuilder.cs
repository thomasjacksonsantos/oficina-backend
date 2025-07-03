using Oficina.Infrastructure.IO.Blobs;
using Oficina.Infrastructure.IO.Templates;
using Microsoft.Extensions.DependencyInjection;

namespace Oficina.Infrastructure.IO;

public static class DependencyBuilder
{
    public static IServiceCollection AddIO(this IServiceCollection services)
    {
        services.AddTransient<IStorage, BlobStorage>();
        services.AddTransient<IEmailSend, EmailSend>();
        services.AddTransient<IBuildTemplate, BuildTemplate>();
        return services;
    }
}