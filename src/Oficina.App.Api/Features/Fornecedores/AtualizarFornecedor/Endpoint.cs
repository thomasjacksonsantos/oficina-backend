using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Oficina.App.Api.Features.Fornecedores.AtualizarFornecedor;

public class Endpoint(
    IUseCase<AtualizarFornecedorRequest, AtualizarFornecedorResponse> useCase)
    : ResultBaseEndpoint<AtualizarFornecedorRequest, AtualizarFornecedorResponse>(useCase)
{
    public override void Configure()
    {
        Put("v1/fornecedores/edit/{id}");
        PreProcessor<AuthInterceptor<AtualizarFornecedorRequest>>();
        Description(c => c.Accepts<AtualizarFornecedorRequest>()
                .Produces<AtualizarFornecedorResponse>()
                .ProducesProblem(400)
                .WithTags("Fornecedores")
            , clearDefaults: false);
    }
}