using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Oficina.App.Api.Features.Clientes.AtualizarCliente;

public class Endpoint(
    IUseCase<AtualizarClienteRequest, AtualizarClienteResponse> useCase)
    : ResultBaseEndpoint<AtualizarClienteRequest, AtualizarClienteResponse>(useCase)
{
    public override void Configure()
    {
        Put("v1/cliente");
        PreProcessor<AuthInterceptor<AtualizarClienteRequest>>();
        Description(c => c.Accepts<AtualizarClienteRequest>()
                .Produces<AtualizarClienteResponse>()
                .ProducesProblem(400)
                .WithTags("Clientes")
            , clearDefaults: false);
    }
}