

using Microsoft.AspNetCore.Mvc;
using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.UnidadesProdutos.GetUnidadeProdutoById;

public sealed record GetUnidadeProdutoByIdRequest(
    [FromRoute] string Id
) : AuthRequest;