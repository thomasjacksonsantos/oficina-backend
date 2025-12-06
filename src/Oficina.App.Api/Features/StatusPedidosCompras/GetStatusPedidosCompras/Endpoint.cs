using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Oficina.Domain.ValueObjects;

namespace Oficina.App.Api.Features.StatusPedidosCompras.GetStatusPedidosCompras;

public class Endpoint(
    IUseCase<GetStatusPedidosComprasRequest, PagedResult<GetStatusPedidosComprasResponse>> useCase)
    : ResultBaseEndpoint<GetStatusPedidosComprasRequest, PagedResult<GetStatusPedidosComprasResponse>>(useCase)
{
    public override void Configure()
    {
        Get("v1/produtos/status-pedidos-compras/all");
        PreProcessor<AuthInterceptor<GetStatusPedidosComprasRequest>>();
        Description(c => c.Accepts<GetStatusPedidosComprasRequest>()
                .Produces<GetStatusPedidosComprasResponse>()
                .ProducesProblem(400)
                .WithTags("Status de Pedidos Compras")
            , clearDefaults: false);
    }
}