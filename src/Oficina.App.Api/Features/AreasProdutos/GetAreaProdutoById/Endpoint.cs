using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Oficina.App.Api.Features.AreasProdutos.GetAreaProdutoById;

public class Endpoint(
    IUseCase<GetAreaProdutoByIdRequest, GetAreaProdutoByIdResponse> useCase)
    : ResultBaseEndpoint<GetAreaProdutoByIdRequest, GetAreaProdutoByIdResponse>(useCase)
{
    public override void Configure()
    {
        Get("v1/produtos/areas-produtos/{id}");
        PreProcessor<AuthInterceptor<GetAreaProdutoByIdRequest>>();
        Description(c => c.Accepts<GetAreaProdutoByIdRequest>()
                .Produces<GetAreaProdutoByIdResponse>()
                .ProducesProblem(400)
                .WithTags("Areas Produtos")
            , clearDefaults: false);
    }
}