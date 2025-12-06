using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Oficina.App.Api.Features.AreasProdutos.AtivarAreaProduto;

public class Endpoint(
    IUseCase<AtivarAreaProdutoRequest, AtivarAreaProdutoResponse> useCase)
    : ResultBaseEndpoint<AtivarAreaProdutoRequest, AtivarAreaProdutoResponse>(useCase)
{
    public override void Configure()
    {
        Put("v1/produtos/areas-produtos/{id}/ativar");
        PreProcessor<AuthInterceptor<AtivarAreaProdutoRequest>>();
        Description(c => c.Accepts<AtivarAreaProdutoRequest>()
                .Produces<AtivarAreaProdutoResponse>()
                .ProducesProblem(400)
                .WithTags("Areas Produtos")
            , clearDefaults: false);
    }
}