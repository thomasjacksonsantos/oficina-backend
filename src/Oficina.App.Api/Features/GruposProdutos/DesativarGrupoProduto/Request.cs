
using Microsoft.AspNetCore.Mvc;
using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.GruposProdutos.DesativarGrupoProduto;

public sealed record DesativarGrupoProdutoRequest(
    [FromRoute] string Id
) : AuthRequest;