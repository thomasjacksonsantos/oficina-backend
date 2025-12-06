using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Oficina.App.Api.Features.MarcasProdutos.AtivarMarcaProduto;

public class Endpoint(
    IUseCase<AtivarMarcaProdutoRequest, AtivarMarcaProdutoResponse> useCase)
    : ResultBaseEndpoint<AtivarMarcaProdutoRequest, AtivarMarcaProdutoResponse>(useCase)
{
    public override void Configure()
    {
        Put("v1/produtos/marcas-produtos/{id}/ativar");
        PreProcessor<AuthInterceptor<AtivarMarcaProdutoRequest>>();
        Description(c => c.Accepts<AtivarMarcaProdutoRequest>()
                .Produces<AtivarMarcaProdutoResponse>()
                .ProducesProblem(400)
                .WithTags("Marcas de Produtos")
            , clearDefaults: false);
    }
}