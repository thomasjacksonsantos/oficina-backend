using Oficina.Domain.SeedWork;
using Oficina.Domain.ValueObjects;

namespace Oficina.Domain.Aggregates.FornecedorAggregates;

public class Fornecedor
{
    public int Id { get; private set; }
    public string NomeFantasia { get; private set; }
    public string Site { get; private set; }
    public string InscricaoMunicipal { get; private set; }
    public string InscricaoEstadual { get; private set; }
    public Guid FornecedorStatusId { get; private set; }
    public FornecedorStatus FornecedorStatus { get; private set; }
    public Guid TipoConsumidorId { get; private set; }
    public TipoConsumidor TipoConsumidor { get; private set; }
    public Documento Documento { get; private set; }
    public DataNascimento DataNascimento { get; private set; }
    public Email Email { get; private set; }
    public Endereco Endereco { get; private set; }
    public ICollection<Contato>? Contatos { get; private set; }
    public DataHora Criado { get; private set; }
    public DataHora Atualizado { get; private set; }

#pragma warning disable CS8618
    private Fornecedor() : base() { }
#pragma warning restore CS8618

    private Fornecedor(
        string nomeFantasia,
        string site,
        string inscricaoEstadual,
        string inscricaoMunicipal,
        TipoConsumidor tipoConsumidor,
        Documento documento,
        Email email,
        DataNascimento dataNascimento,
        Endereco endereco,
        ICollection<Contato> contatos
    )
    {
        NomeFantasia = nomeFantasia;
        Site = site;
        InscricaoEstadual = inscricaoEstadual;
        InscricaoMunicipal = inscricaoMunicipal;
        TipoConsumidorId = tipoConsumidor.Id;
        TipoConsumidor = tipoConsumidor;
        Documento = documento;
        Email = email;
        DataNascimento = dataNascimento;
        Endereco = endereco;
        Contatos = contatos;
        FornecedorStatus = FornecedorStatus.Ativo;
        FornecedorStatusId = FornecedorStatus.Id;
        Criado = DateTime.Now;
        Atualizado = DateTime.Now;
    }

    public void Ativar()
    {
        FornecedorStatus = FornecedorStatus.Ativo;
        Atualizado = DateTime.Now;
    }

    public void Desativar()
    {
        FornecedorStatus = FornecedorStatus.Inativo;
        Atualizado = DateTime.Now;
    }

    public Result<Fornecedor> Atualizar(
        string nomeFantasia,
        string site,
        string inscricaoEstadual,
        string inscricaoMunicipal,
        string tipoConsumidor,
        string documento,
        string email,
        DateTime dataNascimento,
        Endereco endereco,
        ICollection<Contato> contatos
    )
    {
        var result = new Result<Fornecedor>();

        if (string.IsNullOrWhiteSpace(nomeFantasia))
            result.WithError(Erro.ValorInvalido($"{nameof(Fornecedor)}.{nameof(nomeFantasia)}", "Nome Fantasia é obrigatório."));

        if (string.IsNullOrWhiteSpace(site))
            result.WithError(Erro.ValorInvalido($"{nameof(Fornecedor)}.{nameof(site)}", "Site é obrigatório."));

        if (string.IsNullOrWhiteSpace(inscricaoEstadual))
            result.WithError(Erro.ValorInvalido($"{nameof(Fornecedor)}.{nameof(inscricaoEstadual)}", "Inscrição Estadual é obrigatório."));

        if (string.IsNullOrWhiteSpace(inscricaoMunicipal))
            result.WithError(Erro.ValorInvalido($"{nameof(Fornecedor)}.{nameof(inscricaoMunicipal)}", "Inscrição Municipal é obrigatório."));

        var tipoConsumidorObj = TipoConsumidor.Get(tipoConsumidor);
        if (tipoConsumidorObj == null)
            result.WithError(Erro.ValorInvalido($"{nameof(Fornecedor)}.{nameof(TipoConsumidor)}", "Tipo Consumidor informado é inválido."));

        var documentoObj = Documento.Criar(documento);
        if (documentoObj.IsFailed)
            documentoObj.Errors.ForEach(e =>
            {
                result.WithError(Erro.ValorInvalido($"{nameof(Fornecedor)}.{nameof(Documento)}", e.Descricao));
            });

        var emailObj = Email.Criar(email);
        if (emailObj.IsFailed)
            emailObj.Errors.ForEach(e =>
            {
                result.WithError(Erro.ValorInvalido($"{nameof(Fornecedor)}.{nameof(Email)}", e.Descricao));
            });

        var dataNascimentoObj = DataNascimento.Criar(dataNascimento);
        if (dataNascimentoObj.IsFailed)
            dataNascimentoObj.Errors.ForEach(e =>
            {
                result.WithError(Erro.ValorInvalido($"{nameof(Fornecedor)}.{nameof(DataNascimento)}", e.Descricao));
            });

        if (result.IsFailed)
            return result;

        NomeFantasia = nomeFantasia;
        Site = site;
        InscricaoEstadual = inscricaoEstadual;
        InscricaoMunicipal = inscricaoMunicipal;
        TipoConsumidorId = tipoConsumidorObj!.Id;
        Documento = documentoObj.Value!;
        Email = emailObj.Value!;
        DataNascimento = dataNascimentoObj.Value!;
        Endereco = endereco;
        Contatos = contatos;
        Atualizado = DateTime.Now;

        return Result.Success();
    }

    public static Result<Fornecedor> Criar(
        string nomeFantasia,
        string site,
        string inscricaoEstadual,
        string inscricaoMunicipal,
        string tipoConsumidor,
        string documento,
        string email,
        DateTime dataNascimento,
        Endereco endereco,
        ICollection<Contato> contatos
    )
    {
        var result = new Result<Fornecedor>();

        if (string.IsNullOrWhiteSpace(nomeFantasia))
            result.WithError(Erro.ValorInvalido($"{nameof(Fornecedor)}.{nameof(nomeFantasia)}", "Nome Fantasia é obrigatório."));

        if (string.IsNullOrWhiteSpace(site))
            result.WithError(Erro.ValorInvalido($"{nameof(Fornecedor)}.{nameof(site)}", "Site é obrigatório."));

        if (string.IsNullOrWhiteSpace(inscricaoEstadual))
            result.WithError(Erro.ValorInvalido($"{nameof(Fornecedor)}.{nameof(inscricaoEstadual)}", "Inscrição Estadual é obrigatório."));

        if (string.IsNullOrWhiteSpace(inscricaoMunicipal))
            result.WithError(Erro.ValorInvalido($"{nameof(Fornecedor)}.{nameof(inscricaoMunicipal)}", "Inscrição Municipal é obrigatório."));

        var tipoConsumidorObj = TipoConsumidor.Get(tipoConsumidor);
        if (tipoConsumidorObj == null)
            result.WithError(Erro.ValorInvalido($"{nameof(Fornecedor)}.{nameof(TipoConsumidor)}", "Tipo Consumidor informado é inválido."));

        var documentoObj = Documento.Criar(documento);
        if (documentoObj.IsFailed)
            documentoObj.Errors.ForEach(e =>
            {
                result.WithError(Erro.ValorInvalido($"{nameof(Fornecedor)}.{nameof(Documento)}", e.Descricao));
            });

        var emailObj = Email.Criar(email);
        if (emailObj.IsFailed)
            emailObj.Errors.ForEach(e =>
            {
                result.WithError(Erro.ValorInvalido($"{nameof(Fornecedor)}.{nameof(Email)}", e.Descricao));
            });

        var dataNascimentoObj = DataNascimento.Criar(dataNascimento);
        if (dataNascimentoObj.IsFailed)
            dataNascimentoObj.Errors.ForEach(e =>
            {
                result.WithError(Erro.ValorInvalido($"{nameof(Fornecedor)}.{nameof(DataNascimento)}", e.Descricao));
            });

        if (result.IsFailed)
            return result;

        var fornecedor = new Fornecedor(
            nomeFantasia,
            site,
            inscricaoEstadual,
            inscricaoMunicipal,
            tipoConsumidorObj!,
            documentoObj.Value!,
            emailObj.Value!,
            dataNascimentoObj.Value!,
            endereco,
            contatos
        );

        return Result.Success(fornecedor);
    }
}