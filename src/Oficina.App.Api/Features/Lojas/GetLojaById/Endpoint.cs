using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Oficina.App.Api.Features.Lojas.GetLojaById;

public class Endpoint(
    IUseCase<GetLojaByIdRequest, GetLojaByIdResponse> useCase)
    : ResultBaseEndpoint<GetLojaByIdRequest, GetLojaByIdResponse>(useCase)
{
    public override void Configure()
    {
        Get("v1/lojas");
        PreProcessor<AuthInterceptor<GetLojaByIdRequest>>();
        Description(c => c.Accepts<GetLojaByIdRequest>()
                .Produces<GetLojaByIdResponse>()
                .ProducesProblem(400)
                .WithTags("Lojas")
            , clearDefaults: false);
    }
}