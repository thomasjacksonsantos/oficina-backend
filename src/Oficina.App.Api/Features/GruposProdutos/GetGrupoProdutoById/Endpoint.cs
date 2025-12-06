using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Oficina.App.Api.Features.GruposProdutos.GetGrupoProdutoById;

public class Endpoint(
    IUseCase<GetGrupoProdutoByIdRequest, GetGrupoProdutoByIdResponse> useCase)
    : ResultBaseEndpoint<GetGrupoProdutoByIdRequest, GetGrupoProdutoByIdResponse>(useCase)
{
    public override void Configure()
    {
        Get("v1/produtos/grupos-produtos/{id}");
        PreProcessor<AuthInterceptor<GetGrupoProdutoByIdRequest>>();
        Description(c => c.Accepts<GetGrupoProdutoByIdRequest>()
                .Produces<GetGrupoProdutoByIdResponse>()
                .ProducesProblem(400)
                .WithTags("Grupos de Produtos")
            , clearDefaults: false);
    }
}