



using Microsoft.EntityFrameworkCore;
using Oficina.Domain.Aggregates.StatusPedidoCompraAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Domain.ValueObjects;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.App.Api.Features.StatusPedidosCompras.GetStatusPedidosCompras;

public sealed class UseCase(
    IFluentQuery fluentQuery
)
    : IUseCase<GetStatusPedidosComprasRequest, PagedResult<GetStatusPedidosComprasResponse>>
{
    public async Task<Result<PagedResult<GetStatusPedidosComprasResponse>>> Execute(
        GetStatusPedidosComprasRequest input,
        CancellationToken ct = default
    )
    {
        var statusPedidosCompras = await fluentQuery.For<StatusPedidoCompra>()
            .WithIncludes(x => x.Include(c => c.StatusPedidoCompraStatus))
            .FindAllPagedAsync(input.Pagina, input.TotalPagina, ct);

        return statusPedidosCompras.MapTo(x => new GetStatusPedidosComprasResponse
        (
            x.Id.EncodeWithSqids(),
            x.Descricao,
            x.StatusPedidoCompraStatus.Key,
            x.Criado.Valor,
            x.Atualizado.Valor
        ));
    }
}