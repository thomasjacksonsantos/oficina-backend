using System.Linq.Expressions;
using System.Reflection;
using Oficina.Domain.SeedWork;
using Oficina.Domain.ValueObjects;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Oficina.Infrastructure.DataAccess;

public sealed class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int,
    ApplicationUserClaim, ApplicationUserRole, IdentityUserLogin<int>,
    IdentityRoleClaim<int>, IdentityUserToken<int>>
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
 
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    internal async Task<object?> ExecuteRawSql(string sql, object parameters)
    {
        var connection = Database.GetDbConnection() as SqlConnection;
        return await connection!.ExecuteScalarAsync(sql, param: parameters, commandType: System.Data.CommandType.Text);
    }

    internal async Task<T?> ExecuteRawSql<T>(string sql, object parameters)
    {
        var connection = Database.GetDbConnection() as SqlConnection;
        return await connection!.ExecuteScalarAsync<T>(sql, param: parameters, commandType: System.Data.CommandType.Text);
    }

    internal async Task<int> ExecuteRawSql(string sql)
    {
        return await Database.ExecuteSqlRawAsync(sql);
    }
}