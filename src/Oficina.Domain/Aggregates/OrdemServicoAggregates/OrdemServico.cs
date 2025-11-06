using Oficina.Domain.Aggregates.UsuarioAggregates;
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
    public int FuncionarioId { get; private set; }
    public Funcionario? Funcionario { get; private set; }
    public int VeiculoClienteId { get; private set; }
    public VeiculoCliente VeiculoCliente { get; private set; }
    public ICollection<OrdemServicoItem>? Itens { get; private set; }
    public OrdemServicoPagamento? Pagamento { get; private set; }
    public DataHora Criado { get; private set; }
    public DataHora Atualizado { get; private set; }

    // Construtor privado sem parâmetro
#pragma warning disable CS8618
    private OrdemServico() { }
#pragma warning restore CS8618

    // Construtor com parâmetros
    private OrdemServico(
        DateTime dataFaturamentoInicial,
        string observacao,
        int funcionarioId,
        VeiculoCliente veiculoCliente
    )
    {
        DataFaturamentoInicial = dataFaturamentoInicial;
        Observacao = observacao;
        FuncionarioId = funcionarioId;
        VeiculoClienteId = veiculoCliente.Id;
        VeiculoCliente = veiculoCliente;
        Criado = DataHora.Criar().Value!;
        Atualizado = DataHora.Criar().Value!;
    }

    public void AddItem(OrdemServicoItem item)
    {
        Itens ??= new List<OrdemServicoItem>();
        Itens.Add(item);
        ValorTotal += item.ValorUnitario * item.Quantidade;
        Atualizado = DataHora.Criar().Value!;
    }


    // Método estático Criar
    public static Result<OrdemServico> Criar(
        DateTime dataFaturamentoInicial,
        string observacao,
        int funcionarioId,
        VeiculoCliente veiculoCliente
    )
    {
        var result = new Result<OrdemServico>();

        if (veiculoCliente == null)
            result.WithError(Erro.ValorInvalido("OrdemServico.VeiculoCliente"));

        if (string.IsNullOrWhiteSpace(observacao))
            result.WithError(Erro.ValorInvalido("OrdemServico.Observacao"));

        if (dataFaturamentoInicial.Date.ToUniversalTime() < DateTime.MinValue.ToUniversalTime() ||
            dataFaturamentoInicial.Date.ToUniversalTime() > DateTime.MaxValue.ToUniversalTime())
            result.WithError(Erro.ValorInvalido("OrdemServico.DataFaturamentoInicial"));

        if (funcionarioId <= 0)
            result.WithError(Erro.ValorInvalido("OrdemServico.FuncionarioId"));

        if (result.IsFailed)
            return Result.Fail(result.Errors!);

        // Aqui você pode adicionar validações conforme necessário
        var ordemServico = new OrdemServico(
            dataFaturamentoInicial,
            observacao,
            funcionarioId,
            veiculoCliente!
        );

        return Result.Success(ordemServico);
    }
}