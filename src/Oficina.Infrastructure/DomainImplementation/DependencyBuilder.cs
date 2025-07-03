

using Oficina.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Oficina.Domain.Aggregates.ContaAggregates;
using Oficina.Infrastructure.DomainImplementation.Aggregates.PdfAggregates;

namespace Oficina.Infrastructure.DomainImplementation;

public static class DependencyBuilder
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<EfUnitOfWork<ApplicationDbContext>>();
        services.AddScoped(typeof(IRepository<>), typeof(EfApplicationDbRepository<>));
        services.AddScoped<IEntityQueryBuilder, EntityQueryBuilder>();
        services.AddScoped<IUnitOfWork>(factory => factory.GetService<EfUnitOfWork<ApplicationDbContext>>()!);
        services.AddScoped<IPdfRepository, EfPdfRepository>();
        services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, CustomUserClaimsPrincipalFactory>();

        return services;
    }
}