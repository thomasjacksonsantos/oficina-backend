using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Oficina.App.Api.Features.Fornecedores.CadastrarFornecedor;

public class Endpoint(
    IUseCase<CadastrarFornecedorRequest, CadastrarFornecedorResponse> useCase)
    : ResultBaseEndpoint<CadastrarFornecedorRequest, CadastrarFornecedorResponse>(useCase)
{
    public override void Configure()
    {
        Post("v1/fornecedores");
        PreProcessor<AuthInterceptor<CadastrarFornecedorRequest>>();
        Description(c => c.Accepts<CadastrarFornecedorRequest>()
                .Produces<CadastrarFornecedorResponse>()
                .ProducesProblem(400)
                .WithTags("Fornecedores")
            , clearDefaults: false);
    }
}