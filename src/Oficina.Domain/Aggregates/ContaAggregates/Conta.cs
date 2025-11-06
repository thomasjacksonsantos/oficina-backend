

using Oficina.Domain.Aggregates.LojaAggregates;
using Oficina.Domain.Aggregates.UsuarioAggregates;
using Oficina.Domain.ValueObjects;

namespace Oficina.Domain.Aggregates.ContaAggregates;

public sealed class Conta
{
    private List<Loja>? _lojas;
    private List<Usuario> _usuarios = new List<Usuario>(    );

#pragma warning disable CS8618
    private Conta() { }
#pragma warning restore CS8618

    private Conta(
        string nome,
        bool principal
    )
    {
        Nome = nome;
        Principal = principal;
        Status = ContaStatus.Ativo;
        Criado = DateTime.Now;
        Atualizado = DateTime.Now;
    }

    public int Id { get; private set; }
    public string Nome { get; private set; }
    public Guid StatusId { get; private set; }
    public bool Principal { get; private set; }
    public Guid ContaStatusId { get; private set; }
    public ContaStatus Status { get; private set; }
    public IReadOnlyCollection<Usuario> Usuarios =>
        _usuarios.AsReadOnly();
    public IReadOnlyCollection<Loja>? Lojas =>
        _lojas?.AsReadOnly();
    public DataHora Criado { get; private set; }
    public DataHora Atualizado { get; private set; }

    public void AddUsuario(Usuario usuario)
    {
        _usuarios ??= new List<Usuario>();
        _usuarios.Add(usuario);
    }
    public void AddLoja(Loja loja)
    {
        _lojas ??= new List<Loja>();
        _lojas.Add(loja);
    }

    public void AddLoja(List<Loja> lojas)
    {
        _lojas ??= new List<Loja>();
        _lojas.AddRange(lojas);
    }

    public static Conta Criar(
        string nome,
        bool principal
    )
        => new(
            nome,
            principal
        );
}