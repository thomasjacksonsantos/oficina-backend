
using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.OrdemDeServico.CadastrarOrdemDeServico;

public sealed record CadastrarOrdemDeServicoRequest(
    DateTime DataFaturamentoInicial,
    DateTime? DataPrevisao,
    string Observacao,
    int FuncionarioExecutorId,
    int ClienteId,
    int VeiculoId
) : AuthRequest;