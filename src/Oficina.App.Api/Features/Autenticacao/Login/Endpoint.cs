using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Oficina.App.Api.Features.Autenticacao.Login;

public class Endpoint(
    IUseCase<LoginRequest, LoginResponse> useCase)
    : ResultBaseEndpoint<LoginRequest, LoginResponse>(useCase)
{
    public override void Configure()
    {
        Post("v1/autenticacao/login");
        AllowAnonymous();
        Description(c => c.Accepts<LoginRequest>()
                .Produces<LoginResponse>()
                .ProducesProblem(400)
                .WithTags("Autenticacao")
            , clearDefaults: false);
    }
}