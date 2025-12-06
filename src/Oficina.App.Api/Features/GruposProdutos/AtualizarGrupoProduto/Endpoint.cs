using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Oficina.App.Api.Features.GruposProdutos.AtualizarGrupoProduto;

public class Endpoint(
    IUseCase<AtualizarGrupoProdutoRequest, AtualizarGrupoProdutoResponse> useCase)
    : ResultBaseEndpoint<AtualizarGrupoProdutoRequest, AtualizarGrupoProdutoResponse>(useCase)
{
    public override void Configure()
    {
        Put("v1/produtos/grupos-produtos");
        PreProcessor<AuthInterceptor<AtualizarGrupoProdutoRequest>>();
        Description(c => c.Accepts<AtualizarGrupoProdutoRequest>()
                .Produces<AtualizarGrupoProdutoResponse>()
                .ProducesProblem(400)
                .WithTags("Grupos de Produtos")
            , clearDefaults: false);
    }
}