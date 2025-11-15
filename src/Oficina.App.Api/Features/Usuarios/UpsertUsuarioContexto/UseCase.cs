


using Oficina.Domain.Aggregates.UsuarioAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.App.Api.Features.Usuarios.UpsertUsuarioContexto;

public sealed class UseCase(
    IRepository<UsuarioContexto> usuarioContextoRepository,
    IUnitOfWork unitOfWork
)
    : IUseCase<UpsertUsuarioContextoRequest, UpsertUsuarioContextoResponse>
{
    public async Task<Result<UpsertUsuarioContextoResponse>> Execute(
        UpsertUsuarioContextoRequest input,
        CancellationToken ct = default
    )
    {
        var usuarioContext = await usuarioContextoRepository.FindFirstByPredicate(c => c.Usuario!.UserId == input.UserId);
        if (usuarioContext is null)
        {
            return Result.Fail(
                Erro.NaoEncontrado(
                    "UsuarioContexto",
                    "O contexto do usuário não foi encontrado."
                )
            );
        }

        usuarioContext.AtualizarLojaPrincipal(
            input.Loja.Id.DecodeWithSqids()
        );

        await unitOfWork.SaveChangesAsync(ct);

        return Result.Success();
    }
}
