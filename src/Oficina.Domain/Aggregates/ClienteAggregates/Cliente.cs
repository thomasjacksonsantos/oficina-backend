using Oficina.Domain.Aggregates.UsuarioAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Domain.ValueObjects;

namespace Oficina.Domain.Aggregates.ClienteAggregates;

public sealed class Cliente
{
    public int Id { get; private set; }
    public string Nome { get; private set; }
    public Guid SexoId { get; private set; }
    public Sexo Sexo { get; private set; }
    public Guid TipoDocumentoId { get; private set; }
    public TipoDocumento TipoDocumento { get; private set; }
    public Documento Documento { get; private set; }
    public Email Email { get; private set; }
    public DataNascimento DataNascimento { get; private set; }
    public ICollection<Contato> Contatos { get; private set; } = [];
    public Endereco Endereco { get; private set; }
    public DataHora Criado { get; private set; }
    public DataHora Atualizado { get; private set; }

#pragma warning disable CS8618
    private Cliente() : base() { }
#pragma warning restore CS8618

    private Cliente(
        string nome,
        Sexo sexo,
        TipoDocumento tipoDocumento,
        Documento documento,
        Email email,
        DataNascimento dataNascimento,
        ICollection<Contato> contatos,
        Endereco endereco
    )
    {
        Nome = nome;
        SexoId = sexo.Id;
        Sexo = sexo;
        TipoDocumentoId = tipoDocumento.Id;
        TipoDocumento = tipoDocumento;
        Documento = documento;
        Email = email;
        DataNascimento = dataNascimento;
        Contatos = contatos ?? new List<Contato>();
        Endereco = endereco;
        Criado = DateTime.Now;
        Atualizado = DateTime.Now;
    }

    public Result<Cliente> Atualizar(
        string nome,
        string sexo,
        string documento,
        string email,
        DateTime dataNascimento,
        ICollection<Contato> contatos,
        Endereco endereco
    )
    {
        var result = new Result<Cliente>();

        if (string.IsNullOrWhiteSpace(nome))
            result.WithError(Erro.ValorInvalido($"{nameof(Cliente)}.{nameof(Nome)}"));

        var sexoObj = Sexo.Get(sexo);
        if (sexoObj is null)
            result.WithError(Erro.ValorInvalido($"{nameof(Cliente)}.{nameof(Sexo)}"));

        var documentoObj = Documento.Criar(documento);
        if (documentoObj.IsFailed)
            result.WithError(Erro.ValorInvalido($"{nameof(Cliente)}.{nameof(Documento)}"));

        var emailObj = Email.Criar(email);
        if (emailObj.IsFailed)
            result.WithError(Erro.ValorInvalido($"{nameof(Cliente)}.{nameof(Email)}"));

        var dataNascimentoObj = DataNascimento.Criar(dataNascimento);
        if (dataNascimentoObj.IsFailed)
            result.WithError(Erro.ValorInvalido($"{nameof(Cliente)}.{nameof(DataNascimento)}"));

        if (result.IsFailed)
            return result;

        Nome = nome;

        if (Sexo.Key != sexoObj!.Key)
            Sexo = Sexo.Get(sexo)!;

        if (TipoDocumento.Key != documentoObj.Value!.TipoDocumento.Key)
            TipoDocumento = documentoObj.Value!.TipoDocumento;

        Documento = documentoObj.Value!;
        Email = emailObj.Value!;
        DataNascimento = dataNascimentoObj.Value!;
        Contatos = contatos;
        Endereco = endereco;
        Atualizado = DateTime.Now;

        return result;
    }

    public static Result<Cliente> Criar(
        string nome,
        string sexo,
        string documento,
        string email,
        DateTime dataNascimento,
        ICollection<Contato> contatos,
        Endereco endereco
    )
    {
        var result = new Result<Cliente>();

        if (string.IsNullOrWhiteSpace(nome))
            result.WithError(Erro.ValorInvalido(nameof(Nome)));

        var sexoObj = Sexo.Get(sexo);
        if (sexoObj is null)
            result.WithError(Erro.ValorInvalido(nameof(Sexo)));
        var documentoObj = Documento.Criar(documento);
        if (documentoObj.IsFailed)
            result.WithError(Erro.ValorInvalido(nameof(Documento)));

        var emailObj = Email.Criar(email);
        if (emailObj.IsFailed)
            result.WithError(Erro.ValorInvalido(nameof(Email)));
        var dataNascimentoObj = DataNascimento.Criar(dataNascimento);
        if (dataNascimentoObj.IsFailed)
            result.WithError(Erro.ValorInvalido(nameof(DataNascimento)));

        if (result.IsFailed)
            return result;

        var cliente = new Cliente(
            nome,
            sexoObj!,
            documentoObj.Value!.TipoDocumento,
            documentoObj.Value!,
            emailObj.Value!,
            dataNascimentoObj.Value!,
            contatos,
            endereco
        );

        return Result.Success(cliente);
    }
}