using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Oficina.App.Api.Features.StatusPedidosCompras.AtivarStatusPedidoCompra;

public class Endpoint(
    IUseCase<AtivarStatusPedidoCompraRequest, AtivarStatusPedidoCompraResponse> useCase)
    : ResultBaseEndpoint<AtivarStatusPedidoCompraRequest, AtivarStatusPedidoCompraResponse>(useCase)
{
    public override void Configure()
    {
        Put("v1/produtos/status-pedidos-compras/{id}/ativar");
        PreProcessor<AuthInterceptor<AtivarStatusPedidoCompraRequest>>();
        Description(c => c.Accepts<AtivarStatusPedidoCompraRequest>()
                .Produces<AtivarStatusPedidoCompraResponse>()
                .ProducesProblem(400)
                .WithTags("Status de Pedidos Compras")
            , clearDefaults: false);
    }
}