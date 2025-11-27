

namespace Oficina.App.Api.Features.Contas.GetContas;

public record GetContasResponse(
    string Id,
    string Nome,
    ICollection<GetContaLojaResponse> Lojas
);

public record GetContaLojaResponse(
    string Id,
    string NomeFantasia,
    string RazaoSocial
);
