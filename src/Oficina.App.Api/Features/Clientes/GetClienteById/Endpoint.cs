using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Oficina.App.Api.Features.Clientes.GetClienteById;

public class Endpoint(
    IUseCase<GetClienteByIdRequest, GetClienteByIdResponse> useCase)
    : ResultBaseEndpoint<GetClienteByIdRequest, GetClienteByIdResponse>(useCase)
{
    public override void Configure()
    {
        Get("v1/cliente/{id}");
        PreProcessor<AuthInterceptor<GetClienteByIdRequest>>();
        Description(c => c.Accepts<GetClienteByIdRequest>()
                .Produces<GetClienteByIdResponse>()
                .ProducesProblem(400)
                .WithTags("Clientes")
            , clearDefaults: false);
    }
}