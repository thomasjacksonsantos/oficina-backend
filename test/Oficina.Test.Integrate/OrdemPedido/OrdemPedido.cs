using Oficina.Domain.Aggregates.ClienteAggregates;
using Oficina.Domain.Aggregates.ContaAggregates;
using Oficina.Domain.Aggregates.LojaAggregates;
using Oficina.Domain.Aggregates.OrdemServicoAggregates;
using Oficina.Domain.Aggregates.ProdutoAggregates;
using Oficina.Domain.Aggregates.UsuarioAggregates;
using Oficina.Domain.Aggregates.VeiculoAggregates;
using Oficina.Domain.ValueObjects;

namespace Oficina.Test.Integrate.OrdemServicos;

public class OrdemServicoTest
{
    // Testes de integração para OrdemServico podem ser adicionados aqui
    [Fact]
    public void Criar_DeveRetornarOrdemServicoValida()
    {
        // Given

        var usuario = Usuario.Criar(
            "usuario1",
            "Thomas Jackson",
            TipoUsuario.SuperAdmin.Nome.ToString(),
            TipoDocumento.Cnpj.Nome,
            "12345678000199",
            "Masculino",
            new DateTime(1990, 1, 1),
            new List<Contato>
            {
                Contato.Criar("19", "999999999", "telefone").Value!
            }
        );

        var conta = Conta.Criar(
            "oficina.conta1",
            true
        );

        var loja = Loja.Criar(
            "Oficina do Zé",
            "Oficina do Zé Ltda",
            "12345678000199",
            "www.oficinadoze.com",
            "logo.png",
            "35931297000193",
            Conta.Criar("oficinadoze", false),
            Endereco.Criar("Rua B", "200", "Bairro C", "Cidade D", "Estado E", "CEP54321", "404").Value!,
            new List<Contato>
            {
                Contato.Criar("19", "987654321", "telefone").Value!
            }
        ).Value!;

        conta.AddUsuario(usuario.Value!);
        conta.AddLoja(loja);

        var categoria = Categoria.Criar(
            "Serviços"
        ).Value!;

        var produto = Produto.Criar(
            "Serviço de troca de óleo do motor",
            Guid.NewGuid()
        ).Value!;


        var produtoPreco = PrecoLoja.Criar(
            loja.Id,
            produto.Id,
            150.00m
        ).Value!;

        produto.AddPrecoLoja(produtoPreco);

        // When
        var veiculo = Veiculo.Criar(
           "ABC-1234",
           "Modelo X",
           10000,
           "Preto",
           2025,
           "12323aas2",
           "123213123",
           "2.0",
           "213213123"
       ).Value!;

        var cliente = Cliente.Criar(
            "João Silva",
            "masculino",
            "31435600886",
            "thomas.j.santos@gmail.com",
            DateTime.Parse("1990-01-01"),
            new List<Contato>
            {
                Contato.Criar("19", "123456789", "telefone").Value!
            },
            Endereco.Criar("Rua A", "100", "Bairro B", "Cidade C", "Estado D", "CEP12345", "403").Value!
        ).Value!;

        var clienteVeiculo = VeiculoCliente.Criar(
            veiculo,
            cliente
        ).Value!;

        var dataFaturamentoInicial = DateTime.Now;

        var ordemServicoResult = OrdemServico.Criar(
            dataFaturamentoInicial,
            "Observação",
            1,
            clienteVeiculo
        ).Value!;

        var item = OrdemServicoItem.Criar(
            produto.Id,
            1,
            150.00m,
            5,
            143.00m
        ).Value!;

        ordemServicoResult.AddItem(item);

        // Then
        Assert.True(ordemServicoResult.Itens!.Count == 1);
       
    }
}