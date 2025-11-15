


using Microsoft.EntityFrameworkCore;
using Oficina.Domain.Aggregates.UsuarioAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.App.Api.Features.Usuarios.GetUsuarioContexto;

public sealed class UseCase(
    IFluentQuery fluentQuery
)
    : IUseCase<GetUsuarioContextoRequest, GetUsuarioContextoResponse>
{
    public async Task<Result<GetUsuarioContextoResponse>> Execute(
        GetUsuarioContextoRequest input,
        CancellationToken ct = default
    )
    {
        var usuario = await fluentQuery.For<UsuarioContexto>()
            .WithIncludes(c =>
                c.Include(c => c.Loja)
                .Include(c => c.Conta)
                .Include(c => c.Usuario))
            .WithPredicate(c => c.Usuario!.UserId == input.UserId)
            .WithProjection(c => new GetUsuarioContextoResponse
            (
                new UsuarioContextoResponse(
                    c.Usuario!.Id.EncodeWithSqids(),
                    c.Usuario!.Nome
                ),
                new LojaContextoResponse(
                    c.Loja!.Id!.EncodeWithSqids(),
                    c.Loja!.NomeFantasia
                ),
                new ContaContextoResponse(
                    c.Conta!.Id!.EncodeWithSqids(),
                    c.Conta!.Nome
                )
            ))
            .FindFirstAsync(ct);

        if (usuario is null)
            return Result.Fail(Erro.ValorInvalido("Usuário não encontrado."));

        return usuario;
    }
}
