
using Oficina.Domain.SeedWork;
using Oficina.Domain.ValueObjects;

namespace Oficina.Domain.Aggregates.GrupoProdutoAggregates;

public class GrupoProduto
{
    public int Id { get; private set; }
    public string Descricao { get; private set; }
    public string Area { get; private set; }
    public string NCM { get; private set; }
    public string ANP { get; private set; }
    public Guid GrupoProdutoStatusId { get; private set; }
    public GrupoProdutoStatus GrupoProdutoStatus { get; private set; }
    public DataHora Criado { get; private set; }
    public DataHora Atualizado { get; private set; }
#pragma warning disable CS8618
    private GrupoProduto() { }
#pragma warning restore CS8618

    private GrupoProduto(
        string descricao,
        string area,
        string ncm,
        string anp
    )
    {
        Descricao = descricao;
        Area = area;
        NCM = ncm;
        ANP = anp;
        GrupoProdutoStatusId =  GrupoProdutoStatus.Ativo.Id;
        GrupoProdutoStatus = GrupoProdutoStatus.Ativo;
        Criado = DateTime.Now;
        Atualizado = DateTime.Now;
    }

    public void Ativar()
    {
        GrupoProdutoStatusId = GrupoProdutoStatus.Ativo.Id;
        GrupoProdutoStatus = GrupoProdutoStatus.Ativo;
        Atualizado = DateTime.Now;
    }

    public void Desativar()
    {
        GrupoProdutoStatusId = GrupoProdutoStatus.Inativo.Id;
        GrupoProdutoStatus = GrupoProdutoStatus.Inativo;
        Atualizado = DateTime.Now;
    }   

    public Result<GrupoProduto> Atualizar(
        string descricao,
        string area,
        string ncm,
        string anp
    )
    {
        var result = new Result<GrupoProduto>();

        if (string.IsNullOrWhiteSpace(descricao))
            result.WithError(Erro.ValorInvalido($"{nameof(GrupoProduto)}.{nameof(descricao)}", "Descrição é obrigatório."));

        if (string.IsNullOrWhiteSpace(area))
            result.WithError(Erro.ValorInvalido($"{nameof(GrupoProduto)}.{nameof(area)}", "Área é obrigatório."));

        if (string.IsNullOrWhiteSpace(ncm))
            result.WithError(Erro.ValorInvalido($"{nameof(GrupoProduto)}.{nameof(ncm)}", "NCM é obrigatório."));

        if (string.IsNullOrWhiteSpace(anp))
            result.WithError(Erro.ValorInvalido($"{nameof(GrupoProduto)}.{nameof(anp)}", "ANP é obrigatório."));

        if (result.IsFailed)
            return result;

        Descricao = descricao;
        Area = area;
        NCM = ncm;
        ANP = anp;
        Atualizado = DateTime.Now;

        return Result<GrupoProduto>.Success(this);
    }

    public static Result<GrupoProduto> Criar(
        string descricao,
        string area,
        string ncm,
        string anp
    )
    {
        var result = new Result<GrupoProduto>();

        if (string.IsNullOrWhiteSpace(descricao))
            result.WithError(Erro.ValorInvalido($"{nameof(GrupoProduto)}.{nameof(descricao)}", "Descrição é obrigatório."));

        if (string.IsNullOrWhiteSpace(area))
            result.WithError(Erro.ValorInvalido($"{nameof(GrupoProduto)}.{nameof(area)}", "Área é obrigatório."));

        if (string.IsNullOrWhiteSpace(ncm))
            result.WithError(Erro.ValorInvalido($"{nameof(GrupoProduto)}.{nameof(ncm)}", "NCM é obrigatório."));

        if (string.IsNullOrWhiteSpace(anp))
            result.WithError(Erro.ValorInvalido($"{nameof(GrupoProduto)}.{nameof(anp)}", "ANP é obrigatório."));

        if (result.IsFailed)
            return result;

        var grupoProduto = new GrupoProduto(
            descricao,
            area,
            ncm,
            anp
        );

        return Result<GrupoProduto>.Success(grupoProduto);
    }
}