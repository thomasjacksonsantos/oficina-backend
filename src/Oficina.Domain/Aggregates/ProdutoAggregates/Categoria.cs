using Oficina.Domain.SeedWork;
using Oficina.Domain.ValueObjects;

namespace Oficina.Domain.Aggregates.ProdutoAggregates;

public class Categoria
{
    public int Id { get; private set; }
    public string Nome { get; private set; }
    public DataHora Criado { get; private set; }
    public DataHora Atualizado { get; private set; }

#pragma warning disable CS8618 // Para uso do EF/serialização
    private Categoria() { }
#pragma warning restore CS8618

    private Categoria(
        string nome
    )
    {
        Nome = nome;
        Criado = DataHora.Criar().Value!;
        Atualizado = DataHora.Criar().Value!;
    }

    public static Result<Categoria> Criar(
        string nome
    )
    {
        // Validações de negócio podem ser aplicadas aqui

        var categoria = new Categoria(
            nome
        );

        return Result.Success(categoria);
    }
}