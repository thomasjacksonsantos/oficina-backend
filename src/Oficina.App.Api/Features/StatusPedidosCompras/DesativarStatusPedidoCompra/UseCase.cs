



using Oficina.Domain.SeedWork;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;
using Oficina.Domain.Aggregates.StatusPedidoCompraAggregates;

namespace Oficina.App.Api.Features.StatusPedidosCompras.DesativarStatusPedidoCompra;

public sealed class UseCase(
    IFluentQuery fluentQuery,
    IUnitOfWork unitOfWork
)
    : IUseCase<DesativarStatusPedidoCompraRequest, DesativarStatusPedidoCompraResponse>
{
    public async Task<Result<DesativarStatusPedidoCompraResponse>> Execute(
        DesativarStatusPedidoCompraRequest input,
        CancellationToken ct = default
    )
    {
        var statusPedidoCompra = await fluentQuery.For<StatusPedidoCompra>()
            .WithPredicate(x => x.Id == input.Id.DecodeWithSqids())
            .WithTracking()
            .FindFirstAsync(ct);

        if (statusPedidoCompra == null)
            return Result.Fail(Erro.ValorInvalido($"{nameof(StatusPedidoCompra)}.{nameof(input.Id)}", "Status de pedido de compra não encontrado."));

        statusPedidoCompra.Desativar();

        await unitOfWork.SaveChangesAsync(ct);

        return new DesativarStatusPedidoCompraResponse("Status de pedido de compra desativado com sucesso!");
    }
}
