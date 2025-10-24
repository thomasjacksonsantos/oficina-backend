using System.Linq.Expressions;
using Oficina.Domain.ValueObjects;
using Oficina.Infrastructure.Core;
using Microsoft.EntityFrameworkCore;

namespace Oficina.Infrastructure.DataAccess;

public interface IRepository<TEntity>
    where TEntity : class
{
    #region Find by Id

    ValueTask<TEntity> FindAsync(object key, CancellationToken ct = default);
    ValueTask<TEntity> FindBySqidsIdAsync(string key, CancellationToken ct = default);

    #endregion

    #region Find First

    Task<TEntity?> FindFirstByPredicate(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default);
    Task<TResponse?> FindFirstByPredicate<TResponse>(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TResponse>> projection,
        CancellationToken ct = default
    );
    Task<TResponse?> FindFirstByPredicate<TResponse>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResponse>> projection, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes, CancellationToken ct = default);
    Task<TEntity?> FindFirstByPredicate(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes, bool? asNoTracking = null, CancellationToken ct = default);

    #endregion

    #region Find All

    Task<List<TEntity>> GetAll(CancellationToken ct = default);
    Task<List<TEntity>> FindAllByPredicate(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default);
    Task<List<TEntity>> FindAllByPredicate(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes, CancellationToken ct = default);
    Task<List<TResponse>> FindAllByPredicate<TResponse>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResponse>> projection, CancellationToken ct = default);

    #endregion

    #region Pagination

    Task<PagedResult<TEntity>> FindAllByQueryable(IQueryable<TEntity> queryable, Pagination pagination, CancellationToken ct = default);
    Task<PagedResult<TEntity>> FindAllByPredicate(Expression<Func<TEntity, bool>> predicate, Pagination pagination, CancellationToken ct = default);
    Task<PagedResult<TEntity>> FindAllByPredicate(Expression<Func<TEntity, bool>> predicate, Pagination pagination, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes, CancellationToken ct = default);
    Task<PagedResult<TResponse>> FindAllByPredicate<TResponse>(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TResponse>> projection,
        Pagination pagination,
        CancellationToken ct = default
    );

    #endregion

    #region Others

    Task<int> Count(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default);
    Task<bool> Any(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default);

    #endregion

    #region CRUD

    IRepository<TEntity> Add(TEntity entity);
    ValueTask<IRepository<TEntity>> AddAsync(TEntity entity);
    ValueTask MarkAs(TEntity entity, EntityState entityState);

    #endregion
}

public class EfRepository<TDbContext, TEntity>(EfUnitOfWork<TDbContext> unitOfWork)
    : IRepository<TEntity>
    where TDbContext : DbContext
    where TEntity : class
{
    private TDbContext CurrentContext { get; } = unitOfWork.CurrentContext;

    #region Methods

    protected DbSet<TEntity> Queryable()
        => CurrentContext.Set<TEntity>();

    #endregion

    #region Find by Id

    public ValueTask<TEntity> FindAsync(
        object key,
        CancellationToken ct = default) =>
        Queryable()
            .FindAsync([key], ct)!;

    public ValueTask<TEntity> FindBySqidsIdAsync(
        string key,
        CancellationToken ct = default) =>
        Queryable()
            .FindAsync([key.DecodeWithSqids()], ct)!;

    #endregion

    #region Find First

    public Task<TEntity?> FindFirstByPredicate(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken ct = default) =>
        Queryable()
            .FirstOrDefaultAsync(predicate, ct);

    public Task<TResponse?> FindFirstByPredicate<TResponse>(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TResponse>> projection,
        CancellationToken ct = default) =>
            Queryable()
                .Where(predicate)
                .Select(projection)
                .FirstOrDefaultAsync(ct);

    public Task<TResponse?> FindFirstByPredicate<TResponse>(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TResponse>> projection,
        Func<IQueryable<TEntity>, IQueryable<TEntity>> includes,
        CancellationToken ct = default) =>
            QueryableWithIncludes(includes)
                .Where(predicate)
                .Select(projection)
                .FirstOrDefaultAsync(ct);

    public Task<TEntity?> FindFirstByPredicate(
        Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IQueryable<TEntity>> includes,
        bool? asNoTracking = null,
        CancellationToken ct = default) =>
        QueryableWithIncludes(includes, asNoTracking: asNoTracking)
            .FirstOrDefaultAsync(predicate, ct);

    #endregion

    #region Find All

    public Task<List<TEntity>> GetAll(
        CancellationToken ct = default) =>
        Queryable()
            .ToListAsync(ct);

    public Task<List<TEntity>> FindAllByPredicate(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken ct = default) =>
        Queryable()
            .Where(predicate)
            .ToListAsync(ct);

    public Task<List<TEntity>> FindAllByPredicate(
        Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IQueryable<TEntity>> includes,
        CancellationToken ct = default) =>
        QueryableWithIncludes(includes)
            .Where(predicate)
            .ToListAsync(ct);

    public Task<List<TResponse>> FindAllByPredicate<TResponse>(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TResponse>> projection,
        CancellationToken ct = default) =>
        Queryable()
            .AsSplitQuery()
            .Where(predicate)
            .Select(projection)
            .ToListAsync(ct);

    #endregion

    #region Pagination

    public async Task<PagedResult<TEntity>> FindAllByQueryable(
        IQueryable<TEntity> queryable,
        Pagination pagination,
        CancellationToken ct = default)
    {
        var totalRegistros = await queryable.CountAsync(ct);

        var pagedResult = await queryable
            .Skip((pagination.PaginaAtual - 1) * pagination.Limite)
            .Take(pagination.Limite)
            .ToListAsync(ct);

        return new PagedResult<TEntity>
        {
            TotalRegistros = totalRegistros,
            PaginaAtual = pagination.PaginaAtual,
            Limite = pagination.Limite,
            Dados = pagedResult.AsEnumerable()
        };
    }

    public Task<PagedResult<TEntity>> FindAllByPredicate(
        Expression<Func<TEntity, bool>> predicate,
        Pagination pagination,
        CancellationToken ct = default)
    {
        var queryable = Queryable().Where(predicate);
        return FindAllByQueryable(queryable, pagination, ct);
    }

    public async Task<PagedResult<TResponse>> FindAllByPredicate<TResponse>(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TResponse>> projection,
        Pagination pagination,
        CancellationToken ct = default
    )
    {
        var queryable = Queryable().Where(predicate).Select(projection);
        var totalRegistros = queryable.Count();

        var dados = await queryable
            .Skip((pagination.PaginaAtual - 1) * pagination.Limite)
            .Take(pagination.Limite)
            .ToListAsync();

        return new PagedResult<TResponse>
        {
            TotalRegistros = totalRegistros,
            PaginaAtual = pagination.PaginaAtual,
            Limite = pagination.Limite,
            Dados = dados
        };
    }

    public Task<PagedResult<TEntity>> FindAllByPredicate(
        Expression<Func<TEntity, bool>> predicate,
        Pagination pagination,
        Func<IQueryable<TEntity>, IQueryable<TEntity>> includes,
        CancellationToken ct = default)
    {
        var queryable = QueryableWithIncludes(includes).Where(predicate);
        return FindAllByQueryable(queryable, pagination, ct);
    }

    #endregion

    #region Others

    public Task<int> Count(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken ct = default) =>
        Queryable()
            .Where(predicate)
            .CountAsync(ct);

    public Task<bool> Any(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken ct = default) =>
        Queryable()
            .Where(predicate)
            .AnyAsync(ct);


    #endregion

    #region CRUD

    public IRepository<TEntity> Add(TEntity entity)
    {
        CurrentContext.Add(entity);
        return this;
    }

    public ValueTask<IRepository<TEntity>> AddAsync(TEntity entity)
    {
        CurrentContext.AddAsync(entity);
        return ValueTask.FromResult<IRepository<TEntity>>(this);
    }

    public IRepository<TEntity> AddRange(ICollection<TEntity> entities)
    {
        CurrentContext.AddRangeAsync(entities);
        return this;
    }

    #endregion

    protected IQueryable<TEntity> QueryableWithIncludes(
        Func<IQueryable<TEntity>,
        IQueryable<TEntity>> includeFunc,
        bool? asNoTracking = true
    )
    {
        IQueryable<TEntity> queryable;

        if (asNoTracking.HasValue && asNoTracking.Value)
            queryable = Queryable().AsNoTracking();
        else
            queryable = Queryable();

        if (includeFunc != null)
        {
            queryable = includeFunc(queryable);
        }

        return queryable;
    }

    public ValueTask MarkAs(TEntity entity, EntityState entityState)
    {
        CurrentContext.Entry(entity).State = entityState;
        return ValueTask.CompletedTask;
    }
}

public class EfApplicationDbRepository<TEntity>(EfUnitOfWork<ApplicationDbContext> unitOfWork)
    : EfRepository<ApplicationDbContext, TEntity>(unitOfWork)
    where TEntity : class;