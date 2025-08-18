
using Oficina.Domain.Aggregates.ClienteAggregates;
using Oficina.Domain.Aggregates.OrdemServicoAggregates;
using Oficina.Domain.Aggregates.UsuarioAggregates;
using Oficina.Domain.Aggregates.VeiculoAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.App.Api.Features.OrdemDeServico.CadastrarOrdemDeServico;

public sealed class UseCase(
    IRepository<Cliente> clienteRepository,
    IRepository<Veiculo> veiculoRepository,
    IRepository<Usuario> usuarioRepository,
    IRepository<OrdemServico> ordemServicoRepository,
    IUnitOfWork unitOfWork
)
    : IUseCase<CadastrarOrdemDeServicoRequest, CadastrarOrdemDeServicoResponse>
{
    public async Task<Result<CadastrarOrdemDeServicoResponse>> Execute(
        CadastrarOrdemDeServicoRequest input,
        CancellationToken ct = default
    )
    {
        var cliente = await clienteRepository.FindAsync(input.ClienteId);
        var veiculo = await veiculoRepository.FindAsync(input.VeiculoId);
        var usuario = await usuarioRepository.FindAsync(input.FuncionarioExecutorId);

        var veiculoCliente = VeiculoCliente.Criar(
            veiculo,
            cliente
        );

        if (veiculoCliente.IsFailed)
            return Result.Fail(veiculoCliente.Errors!);

        var funcionarioExecutor = FuncionarioExecutor.Criar(
            usuario
        );

        if (funcionarioExecutor.IsFailed)
            return Result.Fail(funcionarioExecutor.Errors!);

        var ordemServico = OrdemServico.Criar(
            input.DataFaturamentoInicial,
            input.Observacao,
            funcionarioExecutor.Value!,
            veiculoCliente.Value!
        );

        if (ordemServico.IsFailed)
            return Result.Fail(ordemServico.Errors!);

        await ordemServicoRepository.AddAsync(ordemServico.Value!);
        await unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
