using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Oficina.App.Api.Features.GruposProdutos.AtivarGrupoProduto;

public class Endpoint(
    IUseCase<AtivarGrupoProdutoRequest, AtivarGrupoProdutoResponse> useCase)
    : ResultBaseEndpoint<AtivarGrupoProdutoRequest, AtivarGrupoProdutoResponse>(useCase)
{
    public override void Configure()
    {
        Put("v1/produtos/grupos-produtos/{id}/ativar");
        PreProcessor<AuthInterceptor<AtivarGrupoProdutoRequest>>();
        Description(c => c.Accepts<AtivarGrupoProdutoRequest>()
                .Produces<AtivarGrupoProdutoResponse>()
                .ProducesProblem(400)
                .WithTags("Grupos de Produtos")
            , clearDefaults: false);
    }
}