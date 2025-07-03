using System;
using System.Transactions;
using Microsoft.EntityFrameworkCore;

namespace Oficina.Infrastructure.DataAccess;

public interface IUnitOfWork
{
    IUnitOfWorkTransaction BeginTransaction();
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}

public interface IUnitOfWorkTransaction : IDisposable
{
    TransactionScope Scope { get; }
    void Commit();
}

public class EfUnitOfWork<TDbContext> : IUnitOfWork
    where TDbContext : DbContext
{
    public TDbContext CurrentContext { get; }

    public EfUnitOfWork(TDbContext context)
        => CurrentContext = context ?? throw new ArgumentNullException(nameof(context));

    public IUnitOfWorkTransaction BeginTransaction() =>
        new EfUnitOfWorkTransaction();

    public Task<int> SaveChangesAsync(CancellationToken ct = default) =>
        CurrentContext.SaveChangesAsync(ct);
}

public class EfUnitOfWorkTransaction : IUnitOfWorkTransaction
{
    public TransactionScope Scope { get; set; }

    public EfUnitOfWorkTransaction()
    {
        Scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
    }

    public void Commit() =>
        Scope.Complete();

    public void Dispose() =>
        Scope.Dispose();
}