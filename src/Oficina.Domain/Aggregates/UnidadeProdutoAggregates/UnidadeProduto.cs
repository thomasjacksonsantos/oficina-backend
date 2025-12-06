
using Oficina.Domain.SeedWork;
using Oficina.Domain.ValueObjects;

namespace Oficina.Domain.Aggregates.UnidadeProdutoAggregates;

public class UnidadeProduto
{
    public int Id { get; private set; }
    public string Descricao { get; private set; }
    public Guid UnidadeProdutoStatusId { get; private set; }
    public UnidadeProdutoStatus UnidadeProdutoStatus { get; private set; }
    public DataHora Criado { get; private set; }
    public DataHora Atualizado { get; private set; }
#pragma warning disable CS8618
    private UnidadeProduto() { }
#pragma warning restore CS8618

    private UnidadeProduto(
        string descricao
    )
    {
        Descricao = descricao;
        UnidadeProdutoStatusId = UnidadeProdutoStatus.Ativo.Id;
        UnidadeProdutoStatus = UnidadeProdutoStatus.Ativo;
        Criado = DateTime.Now;
        Atualizado = DateTime.Now;
    }

    public void Ativar()
    {
        UnidadeProdutoStatusId = UnidadeProdutoStatus.Ativo.Id;
        UnidadeProdutoStatus = UnidadeProdutoStatus.Ativo;
        Atualizado = DateTime.Now;
    }

    public void Desativar()
    {
        UnidadeProdutoStatusId = UnidadeProdutoStatus.Inativo.Id;
        UnidadeProdutoStatus = UnidadeProdutoStatus.Inativo;
        Atualizado = DateTime.Now;
    }   

    public Result<UnidadeProduto> Atualizar(
        string descricao
    )
    {
        var result = new Result<UnidadeProduto>();

        if (string.IsNullOrWhiteSpace(descricao))
            result.WithError(Erro.ValorInvalido($"{nameof(UnidadeProduto)}.{nameof(descricao)}", "Descrição é obrigatório."));

        if (result.IsFailed)
            return result;

        Descricao = descricao;
        Atualizado = DateTime.Now;

        return Result<UnidadeProduto>.Success(this);
    }

    public static Result<UnidadeProduto> Criar(
        string descricao
    )
    {
        var result = new Result<UnidadeProduto>();

        if (string.IsNullOrWhiteSpace(descricao))
            result.WithError(Erro.ValorInvalido($"{nameof(UnidadeProduto)}.{nameof(descricao)}", "Descrição é obrigatório."));        
        
        if (result.IsFailed)
            return result;

        var unidadeProduto = new UnidadeProduto(
            descricao
        );

        return Result<UnidadeProduto>.Success(unidadeProduto);
    }
}