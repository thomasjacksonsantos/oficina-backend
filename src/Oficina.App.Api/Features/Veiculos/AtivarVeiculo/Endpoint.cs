using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Oficina.App.Api.Features.Veiculos.AtivarVeiculo;

public class Endpoint(
    IUseCase<AtivarVeiculoRequest, AtivarVeiculoResponse> useCase)
    : ResultBaseEndpoint<AtivarVeiculoRequest, AtivarVeiculoResponse>(useCase)
{
    public override void Configure()
    {
        Put("v1/veiculos/{id}/ativar");
        PreProcessor<AuthInterceptor<AtivarVeiculoRequest>>();
        Description(c => c.Accepts<AtivarVeiculoRequest>()
                .Produces<AtivarVeiculoResponse>()
                .ProducesProblem(400)
                .WithTags("Veículos")
            , clearDefaults: false);
    }
}