using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Oficina.App.Api.Features.StatusPedidosCompras.DesativarStatusPedidoCompra;

public class Endpoint(
    IUseCase<DesativarStatusPedidoCompraRequest, DesativarStatusPedidoCompraResponse> useCase)
    : ResultBaseEndpoint<DesativarStatusPedidoCompraRequest, DesativarStatusPedidoCompraResponse>(useCase)
{
    public override void Configure()
    {
        Delete("v1/produtos/status-pedidos-compras/{id}/desativar");
        PreProcessor<AuthInterceptor<DesativarStatusPedidoCompraRequest>>();
        Description(c => c.Accepts<DesativarStatusPedidoCompraRequest>()
                .Produces<DesativarStatusPedidoCompraResponse>()
                .ProducesProblem(400)
                .WithTags("Status de Pedidos Compras")
            , clearDefaults: false);
    }
}