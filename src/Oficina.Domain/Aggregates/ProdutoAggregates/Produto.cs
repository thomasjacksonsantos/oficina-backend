using Oficina.Domain.SeedWork;
using Oficina.Domain.ValueObjects;

namespace Oficina.Domain.Aggregates.ProdutoAggregates;

public class Produto
{
    public Guid Id { get; private set; }
    public Guid CategoriaId { get; private set; }
    public Categoria? Categoria { get; private set; }
    public string Descricao { get; private set; }
    public ICollection<PrecoLoja>? PrecoLojas { get; set; }
    public DataHora Criado { get; private set; }
    public DataHora Atualizado { get; private set; }

#pragma warning disable CS8618 // Propriedade não inicializada
    private Produto() { }
#pragma warning restore CS8618

    public Produto(
        Guid categoriaId,
        string descricao
    )
    {
        CategoriaId = categoriaId;
        Descricao = descricao;
        Criado = DataHora.Criar().Value!;
        Atualizado = DataHora.Criar().Value!;
    }

    public void AddPrecoLoja(PrecoLoja precoLoja)
    {
        PrecoLojas ??= new List<PrecoLoja>();
        PrecoLojas.Add(precoLoja);
        Atualizado = DataHora.Criar().Value!;
    }

    public static Result<Produto> Criar(
        string descricao,
        Guid categoriaId)
    {
        var result = new Result<Produto>();

        if (categoriaId == Guid.Empty)
            result.WithError(Erro.ValorInvalido("Produto.CategoriaId"));


        if (string.IsNullOrWhiteSpace(descricao))
            result.WithError(Erro.ValorInvalido("Produto.Descricao"));

        if (result.IsFailed)
            return result;

        return new Produto(categoriaId, descricao);
    }
}