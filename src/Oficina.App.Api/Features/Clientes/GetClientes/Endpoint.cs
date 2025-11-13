using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Oficina.Domain.ValueObjects;

namespace Oficina.App.Api.Features.Clientes.GetClientes;

public class Endpoint(
    IUseCase<GetClientesRequest, PagedResult<GetClientesResponse>> useCase)
    : ResultBaseEndpoint<GetClientesRequest, PagedResult<GetClientesResponse>>(useCase)
{
    public override void Configure()
    {
        Get("v1/clientes");
        PreProcessor<AuthInterceptor<GetClientesRequest>>();
        Description(c => c.Accepts<GetClientesRequest>()
                .Produces<PagedResult<GetClientesResponse>>()
                .ProducesProblem(400)
                .WithTags("Clientes")
            , clearDefaults: false);
    }
}