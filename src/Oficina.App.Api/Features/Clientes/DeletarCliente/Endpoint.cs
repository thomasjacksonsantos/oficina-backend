using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Oficina.App.Api.Features.Clientes.DeletarCliente;

public class Endpoint(
    IUseCase<DeletarClienteRequest, DeletarClienteResponse> useCase)
    : ResultBaseEndpoint<DeletarClienteRequest, DeletarClienteResponse>(useCase)
{
    public override void Configure()
    {
        Delete("v1/clientes/{id}");
        PreProcessor<AuthInterceptor<DeletarClienteRequest>>();
        Description(c => c.Accepts<DeletarClienteRequest>()
                .Produces<DeletarClienteResponse>()
                .ProducesProblem(400)
                .WithTags("Clientes")
            , clearDefaults: false);
    }
}