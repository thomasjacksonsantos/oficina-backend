using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Oficina.App.Api.Features.Lojas.AtualizarLoja;

public class Endpoint(
    IUseCase<AtualizarLojaRequest, AtualizarLojaResponse> useCase)
    : ResultBaseEndpoint<AtualizarLojaRequest, AtualizarLojaResponse>(useCase)
{
    public override void Configure()
    {
        Put("v1/lojas/{id}");
        AllowAnonymous();
        Description(c => c.Accepts<AtualizarLojaRequest>()
                .Produces<AtualizarLojaResponse>()
                .ProducesProblem(400)
                .WithTags("Loja")
            , clearDefaults: false);
    }
}