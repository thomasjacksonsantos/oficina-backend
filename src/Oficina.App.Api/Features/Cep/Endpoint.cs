using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;

namespace Oficina.App.Api.Features.Cep;

public class Endpoint(IUseCase<CepRequest, CepResponse> useCase)
    : ResultBaseEndpoint<CepRequest, CepResponse>(useCase)
{
    public override void Configure()
    {
        Get("v1/cep/{cep}");
        PreProcessor<AuthInterceptor<CepRequest>>();
        Description(c => c.Accepts<CepRequest>()
            .Produces<CepResponse>()
            .ProducesProblem(400)
            .WithTags("Cep")
            , clearDefaults: false);
    }
}