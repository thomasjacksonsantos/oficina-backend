
using Oficina.Domain.Aggregates.ClienteAggregates;
using Oficina.Domain.Aggregates.OrdemServicoAggregates;
using Oficina.Domain.Aggregates.UsuarioAggregates;
using Oficina.Domain.Aggregates.VeiculoAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.App.Api.Features.OrdemDeServico.CadastrarOrdemDeServico;

public sealed class UseCase(
    IRepository<OrdemServico> ordemServicoRepository,
    IFluentQuery fluentQuery,
    IUnitOfWork unitOfWork
)
    : IUseCase<CadastrarOrdemDeServicoRequest, CadastrarOrdemDeServicoResponse>
{
    public async Task<Result<CadastrarOrdemDeServicoResponse>> Execute(
        CadastrarOrdemDeServicoRequest input,
        CancellationToken ct = default
    )
    {

        var cliente = await fluentQuery.For<Cliente>()
            .WithPredicate(x => x.Id == input.ClienteId)
            .FindFirstAsync();

        var veiculo = await fluentQuery.For<Veiculo>()
            .WithPredicate(x => x.Id == input.VeiculoId)
            .FindFirstAsync();

        var funcionario = await fluentQuery.For<Funcionario>()
            .WithPredicate(x => x.Id == input.FuncionarioExecutorId)
            .FindFirstAsync();

        var veiculoCliente = await fluentQuery.For<VeiculoCliente>()
            .WithPredicate(x => x.Cliente.Id == cliente!.Id && x.Veiculo.Id == veiculo!.Id)
            .FindFirstAsync();

        if (veiculoCliente == null)
        {
            var veiculoClienteResult = VeiculoCliente.Criar(
                veiculo!,
                cliente!
            );

            if (veiculoClienteResult.IsFailed)
                return Result.Fail(veiculoClienteResult.Errors!);

            veiculoCliente = veiculoClienteResult.Value!;        
        }

        var ordemServico = OrdemServico.Criar(
            input.DataFaturamentoInicial,
            input.DataPrevisao,
            input.Observacao,
            funcionario!.Id,
            veiculoCliente
        );

        if (ordemServico.IsFailed)
            return Result.Fail(ordemServico.Errors!);

        await ordemServicoRepository.AddAsync(ordemServico.Value!);
        await unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
