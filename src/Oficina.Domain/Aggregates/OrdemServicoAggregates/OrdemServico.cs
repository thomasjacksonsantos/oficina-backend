using Oficina.Domain.Aggregates.VeiculoAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Domain.ValueObjects;

namespace Oficina.Domain.Aggregates.OrdemServicoAggregates;

public class OrdemServico
{
    public int Id { get; private set; }
    public decimal ValorTotal { get; private set; }
    public DateTime DataFaturamentoInicial { get; private set; }
    public DateTime DataFaturamentoFinal { get; private set; }
    public string Observacao { get; private set; }
    public int FuncionarioExecutorId { get; private set; }
    public FuncionarioExecutor FuncionarioExecutor { get; private set; }
    public int VeiculoClienteId { get; private set; }
    public VeiculoCliente VeiculoCliente { get; private set; }
    public ICollection<ProdutoServicoItem>? Items { get; private set; }
    public OrdemServicoPagamento? Pagamento { get; private set; }
    public DataHora Criado { get; private set; }
    public DataHora Atualizado { get; private set; }

    // Construtor privado sem parâmetro
#pragma warning disable CS8618
    private OrdemServico() { }
#pragma warning restore CS8618

    // Construtor com parâmetros
    public OrdemServico(
        DateTime dataFaturamentoInicial,
        string observacao,
        FuncionarioExecutor funcionarioExecutor,
        VeiculoCliente veiculoCliente
    )
    {
        DataFaturamentoInicial = dataFaturamentoInicial;
        Observacao = observacao;
        FuncionarioExecutorId = funcionarioExecutor.Id;
        FuncionarioExecutor = funcionarioExecutor;
        VeiculoClienteId = veiculoCliente.Id;
        VeiculoCliente = veiculoCliente;
        Criado = DataHora.Criar().Value!;
        Atualizado = DataHora.Criar().Value!;
    }

    // Método estático Criar
    public static Result<OrdemServico> Criar(
        DateTime dataFaturamentoInicial,
        string observacao,
        FuncionarioExecutor funcionarioExecutor,
        VeiculoCliente veiculoCliente
    )
    {
        // Aqui você pode adicionar validações conforme necessário
        var ordemServico = new OrdemServico(
            dataFaturamentoInicial,
            observacao,
            funcionarioExecutor,
            veiculoCliente
        );

        return Result.Success(ordemServico);
    }
}