using Oficina.Domain.Aggregates.UsuarioAggregates;
using Xunit;

namespace Oficina.Test.Unit.Usuarios;

public class UsuarioContextoTest
{
    [Fact]
    public void Criar_DeveRetornarUsuarioContextoValido_QuandoDadosValidos()
    {
        var result = UsuarioContexto.Criar(10, 20, 30);

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(10, result.Value.UsuarioId);
        Assert.Equal(20, result.Value.ContaId);
        Assert.Equal(30, result.Value.LojaId);
    }

    [Fact]
    public void Criar_DeveRetornarErro_QuandoUsuarioIdInvalido()
    {
        var result = UsuarioContexto.Criar(0, 20, 30);

        Assert.True(result.IsFailed);
        Assert.Contains(result.Errors, e => e.Descricao.Contains("usuário", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void Criar_DeveRetornarErro_QuandoContaIdInvalido()
    {
        var result = UsuarioContexto.Criar(10, 0, 30);

        Assert.True(result.IsFailed);
        Assert.Contains(result.Errors, e => e.Descricao.Contains("conta", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void Criar_DeveRetornarErro_QuandoLojaIdInvalido()
    {
        var result = UsuarioContexto.Criar(10, 20, 0);

        Assert.True(result.IsFailed);
        Assert.Contains(result.Errors, e => e.Descricao.Contains("loja", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void AtualizarLojaPrincipal_DeveAlterarLojaId()
    {
        var contexto = UsuarioContexto.Criar(10, 20, 30).Value!;
        contexto.AtualizarLojaPrincipal(99);

        Assert.Equal(99, contexto.LojaId);
    }
}