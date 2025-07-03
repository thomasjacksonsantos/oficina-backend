using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Oficina.Infrastructure.DataAccess;

public interface IEntityQueryBuilder
{
    FluentQuery<TEntity> For<TEntity>() where TEntity : class;
}

public class EntityQueryBuilder(ApplicationDbContext context)
    : IEntityQueryBuilder 
{
    public FluentQuery<TEntity> For<TEntity>() 
        where TEntity : class => 
        new FluentQuery<TEntity>(context);
}

public class FluentQuery<TEntity>
    where TEntity : class
{
    private readonly ApplicationDbContext context;
    private IQueryable<TEntity> query;
    private bool asNoTracking = true;

    public FluentQuery(ApplicationDbContext context)
    {
        this.context = context;
        query = this.context
            .Set<TEntity>()
            .AsQueryable();
    }
    
    private FluentQuery(
        ApplicationDbContext context,
        IQueryable<TEntity> query) =>
        (this.context, this.query) = (context, query);

    public FluentQuery<TEntity> WithPredicate(
        Expression<Func<TEntity, bool>> predicate)
    {
        query = query.Where(predicate);
        return this;
    }

    public FluentQuery<TEntity> WithIncludes(
        Func<IQueryable<TEntity>, IQueryable<TEntity>> includes)
    {
        query = includes(query);
        return this;
    }
    
    public FluentQuery<TResponse> WithProjection<TResponse>(
        Expression<Func<TEntity, TResponse>> projection) where TResponse : class
    {
        var projectionQuery = query.Select(projection);
        return new FluentQuery<TResponse>(context, projectionQuery);
    }

    public FluentQuery<TEntity> WithTracking()
    {
        asNoTracking = false;
        return this;
    }

    public Task<TEntity?> FindFirstAsync(
        CancellationToken ct = default)
    {
        if (asNoTracking)
            query = query.AsNoTracking();
        
        return query.FirstOrDefaultAsync(ct);
    }
    
    public Task<List<TEntity>> FindAllAsync(
        CancellationToken ct = default)
    {
        if (asNoTracking)
            query = query.AsNoTracking();
        
        return query.ToListAsync(ct);
    }
    
    public Task<int> CountAsync(
        CancellationToken ct = default) =>
        query.CountAsync(ct);
    
    public Task<bool> AnyAsync(
        CancellationToken ct = default) =>
        query.AnyAsync(ct);
}