using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Oficina.App.Api.Features.Fornecedores.DesativarFornecedor;

public class Endpoint(
    IUseCase<DesativarFornecedorRequest, DesativarFornecedorResponse> useCase)
    : ResultBaseEndpoint<DesativarFornecedorRequest, DesativarFornecedorResponse>(useCase)
{
    public override void Configure()
    {
        Delete("v1/fornecedores/{id}/desativar");
        PreProcessor<AuthInterceptor<DesativarFornecedorRequest>>();
        Description(c => c.Accepts<DesativarFornecedorRequest>()
                .Produces<DesativarFornecedorResponse>()
                .ProducesProblem(400)
                .WithTags("Fornecedores")
            , clearDefaults: false);
    }
}