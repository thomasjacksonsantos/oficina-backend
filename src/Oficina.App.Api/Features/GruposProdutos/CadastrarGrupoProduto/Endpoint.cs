using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Oficina.App.Api.Features.GruposProdutos.CadastrarGrupoProduto;

public class Endpoint(
    IUseCase<CadastrarGrupoProdutoRequest, CadastrarGrupoProdutoResponse> useCase)
    : ResultBaseEndpoint<CadastrarGrupoProdutoRequest, CadastrarGrupoProdutoResponse>(useCase)
{
    public override void Configure()
    {
        Post("v1/produtos/grupos-produtos");
        PreProcessor<AuthInterceptor<CadastrarGrupoProdutoRequest>>();
        Description(c => c.Accepts<CadastrarGrupoProdutoRequest>()
                .Produces<CadastrarGrupoProdutoResponse>()
                .ProducesProblem(400)
                .WithTags("Grupos de Produtos")
            , clearDefaults: false);
    }
}