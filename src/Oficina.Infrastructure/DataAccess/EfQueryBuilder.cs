using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Oficina.Domain.ValueObjects;

namespace Oficina.Infrastructure.DataAccess;
public interface IFluentQuery
{
    EfFluentEntityQuery<TEntity> For<TEntity>() where TEntity : class;
}

public class FluentQuery(ApplicationDbContext context)
    : IFluentQuery
{
    public EfFluentEntityQuery<TEntity> For<TEntity>()
        where TEntity : class =>
        new(context);
}

public class EfFluentEntityQuery<TEntity>
    where TEntity : class
{
    private readonly ApplicationDbContext _context;
    private IQueryable<TEntity> _query;
    private bool _asNoTracking = true;
    private bool _ignoreQueryFilters;

    public EfFluentEntityQuery(ApplicationDbContext context)
    {
        _context = context;
        _query = _context
            .Set<TEntity>()
            .AsQueryable();
    }

    private EfFluentEntityQuery(
        ApplicationDbContext context,
        IQueryable<TEntity> query) =>
        (_context, _query) = (context, query);

    public EfFluentEntityQuery<TEntity> WithPredicate(
        Expression<Func<TEntity, bool>> predicate)
    {
        _query = _query.Where(predicate);
        return this;
    }

    public EfFluentEntityQuery<TEntity> WithIncludes(
        Func<IQueryable<TEntity>, IQueryable<TEntity>> includes)
    {
        _query = includes(_query);
        return this;
    }

    public EfFluentEntityQuery<TResponse> WithProjection<TResponse>(
        Expression<Func<TEntity, TResponse>> projection) where TResponse : class
    {
        var projectionQuery = _query.Select(projection);
        return new EfFluentEntityQuery<TResponse>(_context, projectionQuery);
    }

    public EfFluentEntityQuery<TEntity> WithOrdering(
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
    {
        _query = orderBy(_query);
        return this;
    }

    public EfFluentEntityQuery<TEntity> WithTracking()
    {
        _asNoTracking = false;
        return this;
    }

    public EfFluentEntityQuery<TEntity> IgnoreQueryFilters()
    {
        _ignoreQueryFilters = true;
        return this;
    }

    public Task<TEntity?> FindFirstAsync(
        CancellationToken ct = default)
    {
        if (_asNoTracking)
            _query = _query.AsNoTracking();

        if (_ignoreQueryFilters)
            _query = _query.IgnoreQueryFilters();

        return _query.FirstOrDefaultAsync(ct);
    }

    public Task<List<TEntity>> FindAllAsync(
        CancellationToken ct = default)
    {
        if (_asNoTracking)
            _query = _query.AsNoTracking();

        if (_ignoreQueryFilters)
            _query = _query.IgnoreQueryFilters();

        return _query.ToListAsync(ct);
    }

    public async Task<PagedResult<TEntity>> FindAllPagedAsync(
        int? pagina,
        int? total = null,
        CancellationToken ct = default)
    {
        if (_asNoTracking)
            _query = _query.AsNoTracking();

        if (_ignoreQueryFilters)
            _query = _query.IgnoreQueryFilters();

        var skip = 0;
        var take = 20;

        if (total is > 0)
            take = total.Value;

        if (pagina is > 0)
            skip = (pagina.Value - 1) * take;

        var totalTask = await _query.CountAsync(ct);

        var dadosTask = await _query
            .Skip(skip)
            .Take(take)
            .ToListAsync(ct);

        return new PagedResult<TEntity>
        {
            PaginaAtual = skip == 0 ? 1 : skip,
            Limite = take,
            TotalRegistros = totalTask,
            Dados = dadosTask
        };

    }

    public Task<int> CountAsync(
        CancellationToken ct = default) =>
        _query.CountAsync(ct);

    public Task<bool> AnyAsync(
        CancellationToken ct = default)
    {
        if (_ignoreQueryFilters)
            _query = _query.IgnoreQueryFilters();

        return _query.AnyAsync(ct);
    }
}