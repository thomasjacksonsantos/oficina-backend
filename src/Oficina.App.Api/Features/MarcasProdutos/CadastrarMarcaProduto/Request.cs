

using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.MarcasProdutos.CadastrarMarcaProduto;

public sealed record CadastrarMarcaProdutoRequest(
    string Descricao
) : AuthRequest;