using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Oficina.App.Api.Features.Veiculos.GetVeiculoById;

public class Endpoint(
    IUseCase<GetVeiculoByIdRequest, GetVeiculoByIdResponse> useCase)
    : ResultBaseEndpoint<GetVeiculoByIdRequest, GetVeiculoByIdResponse>(useCase)
{
    public override void Configure()
    {
        Get("v1/veiculos/id/{id}");
        PreProcessor<AuthInterceptor<GetVeiculoByIdRequest>>();
        Description(c => c.Accepts<GetVeiculoByIdRequest>()
                .Produces<GetVeiculoByIdResponse>()
                .ProducesProblem(400)
                .WithTags("Veículos")
            , clearDefaults: false);
    }
}