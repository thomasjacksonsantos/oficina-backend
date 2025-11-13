using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Oficina.App.Api.Features.Veiculos.CadastrarVeiculo;

public class Endpoint(
    IUseCase<CadastrarVeiculoRequest, CadastrarVeiculoResponse> useCase)
    : ResultBaseEndpoint<CadastrarVeiculoRequest, CadastrarVeiculoResponse>(useCase)
{
    public override void Configure()
    {
        Post("v1/veiculos");
        PreProcessor<AuthInterceptor<CadastrarVeiculoRequest>>();
        Description(c => c.Accepts<CadastrarVeiculoRequest>()
                .Produces<CadastrarVeiculoResponse>()
                .ProducesProblem(400)
                .WithTags("Veículos")
            , clearDefaults: false);
    }
}