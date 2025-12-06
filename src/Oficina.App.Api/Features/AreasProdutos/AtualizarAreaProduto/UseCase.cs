


using Oficina.Domain.Aggregates.AreaProdutoAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.App.Api.Features.AreasProdutos.AtualizarAreaProduto;

public sealed class UseCase(
    IFluentQuery fluentQuery,
    IUnitOfWork unitOfWork
)
    : IUseCase<AtualizarAreaProdutoRequest, AtualizarAreaProdutoResponse>
{
    public async Task<Result<AtualizarAreaProdutoResponse>> Execute(
        AtualizarAreaProdutoRequest input,
        CancellationToken ct = default
    )
    {
        var areaProduto = await fluentQuery.For<AreaProduto>()
            .WithPredicate(x =>
                x.Id == input.Id.DecodeWithSqids()
            )
            .WithTracking()
            .FindFirstAsync(ct);

        if (areaProduto is null)
            return Result.Fail(
                Erro.ValorInvalido(
                    $"{nameof(AreaProduto)}.{nameof(AtualizarAreaProdutoRequest.Id)}", 
                    $"Área de produto {input.Id} não encontrada no sistema."
                ));

        areaProduto.Atualizar(
            input.Descricao,
            input.Garantia            
        );

        await unitOfWork.SaveChangesAsync(ct);

        return new AtualizarAreaProdutoResponse("Área de produto atualizada com sucesso!");
    }
}
