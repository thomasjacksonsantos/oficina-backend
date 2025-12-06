
using Oficina.Domain.SeedWork;
using Oficina.Domain.ValueObjects;

namespace Oficina.Domain.Aggregates.AreaProdutoAggregates;

public class AreaProduto
{
    public int Id { get; private set; }
    public string Descricao { get; private set; }
    public string Garantia { get; private set; }
    public Guid AreaProdutoStatusId { get; private set; }
    public AreaProdutoStatus AreaProdutoStatus { get; private set; }
    public DataHora Criado { get; private set; }
    public DataHora Atualizado { get; private set; }
#pragma warning disable CS8618
    private AreaProduto() { }
#pragma warning restore CS8618

    private AreaProduto(
        string descricao,
        string garantia
    )
    {
        Descricao = descricao;
        Garantia = garantia;
        AreaProdutoStatusId = AreaProdutoStatus.Ativo.Id;
        AreaProdutoStatus = AreaProdutoStatus.Ativo;
        Criado = DateTime.Now;
        Atualizado = DateTime.Now;
    }

    public void Ativar()
    {
        AreaProdutoStatusId = AreaProdutoStatus.Ativo.Id;
        AreaProdutoStatus = AreaProdutoStatus.Ativo;
        Atualizado = DateTime.Now;
    }

    public void Desativar()
    {
        AreaProdutoStatusId = AreaProdutoStatus.Inativo.Id;
        AreaProdutoStatus = AreaProdutoStatus.Inativo;
        Atualizado = DateTime.Now;
    }   

    public Result<AreaProduto> Atualizar(
        string descricao,
        string garantia
    )
    {
        var result = new Result<AreaProduto>();

        if (string.IsNullOrWhiteSpace(descricao))
            result.WithError(Erro.ValorInvalido($"{nameof(AreaProduto)}.{nameof(descricao)}", "Descrição é obrigatório."));

        if (string.IsNullOrWhiteSpace(garantia))
            result.WithError(Erro.ValorInvalido($"{nameof(AreaProduto)}.{nameof(garantia)}", "Garantia é obrigatório."));        

        if (result.IsFailed)
            return result;

        Descricao = descricao;
        Garantia = garantia;
        Atualizado = DateTime.Now;

        return Result<AreaProduto>.Success(this);
    }

    public static Result<AreaProduto> Criar(
        string descricao,
        string garantia
    )
    {
        var result = new Result<AreaProduto>();

        if (string.IsNullOrWhiteSpace(descricao))
            result.WithError(Erro.ValorInvalido($"{nameof(AreaProduto)}.{nameof(descricao)}", "Descrição é obrigatório."));

        if (string.IsNullOrWhiteSpace(garantia))
            result.WithError(Erro.ValorInvalido($"{nameof(AreaProduto)}.{nameof(garantia)}", "Garantia é obrigatório."));
        
        if (result.IsFailed)
            return result;

        var areaProduto = new AreaProduto(
            descricao,
            garantia
        );

        return Result<AreaProduto>.Success(areaProduto);
    }
}