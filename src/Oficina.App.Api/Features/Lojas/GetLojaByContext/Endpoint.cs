using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Oficina.App.Api.Features.Lojas.GetLojaByContext;

public class Endpoint(
    IUseCase<GetLojaByContextRequest, GetLojaByContextResponse> useCase,
    IAuthProvider authProvider
)
    : ResultBaseEndpointWithoutRequest<GetLojaByContextRequest, GetLojaByContextResponse>(useCase, authProvider)
{
    public override void Configure()
    {
        Get("v1/lojas/context");
        Description(c => c.Accepts<GetLojaByContextRequest>()
                .Produces<GetLojaByContextResponse>()
                .ProducesProblem(400)
                .WithTags("Lojas")
            , clearDefaults: false);
    }
}