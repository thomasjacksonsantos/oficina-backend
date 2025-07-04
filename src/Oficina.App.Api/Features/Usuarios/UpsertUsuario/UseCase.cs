


using Oficina.Domain.Aggregates.UsuarioAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.App.Api.Features.Usuarios.UpsertUsuario;

public sealed class UseCase(
    IRepository<Usuario> usuarioRepository
)
    : IUseCase<UpsertUsuarioRequest, UpsertUsuarioResponse>
{
    public async Task<Result<UpsertUsuarioResponse>> Execute(
        UpsertUsuarioRequest input,
        CancellationToken ct = default
    )
    {
        var usuario = await usuarioRepository.FindFirstByPredicate(c => c.UserId == input.UserId);

        return Result.Success();
    }
}
