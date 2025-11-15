using Oficina.Aggregates.ContaAggregates;
using Xunit;

namespace Oficina.Test.Unit;

public class PagamentoContaTest
{
    [Fact]
    public void Criar_DeveRetornarPagamentoContaValido_QuandoDadosValidos()
    {
        var result = PagamentoConta.Criar(1, 100.50m, DateTime.UtcNow, "11/2025");

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(1, result.Value.ContaId);
        Assert.Equal(100.50m, result.Value.ValorPago);
        Assert.Equal("11/2025", result.Value.Referencia);
        Assert.True(result.Value.Pago);
    }

    [Fact]
    public void Criar_DeveRetornarErro_QuandoContaIdInvalido()
    {
        var result = PagamentoConta.Criar(0, 100.50m, DateTime.UtcNow, "11/2025");

        Assert.True(result.IsFailed);
        Assert.Contains(result.Errors, e => e.Descricao.Contains("conta", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void Criar_DeveRetornarErro_QuandoValorPagoZero()
    {
        var result = PagamentoConta.Criar(1, 0m, DateTime.UtcNow, "11/2025");

        Assert.True(result.IsFailed);
        Assert.Contains(result.Errors, e => e.Descricao.Contains("valor pago", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void Criar_DeveRetornarErro_QuandoValorPagoNegativo()
    {
        var result = PagamentoConta.Criar(1, -10m, DateTime.UtcNow, "11/2025");

        Assert.True(result.IsFailed);
        Assert.Contains(result.Errors, e => e.Descricao.Contains("valor pago", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void Criar_DeveRetornarErro_QuandoDataPagamentoInvalida()
    {
        var result = PagamentoConta.Criar(1, 100.50m, DateTime.MinValue, "11/2025");

        Assert.True(result.IsFailed);
        Assert.Contains(result.Errors, e => e.Descricao.Contains("data de pagamento", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void Criar_DeveRetornarErro_QuandoReferenciaVazia()
    {
        var result = PagamentoConta.Criar(1, 100.50m, DateTime.UtcNow, "");

        Assert.True(result.IsFailed);
        Assert.Contains(result.Errors, e => e.Descricao.Contains("referência", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void Criar_DeveRetornarErro_QuandoReferenciaNula()
    {
        var result = PagamentoConta.Criar(1, 100.50m, DateTime.UtcNow, null!);

        Assert.True(result.IsFailed);
        Assert.Contains(result.Errors, e => e.Descricao.Contains("referência", StringComparison.OrdinalIgnoreCase));
    }
}