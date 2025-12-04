using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Oficina.App.Api.Features.Fornecedores.GetFornecedorById;

public class Endpoint(
    IUseCase<GetFornecedorByIdRequest, GetFornecedorByIdResponse> useCase)
    : ResultBaseEndpoint<GetFornecedorByIdRequest, GetFornecedorByIdResponse>(useCase)
{
    public override void Configure()
    {
        Get("v1/fornecedores/{id}");
        PreProcessor<AuthInterceptor<GetFornecedorByIdRequest>>();
        Description(c => c.Accepts<GetFornecedorByIdRequest>()
                .Produces<GetFornecedorByIdResponse>()
                .ProducesProblem(400)
                .WithTags("Fornecedores")
            , clearDefaults: false);
    }
}