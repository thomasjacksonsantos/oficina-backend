

using Microsoft.AspNetCore.Mvc;
using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.UnidadesProdutos.AtualizarUnidadeProduto;

public sealed record AtualizarUnidadeProdutoRequest(
    [FromRoute] string Id,
    string Descricao
) : AuthRequest;