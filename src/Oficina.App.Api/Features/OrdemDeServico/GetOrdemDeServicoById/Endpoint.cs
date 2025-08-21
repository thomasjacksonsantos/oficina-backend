using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Oficina.App.Api.Features.OrdemDeServico.GetOrdemDeServicoById;

public class Endpoint(
    IUseCase<GetOrdemDeServicoByIdRequest, GetOrdemDeServicoByIdResponse> useCase)
    : ResultBaseEndpoint<GetOrdemDeServicoByIdRequest, GetOrdemDeServicoByIdResponse>(useCase)
{
    public override void Configure()
    {
        Get("v1/order-servico/{id}");
        PreProcessor<AuthInterceptor<GetOrdemDeServicoByIdRequest>>();
        Description(c => c.Accepts<GetOrdemDeServicoByIdRequest>()
                .Produces<GetOrdemDeServicoByIdResponse>()
                .ProducesProblem(400)
                .WithTags("Ordem de Serviços")
            , clearDefaults: false);
    }
}