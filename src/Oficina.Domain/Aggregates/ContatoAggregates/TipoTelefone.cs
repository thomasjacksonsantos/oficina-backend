
using Oficina.Domain.Enumerations;

namespace Oficina.Domain.Aggregates.UsuarioAggregates;

public record TipoTelefone : DadoDominio
{
    private TipoTelefone(Guid id, string key) : base(id, key) { }
    private TipoTelefone() { }

    public static TipoTelefone? Get(string key) =>
        key.ToLower() switch
        {
            "telefone" => Telefone,
            "celular" => Celular,
            "comercial" => Comercial,
            "residencial" => Residencial,
            _ => null
        };

    public static readonly TipoTelefone Telefone = ("67c3c783-654b-44c5-a07f-24a8b6294709", "Telefone");
    public static readonly TipoTelefone Celular = ("c99a5988-b022-4b68-b640-af5021dc7c8f", "Celular");
    public static readonly TipoTelefone Comercial = ("E2D1A590-5C7C-4F1D-8F1D-3C4F0E5B5E6A", "Comercial");
    public static readonly TipoTelefone Residencial = ("A1F5D6E7-3B2C-4D8E-9F0A-1B2C34D5E6F7", "Residencial");

    public static implicit operator TipoTelefone((string Id, string Key) data) =>
        new(new Guid(data.Id), data.Key);
}
