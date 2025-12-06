

using Microsoft.AspNetCore.Mvc;
using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.MarcasProdutos.AtualizarMarcaProduto;

public sealed record AtualizarMarcaProdutoRequest(
    [FromRoute] string Id,
    string Descricao
) : AuthRequest;