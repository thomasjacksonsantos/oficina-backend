using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Oficina.App.Api.Features.Usuarios.UpsertUsuario;

public class Endpoint(
    IUseCase<UpsertUsuarioRequest, UpsertUsuarioResponse> useCase)
    : ResultBaseEndpoint<UpsertUsuarioRequest, UpsertUsuarioResponse>(useCase)
{
    public override void Configure()
    {
        Post("v1/usuario");
        Description(c => c.Accepts<UpsertUsuarioRequest>()
                .Produces<UpsertUsuarioResponse>()
                .ProducesProblem(400)
                .WithTags("Usuarios")
            , clearDefaults: false);
    }
}