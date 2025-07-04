using Microsoft.AspNetCore.Http;
using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.Usuarios.UpsertUsuario;

public sealed record UpsertUsuarioRequest(
    string Nome
) : AuthRequest;