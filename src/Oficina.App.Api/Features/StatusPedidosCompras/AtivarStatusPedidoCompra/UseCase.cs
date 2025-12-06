



using Oficina.Domain.SeedWork;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;
using Oficina.Domain.Aggregates.StatusPedidoCompraAggregates;

namespace Oficina.App.Api.Features.StatusPedidosCompras.AtivarStatusPedidoCompra;

public sealed class UseCase(
    IFluentQuery fluentQuery,
    IUnitOfWork unitOfWork
)
    : IUseCase<AtivarStatusPedidoCompraRequest, AtivarStatusPedidoCompraResponse>
{
    public async Task<Result<AtivarStatusPedidoCompraResponse>> Execute(
        AtivarStatusPedidoCompraRequest input,
        CancellationToken ct = default
    )
    {
        var statusPedidoCompra = await fluentQuery.For<StatusPedidoCompra>()
            .WithPredicate(x => x.Id == input.Id.DecodeWithSqids())
            .WithTracking()
            .FindFirstAsync(ct);

        if (statusPedidoCompra == null)
            return Result.Fail(Erro.ValorInvalido($"{nameof(StatusPedidoCompra)}.{nameof(input.Id)}", "Status do pedido de compra não encontrado."));

        statusPedidoCompra.Ativar();

        await unitOfWork.SaveChangesAsync(ct);

        return new AtivarStatusPedidoCompraResponse("Status do pedido de compra ativado com sucesso!");
    }
}
