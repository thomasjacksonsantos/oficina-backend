using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Oficina.Domain.ValueObjects;

namespace Oficina.App.Api.Features.Fornecedores.GetFornecedores;

public class Endpoint(
    IUseCase<GetFornecedoresRequest, PagedResult<GetFornecedoresResponse>> useCase)
    : ResultBaseEndpoint<GetFornecedoresRequest, PagedResult<GetFornecedoresResponse>>(useCase)
{
    public override void Configure()
    {
        Get("v1/fornecedores/all");
        PreProcessor<AuthInterceptor<GetFornecedoresRequest>>();
        Description(c => c.Accepts<GetFornecedoresRequest>()
                .Produces<GetFornecedoresResponse>()
                .ProducesProblem(400)
                .WithTags("Fornecedores")
            , clearDefaults: false);
    }
}