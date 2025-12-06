using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Oficina.App.Api.Features.MarcasProdutos.GetMarcaProdutoById;

public class Endpoint(
    IUseCase<GetMarcaProdutoByIdRequest, GetMarcaProdutoByIdResponse> useCase)
    : ResultBaseEndpoint<GetMarcaProdutoByIdRequest, GetMarcaProdutoByIdResponse>(useCase)
{
    public override void Configure()
    {
        Get("v1/produtos/marcas-produtos/{id}");
        PreProcessor<AuthInterceptor<GetMarcaProdutoByIdRequest>>();
        Description(c => c.Accepts<GetMarcaProdutoByIdRequest>()
                .Produces<GetMarcaProdutoByIdResponse>()
                .ProducesProblem(400)
                .WithTags("Marcas de Produtos")
            , clearDefaults: false);
    }
}