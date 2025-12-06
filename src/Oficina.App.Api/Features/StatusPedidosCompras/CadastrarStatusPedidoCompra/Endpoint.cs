using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Oficina.App.Api.Features.StatusPedidosCompras.CadastrarStatusPedidoCompra;

public class Endpoint(
    IUseCase<CadastrarStatusPedidoCompraRequest, CadastrarStatusPedidoCompraResponse> useCase)
    : ResultBaseEndpoint<CadastrarStatusPedidoCompraRequest, CadastrarStatusPedidoCompraResponse>(useCase)
{
    public override void Configure()
    {
        Post("v1/produtos/status-pedidos-compras");
        PreProcessor<AuthInterceptor<CadastrarStatusPedidoCompraRequest>>();
        Description(c => c.Accepts<CadastrarStatusPedidoCompraRequest>()
                .Produces<CadastrarStatusPedidoCompraResponse>()
                .ProducesProblem(400)
                .WithTags("Status de Pedidos Compras")
            , clearDefaults: false);
    }
}