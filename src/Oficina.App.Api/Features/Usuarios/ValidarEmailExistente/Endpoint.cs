using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Oficina.App.Api.Features.Usuarios.ValidarEmailExistente;

public class Endpoint(
    IUseCase<ValidarEmailExistenteRequest, ValidarEmailExistenteResponse> useCase)
    : ResultBaseEndpoint<ValidarEmailExistenteRequest, ValidarEmailExistenteResponse>(useCase)
{
    public override void Configure()
    {
        Post("v1/usuarios/email-existente/{valor}");
        PreProcessor<AuthInterceptor<ValidarEmailExistenteRequest>>();
        Description(c => c.Accepts<ValidarEmailExistenteRequest>()
                .Produces<ValidarEmailExistenteResponse>()
                .ProducesProblem(400)
                .WithTags("Usuários")
            , clearDefaults: false);
    }
}