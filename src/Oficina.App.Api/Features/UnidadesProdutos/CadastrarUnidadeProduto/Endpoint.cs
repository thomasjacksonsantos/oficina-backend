using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Oficina.App.Api.Features.UnidadesProdutos.CadastrarUnidadeProduto;

public class Endpoint(
    IUseCase<CadastrarUnidadeProdutoRequest, CadastrarUnidadeProdutoResponse> useCase)
    : ResultBaseEndpoint<CadastrarUnidadeProdutoRequest, CadastrarUnidadeProdutoResponse>(useCase)
{
    public override void Configure()
    {
        Post("v1/produtos/unidades-produtos");
        PreProcessor<AuthInterceptor<CadastrarUnidadeProdutoRequest>>();
        Description(c => c.Accepts<CadastrarUnidadeProdutoRequest>()
                .Produces<CadastrarUnidadeProdutoResponse>()
                .ProducesProblem(400)
                .WithTags("Unidades do Produtos")
            , clearDefaults: false);
    }
}