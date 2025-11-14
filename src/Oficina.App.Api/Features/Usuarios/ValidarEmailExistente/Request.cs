

using Microsoft.AspNetCore.Mvc;
using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.Usuarios.ValidarEmailExistente;

public sealed record ValidarEmailExistenteRequest(
    [FromRoute] string Valor
) : AuthRequest;