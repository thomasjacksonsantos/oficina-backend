using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Oficina.App.Api.Features.MarcasProdutos.CadastrarMarcaProduto;

public class Endpoint(
    IUseCase<CadastrarMarcaProdutoRequest, CadastrarMarcaProdutoResponse> useCase)
    : ResultBaseEndpoint<CadastrarMarcaProdutoRequest, CadastrarMarcaProdutoResponse>(useCase)
{
    public override void Configure()
    {
        Post("v1/produtos/marcas-produtos");
        PreProcessor<AuthInterceptor<CadastrarMarcaProdutoRequest>>();
        Description(c => c.Accepts<CadastrarMarcaProdutoRequest>()
                .Produces<CadastrarMarcaProdutoResponse>()
                .ProducesProblem(400)
                .WithTags("Marcas de Produtos")
            , clearDefaults: false);
    }
}