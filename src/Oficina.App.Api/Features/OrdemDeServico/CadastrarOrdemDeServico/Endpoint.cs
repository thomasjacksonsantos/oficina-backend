using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Oficina.App.Api.Features.OrdemDeServico.CadastrarOrdemDeServico;

public class Endpoint(
    IUseCase<CadastrarOrdemDeServicoRequest, CadastrarOrdemDeServicoResponse> useCase)
    : ResultBaseEndpoint<CadastrarOrdemDeServicoRequest, CadastrarOrdemDeServicoResponse>(useCase)
{
    public override void Configure()
    {
        Post("v1/order-servico");
        PreProcessor<AuthInterceptor<CadastrarOrdemDeServicoRequest>>();
        Description(c => c.Accepts<CadastrarOrdemDeServicoRequest>()
                .Produces<CadastrarOrdemDeServicoResponse>()
                .ProducesProblem(400)
                .WithTags("Ordem de Serviços")
            , clearDefaults: false);
    }
}