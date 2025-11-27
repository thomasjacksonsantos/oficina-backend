using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Oficina.App.Api.Features.Veiculos.DesativarVeiculo;

public class Endpoint(
    IUseCase<DesativarVeiculoRequest, DesativarVeiculoResponse> useCase)
    : ResultBaseEndpoint<DesativarVeiculoRequest, DesativarVeiculoResponse>(useCase)
{
    public override void Configure()
    {
        Delete("v1/veiculos/{id}");
        PreProcessor<AuthInterceptor<DesativarVeiculoRequest>>();
        Description(c => c.Accepts<DesativarVeiculoRequest>()
                .Produces<DesativarVeiculoResponse>()
                .ProducesProblem(400)
                .WithTags("Veículos")
            , clearDefaults: false);
    }
}