

using Microsoft.AspNetCore.Mvc;
using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.GruposProdutos.GetGrupoProdutoById;

public sealed record GetGrupoProdutoByIdRequest(
    [FromRoute] string Id
) : AuthRequest;