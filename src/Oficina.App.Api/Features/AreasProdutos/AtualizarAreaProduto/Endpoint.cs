using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Oficina.App.Api.Features.AreasProdutos.AtualizarAreaProduto;

public class Endpoint(
    IUseCase<AtualizarAreaProdutoRequest, AtualizarAreaProdutoResponse> useCase)
    : ResultBaseEndpoint<AtualizarAreaProdutoRequest, AtualizarAreaProdutoResponse>(useCase)
{
    public override void Configure()
    {
        Put("v1/produtos/areas-produtos");
        PreProcessor<AuthInterceptor<AtualizarAreaProdutoRequest>>();
        Description(c => c.Accepts<AtualizarAreaProdutoRequest>()
                .Produces<AtualizarAreaProdutoResponse>()
                .ProducesProblem(400)
                .WithTags("Areas Produtos")
            , clearDefaults: false);
    }
}