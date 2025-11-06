

using Oficina.Domain.Aggregates.ContaAggregates;
using Oficina.Domain.Aggregates.UsuarioAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Domain.ValueObjects;

namespace Oficina.Domain.Aggregates.LojaAggregates;

public sealed class Loja : IMultiConta
{
    public int Id { get; private set; }
    public string NomeFantasia { get; private set; }
    public string RazaoSocial { get; private set; }
    public string InscricaoEstadual { get; private set; }
    public string Site { get; private set; }
    public string LogoTipo { get; private set; }
    public Guid TipoDocumentoId { get; private set; }
    public TipoDocumento TipoDocumento { get; private set; }
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
        string nomeFantasia,
        string razaoSocial,
        string inscricaoEstadual,
        string site,
        string logoTipo,
        TipoDocumento tipoDocumento,
        Documento documento,
        Conta conta,
        Endereco endereco,
        ICollection<Contato> contatos
    )
    {
        NomeFantasia = nomeFantasia;
        RazaoSocial = razaoSocial;
        InscricaoEstadual = inscricaoEstadual;
        Site = site;
        LogoTipo = logoTipo;
        TipoDocumento = tipoDocumento;
        Documento = documento;
        Endereco = endereco;
        Conta = conta;
        Contatos = contatos;
        Criado = DateTime.Now;
        Atualizado = DateTime.Now;
    }

    public static Result<Loja> Criar(
        string nomeFantasia,
        string razaoSocial,
        string inscricaoEstadual,
        string site,
        string logoTipo,
        string documento,
        Conta conta,
        Endereco endereco,
        ICollection<Contato> contatos
    )
    {
        var result = new Result<Loja>();

        var documentoObj = Documento.Criar(documento);
        if (documentoObj.IsFailed) result.WithErrors(documentoObj.Errors!);

        if (result.IsFailed) return result;

        return new Loja(
            nomeFantasia,
            razaoSocial,
            inscricaoEstadual,
            site,
            logoTipo,
            documentoObj.Value!.TipoDocumento,
            documentoObj.Value,
            conta,
            endereco,
            contatos
        );
    }
}