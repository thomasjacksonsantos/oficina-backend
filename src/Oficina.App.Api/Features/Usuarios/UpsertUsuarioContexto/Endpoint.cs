using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Oficina.App.Api.Features.Usuarios.UpsertUsuarioContexto;

public class Endpoint(
    IUseCase<UpsertUsuarioContextoRequest, UpsertUsuarioContextoResponse> useCase)
    : ResultBaseEndpoint<UpsertUsuarioContextoRequest, UpsertUsuarioContextoResponse>(useCase)
{
    public override void Configure()
    {
        Put("v1/usuarios/contexto");
        PreProcessor<AuthInterceptor<UpsertUsuarioContextoRequest>>();
        Description(c => c.Accepts<UpsertUsuarioContextoRequest>()
                .Produces<UpsertUsuarioContextoResponse>()
                .ProducesProblem(400)
                .WithTags("Usuários")
            , clearDefaults: false);
    }
}