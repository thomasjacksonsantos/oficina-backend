using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Oficina.Domain.ValueObjects;

namespace Oficina.App.Api.Features.Veiculos.GetVeiculos;

public class Endpoint(
    IUseCase<GetVeiculosRequest, PagedResult<GetVeiculosResponse>> useCase)
    : ResultBaseEndpoint<GetVeiculosRequest, PagedResult<GetVeiculosResponse>>(useCase)
{
    public override void Configure()
    {
        Get("v1/veiculos");
        PreProcessor<AuthInterceptor<GetVeiculosRequest>>();
        Description(c => c.Accepts<GetVeiculosRequest>()
                .Produces<GetVeiculosResponse>()
                .ProducesProblem(400)
                .WithTags("Veículoss")
            , clearDefaults: false);
    }
}