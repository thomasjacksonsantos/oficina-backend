using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Oficina.App.Api.Features.UnidadesProdutos.AtivarUnidadeProduto;

public class Endpoint(
    IUseCase<AtivarUnidadeProdutoRequest, AtivarUnidadeProdutoResponse> useCase)
    : ResultBaseEndpoint<AtivarUnidadeProdutoRequest, AtivarUnidadeProdutoResponse>(useCase)
{
    public override void Configure()
    {
        Put("v1/produtos/unidades-produtos/{id}/ativar");
        PreProcessor<AuthInterceptor<AtivarUnidadeProdutoRequest>>();
        Description(c => c.Accepts<AtivarUnidadeProdutoRequest>()
                .Produces<AtivarUnidadeProdutoResponse>()
                .ProducesProblem(400)
                .WithTags("Unidades do Produtos")
            , clearDefaults: false);
    }
}