using Oficina.Domain.Aggregates.OrdemServicoAggregates;
using Oficina.Domain.Aggregates.VeiculoAggregates;
using Oficina.Domain.Aggregates.ClienteAggregates;
using Oficina.Domain.ValueObjects;
using Oficina.Domain.SeedWork;
using Oficina.Domain.Aggregates.UsuarioAggregates;

namespace Oficina.Test.Unit.OrdemServicos;

public class OrdemServicoTest
{
    [Fact]
    public void Criar_DeveRetornarOrdemServicoValida()
    {
        var veiculo = Veiculo.Criar(
           "ABC-1234",
           "Modelo X",
            "Montadora Y",
            10000,
           "Preto",
           2025,
           "12323aas2",
           "2.0",
           "213213123"
       ).Value!;

        var cliente = Cliente.Criar(
            "João Silva",
            "Razao Social Exemplo",
            "masculino",
            "31435600886",
            "email@exemplo.com",
            DateTime.Parse("1990-01-01"),
            new List<Contato>
            {
                Contato.Criar("19123456789", "telefone").Value!
            },
            Endereco.Criar("Rua A", "100", "Bairro B", "Cidade C", "Estado D", "CEP12345", "403").Value!
        ).Value!;

        var clienteVeiculo = VeiculoCliente.Criar(
            veiculo,
            cliente
        ).Value!;

        var dataFaturamentoInicial = DateTime.Now;

        var result = OrdemServico.Criar(
            dataFaturamentoInicial,
            dataFaturamentoInicial.AddDays(7),
            "Observação",
            1,
            clienteVeiculo
        );

        Assert.True(result.IsSuccess);
        Assert.NotEmpty(result.Value!.Observacao);
        Assert.Equal(1, result.Value!.FuncionarioId);
        Assert.Equal(clienteVeiculo.Id, result.Value!.VeiculoClienteId);
        Assert.Equal(0, result.Value!.Id);
        Assert.Equal(0, result.Value!.ValorTotal);
        Assert.Equal(dataFaturamentoInicial, result.Value!.DataFaturamentoInicial);
        Assert.Equal(DateTime.MinValue, result.Value!.DataFaturamentoFinal);
        Assert.Null(result.Value!.Funcionario);
        Assert.NotNull(result.Value!.VeiculoCliente);
        Assert.Null(result.Value!.Itens);
        Assert.Null(result.Value!.Pagamento);
        Assert.NotNull(result.Value!.Criado);
        Assert.NotNull(result.Value!.Atualizado);
    }

    [Fact]
    public void Criar_DeveRetornarErro_QuandoVeiculoClienteForNulo()
    {
        var result = OrdemServico.Criar(DateTime.Now, null, "Observação", 1, null!);

        Assert.False(result.IsSuccess);
        Assert.Contains(result.Errors!, c => c.Descricao == "Valor informado para o OrdemServico.VeiculoCliente está inválido");
    }

    [Fact]
    public void Criar_DeveRetornarErro_QuandoFuncionarioIdForInvalido()
    {
        var funcionarioIdInvalido = 0;

        var veiculo = Veiculo.Criar(
            "ABC-1234",
            "Modelo X",
            "Montadora Y",
            10000,
            "Preto",
            2025,
            "12323aas2",
            "2.0",
            "213213123"
        ).Value!;

        var cliente = Cliente.Criar(
            "João Silva",
            "Razao Social Exemplo",
            "masculino",
            "31435600886",
            "email@exemplo.com",
            DateTime.Parse("1990-01-01"),
            new List<Contato>
            {
                Contato.Criar("19123456789", "telefone").Value!
            },
            Endereco.Criar("Rua A", "100", "Bairro B", "Cidade C", "Estado D", "CEP12345", "403").Value!
        ).Value!;

        var clienteVeiculo = VeiculoCliente.Criar(
            veiculo,
            cliente
        ).Value!;

        var result = OrdemServico.Criar(
            DateTime.Now,
            null,
            "Observação",
            funcionarioIdInvalido,
            clienteVeiculo
        );

        Assert.False(result.IsSuccess);
        Assert.Contains(result.Errors!, c => c.Descricao == "Valor informado para o OrdemServico.FuncionarioId está inválido");
    }

    [Fact]
    public void Criar_DeveRetornarErro_QuandoDataFaturamentoForInvalido()
    {
        var veiculo = Veiculo.Criar(
            "ABC-1234",
            "Modelo X",
            "Montadora Y",
            10000,
            "Preto",
            2025,
            "12323aas2",
            "2.0",
            "213213123"
        ).Value!;
        var cliente = Cliente.Criar(
            "João Silva",
            "Razao Social Exemplo",
            "masculino",
            "31435600886",
            "email@exemplo.com",
            DateTime.Parse("1990-01-01"),
            new List<Contato>
            {
                Contato.Criar("19123456789", "telefone").Value!
            },
            Endereco.Criar("Rua A", "100", "Bairro B", "Cidade C", "Estado D", "CEP12345", "403").Value!
        ).Value!;

        var clienteVeiculo = VeiculoCliente.Criar(
            veiculo,
            cliente
        ).Value!;

        var result = OrdemServico.Criar(DateTime.MinValue, null, "Observação", 1, clienteVeiculo);

        Assert.False(result.IsSuccess);
        Assert.Contains(result.Errors!, c => c.Descricao == "Valor informado para o OrdemServico.DataFaturamentoInicial está inválido");
    }

    [Fact]
    public void Criar_DeveRetornarErro_QuandoObservacaoForInvalido()
    {
        var veiculo = Veiculo.Criar(
            "ABC-1234",
            "Modelo X",
            "Motadora Y",
            10000,
            "Preto",
            2025,
            "123213123",
            "2.0",
            "213213123"
        ).Value!;

        var cliente = Cliente.Criar(
            "João Silva",
            "Razao Social Exemplo",
            "masculino",
            "31435600886",
            "email@exemplo.com",
            DateTime.Parse("1990-01-01"),
            new List<Contato>
            {
                Contato.Criar("19123456789", "telefone").Value!
            },
            Endereco.Criar("Rua A", "100", "Bairro B", "Cidade C", "Estado D", "CEP12345", "403").Value!
        ).Value!;
        var clienteVeiculo = VeiculoCliente.Criar(
            veiculo,
            cliente
        ).Value!;
        var result = OrdemServico.Criar(DateTime.Now, null, "", 1, clienteVeiculo);

        Assert.False(result.IsSuccess);
        Assert.Contains(result.Errors!, c => c.Descricao == "Valor informado para o OrdemServico.Observacao está inválido");
    }

    [Fact]
    public void Criar_DeveRetornarErro_QuandoDataFaturamentoInicialForPassado()
    {
        var veiculo = Veiculo.Criar(
            "ABC-1234",
            "Modelo X",
            "Montadora Y",
            10000,
            "Preto",
            2025,
            "12323aas2",
            "2.0",
            "213213123"
        ).Value!;
        var cliente = Cliente.Criar(
            "João Silva",
            "Razao Social Exemplo",
            "masculino",
            "31435600886",
            "email@exemplo.com",
            DateTime.Parse("1990-01-01"),
            new List<Contato>
            {
                Contato.Criar("19123456789", "telefone").Value!
            },
            Endereco.Criar("Rua A", "100", "Bairro B", "Cidade C", "Estado D", "CEP12345", "403").Value!
        ).Value!;

        var clienteVeiculo = VeiculoCliente.Criar(
            veiculo,
            cliente
        ).Value!;

        var result = OrdemServico.Criar(
            DateTime.UtcNow.AddDays(-1),
            null,
            "Observação",
            1,
            clienteVeiculo
        );

        Assert.False(result.IsSuccess);
        Assert.Contains(result.Errors!, e => e.Descricao == Erro.ValorInvalido("OrdemServico.DataFaturamentoInicial").Descricao);
    }
}