

using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.UnidadesProdutos.CadastrarUnidadeProduto;

public sealed record CadastrarUnidadeProdutoRequest(
    string Descricao
) : AuthRequest;