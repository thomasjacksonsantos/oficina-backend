
using Microsoft.AspNetCore.Mvc;
using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.Lojas.GetLojas;

public record GetLojasRequest(
    [FromRoute] string ContaId
) : AuthRequest;