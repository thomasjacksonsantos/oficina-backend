using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Oficina.App.Api.Features.UnidadesProdutos.AtualizarUnidadeProduto;

public class Endpoint(
    IUseCase<AtualizarUnidadeProdutoRequest, AtualizarUnidadeProdutoResponse> useCase)
    : ResultBaseEndpoint<AtualizarUnidadeProdutoRequest, AtualizarUnidadeProdutoResponse>(useCase)
{
    public override void Configure()
    {
        Put("v1/produtos/unidades-produtos");
        PreProcessor<AuthInterceptor<AtualizarUnidadeProdutoRequest>>();
        Description(c => c.Accepts<AtualizarUnidadeProdutoRequest>()
                .Produces<AtualizarUnidadeProdutoResponse>()
                .ProducesProblem(400)
                .WithTags("Unidades do Produtos")
            , clearDefaults: false);
    }
}