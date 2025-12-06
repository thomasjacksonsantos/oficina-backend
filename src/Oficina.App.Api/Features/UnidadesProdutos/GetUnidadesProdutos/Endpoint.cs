using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Oficina.Domain.ValueObjects;

namespace Oficina.App.Api.Features.UnidadesProdutos.GetUnidadesProdutos;

public class Endpoint(
    IUseCase<GetUnidadesProdutosRequest, PagedResult<GetUnidadesProdutosResponse>> useCase)
    : ResultBaseEndpoint<GetUnidadesProdutosRequest, PagedResult<GetUnidadesProdutosResponse>>(useCase)
{
    public override void Configure()
    {
        Get("v1/produtos/unidades-produtos/all");
        PreProcessor<AuthInterceptor<GetUnidadesProdutosRequest>>();
        Description(c => c.Accepts<GetUnidadesProdutosRequest>()
                .Produces<GetUnidadesProdutosResponse>()
                .ProducesProblem(400)
                .WithTags("Unidades do Produtos")
            , clearDefaults: false);
    }
}