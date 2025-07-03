
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Oficina.App.Api.Shared;

public interface IAuthProvider
{
    TRequest FillAuthRequest<TRequest>(TRequest request) where TRequest : AuthRequest;
}

public sealed class HttpAuthProvider(
    IHttpContextAccessor httpContextAccessor,
    IServiceProvider serviceProvider
) : IAuthProvider
{
    private readonly IServiceProvider serviceProvider = serviceProvider;

    public TRequest FillAuthRequest<TRequest>(TRequest request)
        where TRequest : AuthRequest
    {
        var context = httpContextAccessor?.HttpContext;

        if (context is null)
            throw new NullReferenceException();

        var claims = context.User?.Claims.Select(x => new { x.Type, x.Value }).ToList();
        request.UserName = context.User?.Identity?.Name;
        request.Email = claims?.Where(c => c.Type == System.Security.Claims.ClaimTypes.Email).Select(c => c.Value)
            .FirstOrDefault();

        var id = claims?.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        // using var scope = serviceProvider.CreateScope();
        // var repository = scope.ServiceProvider.GetRequiredService<IRepository<UsuarioBase>>();

        request.UserId = id!.DecodeWithSqids();

        // request.UsuarioPerfis = repository.FindAllByPredicate<UsuarioPerfilRequest>(
        //     predicate: c => c.UsuarioId == id!.DecodeWithSqids(),
        //     projection: c => new UsuarioPerfilRequest(c.Id, c.TipoUsuario.Nome)
        // ).Result;

        return request;
    }
}