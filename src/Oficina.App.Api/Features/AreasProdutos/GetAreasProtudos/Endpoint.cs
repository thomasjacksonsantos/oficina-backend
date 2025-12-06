using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Oficina.Domain.ValueObjects;

namespace Oficina.App.Api.Features.AreasProdutos.GetAreasProdutos;

public class Endpoint(
    IUseCase<GetAreasProdutosRequest, PagedResult<GetAreasProdutosResponse>> useCase)
    : ResultBaseEndpoint<GetAreasProdutosRequest, PagedResult<GetAreasProdutosResponse>>(useCase)
{
    public override void Configure()
    {
        Get("v1/produtos/areas-produtos/all");
        PreProcessor<AuthInterceptor<GetAreasProdutosRequest>>();
        Description(c => c.Accepts<GetAreasProdutosRequest>()
                .Produces<GetAreasProdutosResponse>()
                .ProducesProblem(400)
                .WithTags("Areas Produtos")
            , clearDefaults: false);
    }
}