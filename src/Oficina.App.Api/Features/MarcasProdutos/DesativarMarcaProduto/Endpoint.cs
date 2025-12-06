using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Oficina.App.Api.Features.MarcasProdutos.DesativarMarcaProduto;

public class Endpoint(
    IUseCase<DesativarMarcaProdutoRequest, DesativarMarcaProdutoResponse> useCase)
    : ResultBaseEndpoint<DesativarMarcaProdutoRequest, DesativarMarcaProdutoResponse>(useCase)
{
    public override void Configure()
    {
        Delete("v1/produtos/marcas-produtos/{id}/desativar");
        PreProcessor<AuthInterceptor<DesativarMarcaProdutoRequest>>();
        Description(c => c.Accepts<DesativarMarcaProdutoRequest>()
                .Produces<DesativarMarcaProdutoResponse>()
                .ProducesProblem(400)
                .WithTags("Marcas de Produtos")
            , clearDefaults: false);
    }
}