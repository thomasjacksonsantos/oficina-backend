using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Oficina.App.Api.Features.StatusPedidosCompras.GetStatusPedidoCompraById;

public class Endpoint(
    IUseCase<GetStatusPedidoCompraByIdRequest, GetStatusPedidoCompraByIdResponse> useCase)
    : ResultBaseEndpoint<GetStatusPedidoCompraByIdRequest, GetStatusPedidoCompraByIdResponse>(useCase)
{
    public override void Configure()
    {
        Get("v1/produtos/status-pedidos-compras/{id}");
        PreProcessor<AuthInterceptor<GetStatusPedidoCompraByIdRequest>>();
        Description(c => c.Accepts<GetStatusPedidoCompraByIdRequest>()
                .Produces<GetStatusPedidoCompraByIdResponse>()
                .ProducesProblem(400)
                .WithTags("Status de Pedidos Compras")
            , clearDefaults: false);
    }
}