using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Oficina.Domain.ValueObjects;

namespace Oficina.App.Api.Features.OrdemDeServico.GetAllOrdemDeServico;

public class Endpoint(
    IUseCase<GetAllOrdemDeServicoRequest, PagedResult<GetAllOrdemDeServicoResponse>> useCase)
    : ResultBaseEndpoint<GetAllOrdemDeServicoRequest, PagedResult<GetAllOrdemDeServicoResponse>>(useCase)
{
    public override void Configure()
    {
        Get("v1/ordem-servicos");
        PreProcessor<AuthInterceptor<GetAllOrdemDeServicoRequest>>();
        Description(c => c.Accepts<GetAllOrdemDeServicoRequest>()
                .Produces<PagedResult<GetAllOrdemDeServicoResponse>>()
                .ProducesProblem(400)
                .WithTags("Ordem de Serviços")
            , clearDefaults: false);
    }
}