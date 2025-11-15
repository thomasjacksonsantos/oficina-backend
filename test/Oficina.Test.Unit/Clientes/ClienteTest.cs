using Oficina.Domain.Aggregates.ClienteAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Domain.ValueObjects;

namespace Oficina.Test.Unit.Clientes;

public class ClienteTest
{   
    [Fact]
    public void Criar_DeveRetornarClienteValido()
    {
        var endereco = Endereco.Criar("Rua A", "100", "Bairro B", "Cidade C", "Estado D", "CEP12345", "403").Value!;

        var contatos = new List<Contato>
        {
            Contato.Criar("19123456789", "telefone").Value!
        };

        var result = Cliente.Criar(
            "João Silva",
            "masculino",
            "31435600886",
            "joao.silva@example.com",
            DateTime.Parse("1990-01-01"),
            contatos,
            endereco
        );

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal("João Silva", result.Value!.Nome);
        Assert.Equal("Masculino", result.Value!.Sexo.Key);
        Assert.Equal(Guid.Parse("01963629-5d16-75e4-a596-295d8ccd46fa"), result.Value!.SexoId);
        Assert.Equal("Cpf", result.Value!.TipoDocumento.Key);
        Assert.Equal(Guid.Parse("10132080-452d-4d7d-861e-0c5801c94d57"), result.Value!.TipoDocumentoId);
        Assert.Equal("31435600886", result.Value!.Documento.Numero);
        Assert.Equal("joao.silva@example.com", result.Value!.Email.Valor);
        Assert.Equal(new DateTime(1990, 1, 1), result.Value!.DataNascimento.Valor);
        Assert.Equal(0, result.Value!.Id);
        Assert.NotNull(result.Value!.Contatos);
        Assert.NotNull(result.Value!.Endereco);
        Assert.NotNull(result.Value!.Criado);
        Assert.NotNull(result.Value!.Atualizado);
    }

    [Fact]
    public void Criar_ContatoNuloDeveRetornarClienteValido()
    {
        var endereco = Endereco.Criar("Rua A", "100", "Bairro B", "Cidade C", "Estado D", "CEP12345", "403").Value!;

        var result = Cliente.Criar(
            "João Silva",
            "masculino",
            "31435600886",
            "joao.silva@example.com",
            DateTime.Parse("1990-01-01"),
            null!,
            endereco
        );

        Assert.True(result.IsSuccess);
        Assert.Equal(0, result.Value!.Id);
        Assert.NotNull(result.Value);
    }

    [Fact]
    public void Criar_DeveRetornarErro_QuandoNomeForInvalido()
    {
        var endereco = Endereco.Criar("Rua A", "100", "Bairro B", "Cidade C", "Estado D", "CEP12345", "403").Value!;

        var contatos = new List<Contato>
        {
            Contato.Criar("19123456789", "telefone").Value!
        };

        var result = Cliente.Criar(
            "",
            "",
            "31435600880",
            "joao.silva",
            DateTime.Parse("1900-01-01"),
            contatos,
            endereco
        );

        Assert.False(result.IsSuccess);

        Assert.Contains(result.Errors!, e => e.Descricao == Erro.ValorInvalido("Cliente.Nome").Descricao);
        Assert.Contains(result.Errors!, e => e.Descricao == Erro.ValorInvalido("Cliente.Sexo").Descricao);
        Assert.Contains(result.Errors!, e => e.Descricao == Erro.ValorInvalido("Cliente.Documento").Descricao);
        Assert.Contains(result.Errors!, e => e.Descricao == Erro.ValorInvalido("Cliente.Email").Descricao);
        Assert.Contains(result.Errors!, e => e.Descricao == Erro.ValorInvalido("Cliente.DataNascimento").Descricao);
    }

    [Fact]
    public void Atualizar_Cliente_DeveRetornarClienteAtualizado()
    {
        // Given
        var endereco = Endereco.Criar("Rua A", "100", "Bairro B", "Cidade C", "Estado D", "CEP12345", "403").Value!;
        var contatos = new List<Contato>
        {
            Contato.Criar("19123456789", "telefone").Value!
        };

        var clienteResult = Cliente.Criar(
            "João Silva",
            "masculino",
            "31435600886",
            "joao.silva@example.com",
            DateTime.Parse("1990-01-01"),
            contatos,
            endereco
        );

        // When

        Assert.True(clienteResult.IsSuccess);
        var cliente = clienteResult.Value!;
        var updateResult = cliente.Atualizar(
            "João Pereira",
            "masculino",
            "31435600886",
            "joao.pereira@example.com",
            DateTime.Parse("1991-02-02"),
            contatos,
            endereco
        );

        // Then
        Assert.True(updateResult.IsSuccess);
        Assert.Equal(0, cliente.Id);
        Assert.Equal("João Pereira", cliente.Nome);
        Assert.Equal("joao.pereira@example.com", cliente.Email.Valor);
        Assert.Equal("Masculino", cliente.Sexo.Key);
        Assert.Equal("31435600886", cliente.Documento.Numero);
        Assert.Equal(new DateTime(1991, 2, 2), cliente.DataNascimento.Valor);
        Assert.NotNull(cliente.Contatos);
        Assert.NotNull(cliente.Endereco);
        Assert.NotNull(cliente.Criado);
        Assert.NotNull(cliente.Atualizado);
    }

    [Fact]
    public void Atualizar_DeveRetornarErro_QuandoNomeForInvalido()
    {
        // Given
        var endereco = Endereco.Criar("Rua A", "100", "Bairro B", "Cidade C", "Estado D", "CEP12345", "403").Value!;
        var contatos = new List<Contato>
        {
            Contato.Criar("19123456789", "telefone").Value!
        };
        var clienteResult = Cliente.Criar(
            "João Silva",
            "masculino",
            "31435600886",
            "joao.silva@example.com",
            DateTime.Parse("1990-01-01"),
            contatos,
            endereco
        );

        // When
        Assert.True(clienteResult.IsSuccess);
        var cliente = clienteResult.Value!;
        var updateResult = cliente.Atualizar(
            "",
            "",
            "31435600880",
            "",
            DateTime.Parse("1900-01-01"),
            null!,
            null!
        );

        // Then
        Assert.False(updateResult.IsSuccess);
        Assert.Equal(0, cliente.Id);
        Assert.Contains(updateResult.Errors!, e => e.Descricao == Erro.ValorInvalido("Cliente.Nome").Descricao);
        Assert.Contains(updateResult.Errors!, e => e.Descricao == Erro.ValorInvalido("Cliente.Documento").Descricao);
        Assert.Contains(updateResult.Errors!, e => e.Descricao == Erro.ValorInvalido("Cliente.Email").Descricao);
        Assert.Contains(updateResult.Errors!, e => e.Descricao == Erro.ValorInvalido("Cliente.DataNascimento").Descricao);
        Assert.NotNull(cliente.Criado);
        Assert.NotNull(cliente.Atualizado);
    }

    [Fact]
    public void Atualizar_DeveRetornarValido_QuandoAlterarSexoETipoDocumento()
    {
        // Given
        var endereco = Endereco.Criar("Rua A", "100", "Bairro B", "Cidade C", "Estado D", "CEP12345", "403").Value!;
        var contatos = new List<Contato>
        {
            Contato.Criar("19123456789", "telefone").Value!
        };
        var clienteResult = Cliente.Criar(
            "João Silva",
            "masculino",
            "31435600886",
            "joao.silva@example.com",
            DateTime.Parse("1990-01-01"),
            contatos,
            endereco
        );

        // When
        Assert.True(clienteResult.IsSuccess);
        var cliente = clienteResult.Value!;
        var updateResult = cliente.Atualizar(
            "Maria das Dores",
            "feminino",
            "81465252000198",
            "mariadasdores@gmail.com",
            DateTime.Parse("1990-01-01"),
            contatos,
            endereco
        );

        // Then
        Assert.True(updateResult.IsSuccess);
        Assert.Equal(0, cliente.Id);
        Assert.Equal("Maria das Dores", cliente.Nome);
        Assert.Equal("Feminino", cliente.Sexo.Key);
        Assert.Equal("Cnpj", cliente.TipoDocumento.Key);
        Assert.Equal("81465252000198", cliente.Documento.Numero);
        Assert.Equal(new DateTime(1990, 1, 1), cliente.DataNascimento.Valor);
        Assert.NotNull(cliente.Contatos);
        Assert.NotNull(cliente.Endereco);
        Assert.NotNull(cliente.Criado);
        Assert.NotNull(cliente.Atualizado);
    }
}