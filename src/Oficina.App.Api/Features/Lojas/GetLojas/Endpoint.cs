using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Oficina.App.Api.Features.Lojas.GetLojas;

public class Endpoint(
    IUseCase<GetLojasRequest, IEnumerable<GetLojasResponse>> useCase)
    : ResultBaseEndpoint<GetLojasRequest, IEnumerable<GetLojasResponse>>(useCase)
{
    public override void Configure()
    {
        Get("v1/lojas/conta/{contaId}");
        PreProcessor<AuthInterceptor<GetLojasRequest>>();
        Description(c => c.Accepts<GetLojasRequest>()
                .Produces<GetLojasResponse>()
                .ProducesProblem(400)
                .WithTags("Lojas")
            , clearDefaults: false);
    }
}