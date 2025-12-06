


using Oficina.Domain.Aggregates.GrupoProdutoAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.App.Api.Features.GruposProdutos.AtualizarGrupoProduto;

public sealed class UseCase(
    IFluentQuery fluentQuery,
    IUnitOfWork unitOfWork
)
    : IUseCase<AtualizarGrupoProdutoRequest, AtualizarGrupoProdutoResponse>
{
    public async Task<Result<AtualizarGrupoProdutoResponse>> Execute(
        AtualizarGrupoProdutoRequest input,
        CancellationToken ct = default
    )
    {
        var grupoProduto = await fluentQuery.For<GrupoProduto>()
            .WithPredicate(x =>
                x.Id == input.Id.DecodeWithSqids()
            )
            .WithTracking()
            .FindFirstAsync(ct);

        if (grupoProduto is null)
            return Result.Fail(
                Erro.ValorInvalido(
                    $"{nameof(GrupoProduto)}.{nameof(AtualizarGrupoProdutoRequest.Id)}", 
                    $"Grupo de produto {input.Id} não encontrado no sistema."
                ));

        grupoProduto.Atualizar(
            input.Descricao,
            input.Area,
            input.NCM,
            input.ANP
        );

        await unitOfWork.SaveChangesAsync(ct);

        return new AtualizarGrupoProdutoResponse("Grupo de produto atualizado com sucesso!");
    }
}
