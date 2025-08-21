

using System.Collections.ObjectModel;
using Oficina.Domain.Aggregates.ContaAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Domain.ValueObjects;

namespace Oficina.Domain.Aggregates.UsuarioAggregates;

public class Usuario
{
    public int Id { get; private set; }
    public string UserId { get; private set; }
    public string Nome { get; private set; }
    public TipoDocumento TipoDocumento { get; private set; }
    public Documento Documento { get; private set; }
    public TipoUsuario TipoUsuario { get; private set; }
    public Sexo Sexo { get; private set; }
    public DataNascimento DataNascimento { get; private set; }
    public bool UsuarioPadrao { get; private set; }
    public Collection<Conta> Contas { get; private set; } = [];
    public Conta ContaPrincipal => Contas.FirstOrDefault(c => c.Principal) ?? Contas.FirstOrDefault()!;
    public Collection<Contato> Contatos { get; private set; }
    public DataHora Criado { get; private set; }
    public DataHora Atualizado { get; private set; }

#pragma warning disable CS8618
    public Usuario() { }
#pragma warning restore CS8618

    public Usuario(
        string userId,
        string nome,
        TipoDocumento tipoDocumento,
        Documento documento,
        TipoUsuario tipoUsuario,
        Sexo sexo,
        DataNascimento dataNascimento,        
        Collection<Contato> contatos
    )
    {
        if (string.IsNullOrEmpty(userId))
            throw new ArgumentOutOfRangeException($"O ${nameof(userId)} informado está inválido id:{userId}");
        Sexo = sexo;
        UserId = userId;
        Nome = nome;
        TipoDocumento = tipoDocumento;
        Documento = documento;
        TipoUsuario = tipoUsuario;
        Criado = DateTime.Now;
        Atualizado = DateTime.Now;
        DataNascimento = dataNascimento;
        Contatos = contatos ?? new Collection<Contato>();
    }

    public void AddConta(Conta conta)
    {
        Contas ??= new Collection<Conta>();
        Contas.Add(conta);
    }

    public static string GerarFotoUrl(string nomeFoto)
        => $"usuario/image/{nomeFoto}";

    public void AtualizarDataHora() =>
        Atualizado = DateTime.Now;

    public void AddUsuarioPadrao() =>
        UsuarioPadrao = true;
    public void RemoveUsuarioPadrao() =>
        UsuarioPadrao = false;

    public static Result<Usuario> Criar(
        string userId,
        string nome,
        string tipoUsuario,
        string tipoDocumento,
        string documento,
        string sexo,
        DateTime dataNascimento,
        Collection<Contato> contatos
    )
    {
        var result = new Result<Usuario>();

        // Validate value objects
        if (string.IsNullOrWhiteSpace(nome))
            result.WithError(Erro.ValorNaoInformado(nameof(nome)));
        var tipoDocumentoObj = TipoDocumento.Get(tipoDocumento);
        if (tipoDocumentoObj is null)
            result.WithError(Erro.NaoEncontrado(tipoDocumento));

        var documentoObj = Documento.Criar(documento);
        if (documentoObj.IsFailed) result.WithErrors(documentoObj.Errors!);

        var tipoUsuarioObj = TipoUsuario.Get(tipoUsuario);
        if (tipoUsuarioObj is null) result.WithError(Erro.NaoEncontrado(tipoUsuario));

        var tipoSexo = Sexo.Get(sexo);
        if (tipoSexo is null) result.WithError(Erro.NaoEncontrado(sexo));

        var dataNascimentoObj = DataNascimento.Criar(dataNascimento);
        if (dataNascimentoObj.IsFailed) result.WithErrors(dataNascimentoObj.Errors!);
        
        if (result.IsFailed)
            return result;

        return new Usuario(
            userId,
            nome,
            tipoDocumentoObj!,
            documentoObj.Value!,
            tipoUsuarioObj!,
            tipoSexo!,
            dataNascimentoObj.Value!,
            contatos
        );
    }
}