using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Oficina.App.Api.Features.UnidadesProdutos.DesativarUnidadeProduto;

public class Endpoint(
    IUseCase<DesativarUnidadeProdutoRequest, DesativarUnidadeProdutoResponse> useCase)
    : ResultBaseEndpoint<DesativarUnidadeProdutoRequest, DesativarUnidadeProdutoResponse>(useCase)
{
    public override void Configure()
    {
        Delete("v1/produtos/unidades-produtos/{id}/desativar");
        PreProcessor<AuthInterceptor<DesativarUnidadeProdutoRequest>>();
        Description(c => c.Accepts<DesativarUnidadeProdutoRequest>()
                .Produces<DesativarUnidadeProdutoResponse>()
                .ProducesProblem(400)
                .WithTags("Unidades do Produtos")
            , clearDefaults: false);
    }
}