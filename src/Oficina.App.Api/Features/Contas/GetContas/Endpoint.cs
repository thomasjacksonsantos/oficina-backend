using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Oficina.App.Api.Features.Contas.GetContas;

public class Endpoint(
    IUseCase<GetContasRequest, IEnumerable<GetContasResponse>> useCase)
    : ResultBaseEndpoint<GetContasRequest, IEnumerable<GetContasResponse>>(useCase)
{
    public override void Configure()
    {
        Get("v1/contas");
        PreProcessor<AuthInterceptor<GetContasRequest>>();
        Description(c => c.Accepts<GetContasRequest>()
                .Produces<GetContasResponse>()
                .ProducesProblem(400)
                .WithTags("Contas")
            , clearDefaults: false);
    }
}