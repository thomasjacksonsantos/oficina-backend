using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Oficina.App.Api.Features.Usuarios.ValidarDocumento;

public class Endpoint(
    IUseCase<UpsertUsuarioRequest, UpsertUsuarioResponse> useCase)
    : ResultBaseEndpoint<UpsertUsuarioRequest, UpsertUsuarioResponse>(useCase)
{
    public override void Configure()
    {
        Post("v1/usuarios/validar-documento/{documento}");
        PreProcessor<AuthInterceptor<UpsertUsuarioRequest>>();
        Description(c => c.Accepts<UpsertUsuarioRequest>()
                .Produces<UpsertUsuarioResponse>()
                .ProducesProblem(400)
                .WithTags("Usuários")
            , clearDefaults: false);
    }
}