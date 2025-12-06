

using Microsoft.AspNetCore.Mvc;
using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.MarcasProdutos.GetMarcaProdutoById;

public sealed record GetMarcaProdutoByIdRequest(
    [FromRoute] string Id
) : AuthRequest;