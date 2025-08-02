
using Oficina.Domain.Enumerations;

namespace Oficina.Domain.Aggregates.UsuarioAggregates;

public record TipoTelefone : DadoDominio
{
    private TipoTelefone(Guid id, string key) : base(id, key) { }

    public static TipoTelefone? Get(string key) =>
        key.ToLower() switch
        {
            "telefone" => Telefone,
            "celular" => Celular,
            _ => null
        };

    public static readonly TipoTelefone Telefone = ("67c3c783-654b-44c5-a07f-24a8b6294709", "Telefone");
    public static readonly TipoTelefone Celular = ("c99a5988-b022-4b68-b640-af5021dc7c8f", "Celular");

    public static implicit operator TipoTelefone((string Id, string Key) data) =>
        new(new Guid(data.Id), data.Key);
}
