

namespace Oficina.App.Api.Features.OrdemDeServico.GetAllOrdemDeServico;


public sealed record GetAllOrdemDeServicoResponse(
    int Id,
    DateTime DataInicio,
    DateTime DataFim,
    string Veiculo,
    string Placa,
    string Documento,
    string Cliente,
    string Responsalve
);