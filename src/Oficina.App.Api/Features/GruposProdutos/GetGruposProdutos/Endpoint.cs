using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Oficina.Domain.ValueObjects;

namespace Oficina.App.Api.Features.GruposProdutos.GetGruposProdutos;

public class Endpoint(
    IUseCase<GetGruposProdutosRequest, PagedResult<GetGruposProdutosResponse>> useCase)
    : ResultBaseEndpoint<GetGruposProdutosRequest, PagedResult<GetGruposProdutosResponse>>(useCase)
{
    public override void Configure()
    {
        Get("v1/produtos/grupos-produtos/all");
        PreProcessor<AuthInterceptor<GetGruposProdutosRequest>>();
        Description(c => c.Accepts<GetGruposProdutosRequest>()
                .Produces<GetGruposProdutosResponse>()
                .ProducesProblem(400)
                .WithTags("Grupos de Produtos")
            , clearDefaults: false);
    }
}