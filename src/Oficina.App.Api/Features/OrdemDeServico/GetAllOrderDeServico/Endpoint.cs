using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Oficina.App.Api.Features.OrdemDeServico.GetAllOrdemDeServico;

public class Endpoint(
    IUseCase<GetAllOrdemDeServicoRequest, GetAllOrdemDeServicoResponse> useCase)
    : ResultBaseEndpoint<GetAllOrdemDeServicoRequest, GetAllOrdemDeServicoResponse>(useCase)
{
    public override void Configure()
    {
        Post("v1/ordem-servicos");
        PreProcessor<AuthInterceptor<GetAllOrdemDeServicoRequest>>();
        Description(c => c.Accepts<GetAllOrdemDeServicoRequest>()
                .Produces<GetAllOrdemDeServicoResponse>()
                .ProducesProblem(400)
                .WithTags("Ordem de Serviços")
            , clearDefaults: false);
    }
}