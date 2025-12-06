using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Oficina.App.Api.Features.StatusPedidosCompras.AtualizarStatusPedidoCompra;

public class Endpoint(
    IUseCase<AtualizarStatusPedidoCompraRequest, AtualizarStatusPedidoCompraResponse> useCase)
    : ResultBaseEndpoint<AtualizarStatusPedidoCompraRequest, AtualizarStatusPedidoCompraResponse>(useCase)
{
    public override void Configure()
    {
        Put("v1/produtos/status-pedidos-compras");
        PreProcessor<AuthInterceptor<AtualizarStatusPedidoCompraRequest>>();
        Description(c => c.Accepts<AtualizarStatusPedidoCompraRequest>()
                .Produces<AtualizarStatusPedidoCompraResponse>()
                .ProducesProblem(400)
                .WithTags("Status de Pedidos Compras")
            , clearDefaults: false);
    }
}