using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Oficina.App.Api.Features.GruposProdutos.DesativarGrupoProduto;

public class Endpoint(
    IUseCase<DesativarGrupoProdutoRequest, DesativarGrupoProdutoResponse> useCase)
    : ResultBaseEndpoint<DesativarGrupoProdutoRequest, DesativarGrupoProdutoResponse>(useCase)
{
    public override void Configure()
    {
        Delete("v1/produtos/grupos-produtos/{id}/desativar");
        PreProcessor<AuthInterceptor<DesativarGrupoProdutoRequest>>();
        Description(c => c.Accepts<DesativarGrupoProdutoRequest>()
                .Produces<DesativarGrupoProdutoResponse>()
                .ProducesProblem(400)
                .WithTags("Grupos de Produtos")
            , clearDefaults: false);
    }
}