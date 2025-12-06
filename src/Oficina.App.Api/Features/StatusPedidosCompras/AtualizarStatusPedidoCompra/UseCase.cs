


using Oficina.Domain.Aggregates.StatusPedidoCompraAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.App.Api.Features.StatusPedidosCompras.AtualizarStatusPedidoCompra;

public sealed class UseCase(
    IFluentQuery fluentQuery,
    IUnitOfWork unitOfWork
)
    : IUseCase<AtualizarStatusPedidoCompraRequest, AtualizarStatusPedidoCompraResponse>
{
    public async Task<Result<AtualizarStatusPedidoCompraResponse>> Execute(
        AtualizarStatusPedidoCompraRequest input,
        CancellationToken ct = default
    )
    {
        var statusPedidoCompra = await fluentQuery.For<StatusPedidoCompra>()
            .WithPredicate(x =>
                x.Id == input.Id.DecodeWithSqids()
            )
            .WithTracking()
            .FindFirstAsync(ct);

        if (statusPedidoCompra is null)
            return Result.Fail(
                Erro.ValorInvalido(
                    $"{nameof(StatusPedidoCompra)}.{nameof(AtualizarStatusPedidoCompraRequest.Id)}", 
                    $"Status do pedido de compra {input.Id} não encontrado no sistema."
                ));

        statusPedidoCompra.Atualizar(
            input.Descricao
        );

        await unitOfWork.SaveChangesAsync(ct);

        return new AtualizarStatusPedidoCompraResponse("Status do pedido de compra atualizado com sucesso!");
    }
}
