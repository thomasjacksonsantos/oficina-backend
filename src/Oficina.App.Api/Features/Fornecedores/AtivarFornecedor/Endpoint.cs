using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Oficina.App.Api.Features.Fornecedores.AtivarFornecedor;

public class Endpoint(
    IUseCase<AtivarFornecedorRequest, AtivarFornecedorResponse> useCase)
    : ResultBaseEndpoint<AtivarFornecedorRequest, AtivarFornecedorResponse>(useCase)
{
    public override void Configure()
    {
        Put("v1/fornecedores/{id}/ativar");
        PreProcessor<AuthInterceptor<AtivarFornecedorRequest>>();
        Description(c => c.Accepts<AtivarFornecedorRequest>()
                .Produces<AtivarFornecedorResponse>()
                .ProducesProblem(400)
                .WithTags("Fornecedores")
            , clearDefaults: false);
    }
}