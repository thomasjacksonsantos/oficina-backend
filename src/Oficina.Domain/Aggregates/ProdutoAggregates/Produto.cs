using Oficina.Domain.SeedWork;
using Oficina.Domain.ValueObjects;

namespace Oficina.Domain.Aggregates.ProdutoAggregates;

public class Produto
{
    public int Id { get; private set; }
    public int CategoriaId { get; private set; }
    public Categoria Categoria { get; private set; }
    public ICollection<PrecoLoja> PrecoLojas { get; set; }
    public DataHora Criado { get; private set; }
    public DataHora Atualizado { get; private set; }

#pragma warning disable CS8618 // Propriedade não inicializada
    private Produto() { }
#pragma warning restore CS8618

    public Produto(int categoriaId, Categoria categoria, ICollection<PrecoLoja> precoLojas)
    {
        CategoriaId = categoriaId;
        Categoria = categoria ?? throw new ArgumentNullException(nameof(categoria));
        PrecoLojas = precoLojas ?? new List<PrecoLoja>();
        Criado = DataHora.Criar().Value!;
        Atualizado = DataHora.Criar().Value!;
    }

    public static Result<Produto> Criar(int categoriaId, Categoria categoria, ICollection<PrecoLoja> precoLojas)
    {
        if (categoria == null)
            return Result.Fail("Categoria não pode ser nula.");

        var produto = new Produto(categoriaId, categoria, precoLojas);
        return Result.Success(produto);
    }
}