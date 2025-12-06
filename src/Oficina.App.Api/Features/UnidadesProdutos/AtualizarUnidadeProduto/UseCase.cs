


using Oficina.Domain.Aggregates.UnidadeProdutoAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.App.Api.Features.UnidadesProdutos.AtualizarUnidadeProduto;

public sealed class UseCase(
    IFluentQuery fluentQuery,
    IUnitOfWork unitOfWork
)
    : IUseCase<AtualizarUnidadeProdutoRequest, AtualizarUnidadeProdutoResponse>
{
    public async Task<Result<AtualizarUnidadeProdutoResponse>> Execute(
        AtualizarUnidadeProdutoRequest input,
        CancellationToken ct = default
    )
    {
        var unidadeProduto = await fluentQuery.For<UnidadeProduto>()
            .WithPredicate(x =>
                x.Id == input.Id.DecodeWithSqids()
            )
            .WithTracking()
            .FindFirstAsync(ct);

        if (unidadeProduto is null)
            return Result.Fail(
                Erro.ValorInvalido(
                    $"{nameof(UnidadeProduto)}.{nameof(AtualizarUnidadeProdutoRequest.Id)}", 
                    $"Unidade de produto {input.Id} não encontrado no sistema."
                ));

        unidadeProduto.Atualizar(
            input.Descricao
        );

        await unitOfWork.SaveChangesAsync(ct);

        return new AtualizarUnidadeProdutoResponse("Unidade de produto atualizada com sucesso!");
    }
}
