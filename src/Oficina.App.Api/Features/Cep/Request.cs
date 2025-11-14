
using Microsoft.AspNetCore.Mvc;
using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.Cep;

public record CepRequest(
    [FromRoute] string Cep
) : AuthRequest;