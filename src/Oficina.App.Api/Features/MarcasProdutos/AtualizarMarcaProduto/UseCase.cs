


using Oficina.Domain.Aggregates.MarcaProdutoAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.App.Api.Features.MarcasProdutos.AtualizarMarcaProduto;

public sealed class UseCase(
    IFluentQuery fluentQuery,
    IUnitOfWork unitOfWork
)
    : IUseCase<AtualizarMarcaProdutoRequest, AtualizarMarcaProdutoResponse>
{
    public async Task<Result<AtualizarMarcaProdutoResponse>> Execute(
        AtualizarMarcaProdutoRequest input,
        CancellationToken ct = default
    )
    {
        var marcaProduto = await fluentQuery.For<MarcaProduto>()
            .WithPredicate(x =>
                x.Id == input.Id.DecodeWithSqids()
            )
            .WithTracking()
            .FindFirstAsync(ct);

        if (marcaProduto is null)
            return Result.Fail(
                Erro.ValorInvalido(
                    $"{nameof(MarcaProduto)}.{nameof(AtualizarMarcaProdutoRequest.Id)}", 
                    $"Marca de produto {input.Id} não encontrado no sistema."
                ));

        marcaProduto.Atualizar(
            input.Descricao
        );

        await unitOfWork.SaveChangesAsync(ct);

        return new AtualizarMarcaProdutoResponse("Marca de produto atualizada com sucesso!");
    }
}
