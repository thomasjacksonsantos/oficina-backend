using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Oficina.App.Api.Features.AreasProdutos.DesativarAreaProduto;

public class Endpoint(
    IUseCase<DesativarAreaProdutoRequest, DesativarAreaProdutoResponse> useCase)
    : ResultBaseEndpoint<DesativarAreaProdutoRequest, DesativarAreaProdutoResponse>(useCase)
{
    public override void Configure()
    {
        Delete("v1/produtos/areas-produtos/{id}/desativar");
        PreProcessor<AuthInterceptor<DesativarAreaProdutoRequest>>();
        Description(c => c.Accepts<DesativarAreaProdutoRequest>()
                .Produces<DesativarAreaProdutoResponse>()
                .ProducesProblem(400)
                .WithTags("Areas Produtos")
            , clearDefaults: false);
    }
}