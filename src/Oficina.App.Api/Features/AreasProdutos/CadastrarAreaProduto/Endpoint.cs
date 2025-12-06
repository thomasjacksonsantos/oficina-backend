using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Oficina.App.Api.Features.AreasProdutos.CadastrarAreaProduto;

public class Endpoint(
    IUseCase<CadastrarAreaProdutoRequest, CadastrarAreaProdutoResponse> useCase)
    : ResultBaseEndpoint<CadastrarAreaProdutoRequest, CadastrarAreaProdutoResponse>(useCase)
{
    public override void Configure()
    {
        Post("v1/produtos/areas-produtos");
        PreProcessor<AuthInterceptor<CadastrarAreaProdutoRequest>>();
        Description(c => c.Accepts<CadastrarAreaProdutoRequest>()
                .Produces<CadastrarAreaProdutoResponse>()
                .ProducesProblem(400)
                .WithTags("Areas Produtos")
            , clearDefaults: false);
    }
}