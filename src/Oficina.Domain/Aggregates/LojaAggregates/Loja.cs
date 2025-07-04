

using Oficina.Domain.Aggregates.ContaAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Domain.ValueObjects;

namespace Oficina.Domain.Aggregates.LojaAggregates;

public sealed class Loja : IMultiConta
{
    public int Id { get; private set; }
    public string Nome { get; private set; }
    public Documento Documento { get; private set; }
    public Endereco Endereco { get; private set; }
    public ICollection<Contato>? Contatos { get; private set; }
    public int ContaId { get; private set; }
    public Conta Conta { get; private set; } = null!;
    public ICollection<UsuarioLoja>? UsuariosLoja { get; set; }
    public DataHora Criado { get; private set; }
    public DataHora Atualizado { get; private set; }

#pragma warning disable CS8618
    private Loja() { }
#pragma warning restore CS8618

    private Loja(
        string nome,
        Documento documento,
        Conta conta,
        Endereco endereco,
        ICollection<Contato> contatos
    )
    {
        Nome = nome;
        Documento = documento;
        Endereco = endereco;
        Conta = conta;
        Contatos = contatos;
        Criado = DateTime.Now;
        Atualizado = DateTime.Now;
    }

    public static Loja Criar(
        string nome,
        string documento,
        Conta conta,
        Endereco endereco,
        ICollection<Contato> contatos
    )
        => new(
            nome,
            Documento.Criar(
                documento
            ).Value!,
            conta,
            endereco,
            contatos
        );
}