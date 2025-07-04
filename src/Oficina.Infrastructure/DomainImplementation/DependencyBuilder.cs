

using Oficina.Infrastructure.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using CarePath.Infrastructure.DomainImplementation.Aggregates.UsuarioAggregates;
using Oficina.Domain.Aggregates.UsuarioAggregates;
using Oficina.Domain.Aggregates.LojaAggregates;
using Oficina.Infrastructure.DomainImplementation.Aggregates.ClienteAggregates;
using Oficina.Infrastructure.DomainImplementation.Aggregates.LojaAggregates;

namespace Oficina.Infrastructure.DomainImplementation;

public static class DependencyBuilder
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<EfUnitOfWork<ApplicationDbContext>>();
        services.AddScoped(typeof(IRepository<>), typeof(EfApplicationDbRepository<>));
        services.AddScoped<IEntityQueryBuilder, EntityQueryBuilder>();
        services.AddScoped<IUnitOfWork>(factory => factory.GetService<EfUnitOfWork<ApplicationDbContext>>()!);
        services.AddScoped<IClienteRepository, EfClienteRepository>();
        services.AddScoped<ISuperAdminRepository, EfSuperAdminRepository>();
        services.AddScoped<EfUsuarioRepository, EfUsuarioRepository>();
        services.AddScoped<ILojaRepository, EfLojaRepository>();
        services.AddScoped<IClienteRepository, EfClienteRepository>();

        return services;
    }
}