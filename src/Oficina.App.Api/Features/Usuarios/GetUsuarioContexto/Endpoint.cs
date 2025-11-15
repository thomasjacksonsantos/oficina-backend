using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Oficina.App.Api.Features.Usuarios.GetUsuarioContexto;

public class Endpoint(
    IUseCase<GetUsuarioContextoRequest, GetUsuarioContextoResponse> useCase)
    : ResultBaseEndpoint<GetUsuarioContextoRequest, GetUsuarioContextoResponse>(useCase)
{
    public override void Configure()
    {
        Get("v1/usuarios/contexto");
        PreProcessor<AuthInterceptor<GetUsuarioContextoRequest>>();
        Description(c => c.Accepts<GetUsuarioContextoRequest>()
                .Produces<GetUsuarioContextoResponse>()
                .ProducesProblem(400)
                .WithTags("Usuários")
            , clearDefaults: false);
    }
}