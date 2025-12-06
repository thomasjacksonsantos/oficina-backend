

using Microsoft.AspNetCore.Mvc;
using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.AreasProdutos.GetAreaProdutoById;

public sealed record GetAreaProdutoByIdRequest(
    [FromRoute] string Id
) : AuthRequest;