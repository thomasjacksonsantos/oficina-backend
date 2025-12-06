using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Oficina.Domain.ValueObjects;

namespace Oficina.App.Api.Features.MarcasProdutos.GetMarcasProdutos;

public class Endpoint(
    IUseCase<GetMarcasProdutosRequest, PagedResult<GetMarcasProdutosResponse>> useCase)
    : ResultBaseEndpoint<GetMarcasProdutosRequest, PagedResult<GetMarcasProdutosResponse>>(useCase)
{
    public override void Configure()
    {
        Get("v1/produtos/marcas-produtos/all");
        PreProcessor<AuthInterceptor<GetMarcasProdutosRequest>>();
        Description(c => c.Accepts<GetMarcasProdutosRequest>()
                .Produces<GetMarcasProdutosResponse>()
                .ProducesProblem(400)
                .WithTags("Marcas de Produtos")
            , clearDefaults: false);
    }
}