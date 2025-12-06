

using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.AreasProdutos.CadastrarAreaProduto;

public sealed record CadastrarAreaProdutoRequest(
    string Descricao,
    string Garantia
) : AuthRequest;