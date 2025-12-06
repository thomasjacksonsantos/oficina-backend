using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Oficina.App.Api.Features.MarcasProdutos.AtualizarMarcaProduto;

public class Endpoint(
    IUseCase<AtualizarMarcaProdutoRequest, AtualizarMarcaProdutoResponse> useCase)
    : ResultBaseEndpoint<AtualizarMarcaProdutoRequest, AtualizarMarcaProdutoResponse>(useCase)
{
    public override void Configure()
    {
        Put("v1/produtos/marcas-produtos");
        PreProcessor<AuthInterceptor<AtualizarMarcaProdutoRequest>>();
        Description(c => c.Accepts<AtualizarMarcaProdutoRequest>()
                .Produces<AtualizarMarcaProdutoResponse>()
                .ProducesProblem(400)
                .WithTags("Marcas de Produtos")
            , clearDefaults: false);
    }
}