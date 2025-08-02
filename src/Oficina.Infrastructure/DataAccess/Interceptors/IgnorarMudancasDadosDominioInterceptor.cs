
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Oficina.Domain.Enumerations;

namespace Oficina.Infrastructure.DataAccess.Interceptors;

public class IgnorarMudancasDadosDominioInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new())
    {
        var entries = eventData!.Context!.ChangeTracker.Entries<DadoDominio>();
        
        foreach (var entry in entries)
        {
            if (entry.State is EntityState.Added or EntityState.Modified)
            {
                entry.State = EntityState.Unchanged;
            }
        }
        
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}