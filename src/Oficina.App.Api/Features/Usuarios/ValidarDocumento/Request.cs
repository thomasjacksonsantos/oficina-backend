

using Microsoft.AspNetCore.Mvc;
using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.Usuarios.ValidarDocumento;

public sealed record UpsertUsuarioRequest(
    [FromRoute] string Documento
) : AuthRequest;