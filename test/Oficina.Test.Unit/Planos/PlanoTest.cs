using Oficina.Domain.Aggregates.PlanoAggregates;

namespace Oficina.Test.Unit.Planos;

public class PlanoTest
{
    [Fact]
    public void Criar_DeveRetornarPlanoValido_QuandoDadosValidos()
    {
        var result = Plano.Criar("Plano Ouro", 99.90m, 10);

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal("Plano Ouro", result.Value.Nome);
        Assert.Equal(99.90m, result.Value.ValorMensal);
        Assert.Equal(10, result.Value.LimiteLojas);
        Assert.Equal(PlanoStatus.Ativo.Id, result.Value.PlanoStatusId);
    }

    [Fact]
    public void Criar_DeveRetornarErro_QuandoNomeVazio()
    {
        var result = Plano.Criar("", 99.90m, 10);

        Assert.True(result.IsFailed);
        Assert.Contains(result.Errors, e => e.Descricao.Contains("nome", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void Criar_DeveRetornarErro_QuandoValorMensalZero()
    {
        var result = Plano.Criar("Plano Prata", 0m, 5);

        Assert.True(result.IsFailed);
        Assert.Contains(result.Errors, e => e.Descricao.Contains("valor mensal", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void Criar_DeveRetornarErro_QuandoValorMensalNegativo()
    {
        var result = Plano.Criar("Plano Bronze", -10m, 5);

        Assert.True(result.IsFailed);
        Assert.Contains(result.Errors, e => e.Descricao.Contains("valor mensal", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void Criar_DeveRetornarErro_QuandoLimiteLojasNegativo()
    {
        var result = Plano.Criar("Plano Básico", 49.90m, -1);

        Assert.True(result.IsFailed);
        Assert.Contains(result.Errors, e => e.Descricao.Contains("limite de lojas", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void Criar_DevePermitirLimiteLojasZero()
    {
        var result = Plano.Criar("Plano Free", 0.01m, 0);

        Assert.True(result.IsSuccess);
        Assert.Equal(0, result.Value!.LimiteLojas);
    }

    [Fact]
    public void Criar_DevePermitirLimiteLojasPositivo()
    {
        var result = Plano.Criar("Plano Empresarial", 199.90m, 50);

        Assert.True(result.IsSuccess);
        Assert.Equal(50, result.Value!.LimiteLojas);
    }
}