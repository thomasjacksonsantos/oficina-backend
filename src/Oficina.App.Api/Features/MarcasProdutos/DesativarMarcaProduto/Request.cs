
using Microsoft.AspNetCore.Mvc;
using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.MarcasProdutos.DesativarMarcaProduto;

public sealed record DesativarMarcaProdutoRequest(
    [FromRoute] string Id
) : AuthRequest;