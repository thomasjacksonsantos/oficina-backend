using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Oficina.App.Api.Features.UnidadesProdutos.GetUnidadeProdutoById;

public class Endpoint(
    IUseCase<GetUnidadeProdutoByIdRequest, GetUnidadeProdutoByIdResponse> useCase)
    : ResultBaseEndpoint<GetUnidadeProdutoByIdRequest, GetUnidadeProdutoByIdResponse>(useCase)
{
    public override void Configure()
    {
        Get("v1/produtos/unidades-produtos/{id}");
        PreProcessor<AuthInterceptor<GetUnidadeProdutoByIdRequest>>();
        Description(c => c.Accepts<GetUnidadeProdutoByIdRequest>()
                .Produces<GetUnidadeProdutoByIdResponse>()
                .ProducesProblem(400)
                .WithTags("Unidades do Produtos")
            , clearDefaults: false);
    }
}