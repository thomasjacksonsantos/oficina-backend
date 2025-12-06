



using Microsoft.EntityFrameworkCore;
using Oficina.Domain.Aggregates.StatusPedidoCompraAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.App.Api.Features.StatusPedidosCompras.GetStatusPedidoCompraById;

public sealed class UseCase(
    IFluentQuery fluentQuery
)
    : IUseCase<GetStatusPedidoCompraByIdRequest, GetStatusPedidoCompraByIdResponse>
{
    public async Task<Result<GetStatusPedidoCompraByIdResponse>> Execute(
        GetStatusPedidoCompraByIdRequest input,
        CancellationToken ct = default
    )
    {
        var statusPedidoCompra = await fluentQuery.For<StatusPedidoCompra>()
            .WithPredicate(x => x.Id == input.Id.DecodeWithSqids())
            .WithIncludes(x => x.Include(c => c.StatusPedidoCompraStatus))
            .FindFirstAsync(ct);
        
        if (statusPedidoCompra is null)
            return Erro.ValorInvalido("Status de pedido de compra não encontrado.");

        return new GetStatusPedidoCompraByIdResponse
        (
            statusPedidoCompra.Id.EncodeWithSqids(),
            statusPedidoCompra.Descricao,
            statusPedidoCompra.StatusPedidoCompraStatus.Key,
            statusPedidoCompra.Criado.Valor,
            statusPedidoCompra.Atualizado.Valor
        );
    }
}