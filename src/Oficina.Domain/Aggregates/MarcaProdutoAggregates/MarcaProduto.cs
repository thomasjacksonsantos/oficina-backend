
using Oficina.Domain.SeedWork;
using Oficina.Domain.ValueObjects;

namespace Oficina.Domain.Aggregates.MarcaProdutoAggregates;

public class MarcaProduto
{
    public int Id { get; private set; }
    public string Descricao { get; private set; }
    public Guid MarcaProdutoStatusId { get; private set; }
    public MarcaProdutoStatus MarcaProdutoStatus { get; private set; }
    public DataHora Criado { get; private set; }
    public DataHora Atualizado { get; private set; }
#pragma warning disable CS8618
    private MarcaProduto() { }
#pragma warning restore CS8618

    private MarcaProduto(
        string descricao
    )
    {
        Descricao = descricao;
        MarcaProdutoStatusId = MarcaProdutoStatus.Ativo.Id;
        MarcaProdutoStatus = MarcaProdutoStatus.Ativo;
        Criado = DateTime.Now;
        Atualizado = DateTime.Now;
    }

    public void Ativar()
    {
        MarcaProdutoStatusId = MarcaProdutoStatus.Ativo.Id;
        MarcaProdutoStatus = MarcaProdutoStatus.Ativo;
        Atualizado = DateTime.Now;
    }

    public void Desativar()
    {
        MarcaProdutoStatusId = MarcaProdutoStatus.Inativo.Id;
        MarcaProdutoStatus = MarcaProdutoStatus.Inativo;
        Atualizado = DateTime.Now;
    }   

    public Result<MarcaProduto> Atualizar(
        string descricao
    )
    {
        var result = new Result<MarcaProduto>();

        if (string.IsNullOrWhiteSpace(descricao))
            result.WithError(Erro.ValorInvalido($"{nameof(MarcaProduto)}.{nameof(descricao)}", "Descrição é obrigatório."));

        if (result.IsFailed)
            return result;

        Descricao = descricao;
        Atualizado = DateTime.Now;

        return Result<MarcaProduto>.Success(this);
    }

    public static Result<MarcaProduto> Criar(
        string descricao
    )
    {
        var result = new Result<MarcaProduto>();

        if (string.IsNullOrWhiteSpace(descricao))
            result.WithError(Erro.ValorInvalido($"{nameof(MarcaProduto)}.{nameof(descricao)}", "Descrição é obrigatório."));        
        
        if (result.IsFailed)
            return result;

        var marcaProduto = new MarcaProduto(
            descricao
        );

        return Result<MarcaProduto>.Success(marcaProduto);
    }
}