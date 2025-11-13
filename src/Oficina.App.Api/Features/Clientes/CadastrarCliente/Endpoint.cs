using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Oficina.App.Api.Features.Clientes.CadastrarCliente;

public class Endpoint(
    IUseCase<CadastrarClienteRequest, CadastrarClienteResponse> useCase)
    : ResultBaseEndpoint<CadastrarClienteRequest, CadastrarClienteResponse>(useCase)
{
    public override void Configure()
    {
        Post("v1/clientes");
        PreProcessor<AuthInterceptor<CadastrarClienteRequest>>();
        Description(c => c.Accepts<CadastrarClienteRequest>()
                .Produces<CadastrarClienteResponse>()
                .ProducesProblem(400)
                .WithTags("Clientes")
            , clearDefaults: false);
    }
}