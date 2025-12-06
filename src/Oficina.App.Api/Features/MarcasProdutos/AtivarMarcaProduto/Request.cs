
using Microsoft.AspNetCore.Mvc;
using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.MarcasProdutos.AtivarMarcaProduto;

public sealed record AtivarMarcaProdutoRequest(
    [FromRoute] string Id
) : AuthRequest;