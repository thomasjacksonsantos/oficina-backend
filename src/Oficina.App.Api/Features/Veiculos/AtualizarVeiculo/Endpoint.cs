using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Oficina.App.Api.Features.Veiculos.AtualizarVeiculo;

public class Endpoint(
    IUseCase<AtualizarVeiculoRequest, AtualizarVeiculoResponse> useCase)
    : ResultBaseEndpoint<AtualizarVeiculoRequest, AtualizarVeiculoResponse>(useCase)
{
    public override void Configure()
    {
        Put("v1/veiculos");
        PreProcessor<AuthInterceptor<AtualizarVeiculoRequest>>();
        Description(c => c.Accepts<AtualizarVeiculoRequest>()
                .Produces<AtualizarVeiculoResponse>()
                .ProducesProblem(400)
                .WithTags("Veículos")
            , clearDefaults: false);
    }
}