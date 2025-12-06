
using Microsoft.AspNetCore.Mvc;
using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.GruposProdutos.AtivarGrupoProduto;

public sealed record AtivarGrupoProdutoRequest(
    [FromRoute] string Id
) : AuthRequest;