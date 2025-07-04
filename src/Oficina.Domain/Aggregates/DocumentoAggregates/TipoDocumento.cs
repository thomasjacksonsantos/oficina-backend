
using Oficina.Domain.Enumerations;

namespace Oficina.Domain.Aggregates.UsuarioAggregates;

public record TipoDocumento : DadoDominio
{
    private TipoDocumento(Guid id, string key) : base(id, key) { }

    public static TipoDocumento? Get(string key) =>
        key.ToLower() switch
        {
            "cpf" => Cpf,
            "cnpj" => Cnpj,
            "rg" => Rg,
            _ => null
        };

    public static readonly TipoDocumento Cpf = ("10132080-452d-4d7d-861e-0c5801c94d57", "Cpf");
    public static readonly TipoDocumento Cnpj = ("258f6580-0046-4b1e-9884-8757da63a48e", "Cnpj");
    public static readonly TipoDocumento Rg = ("0c4df770-8401-4dc7-aebb-338b70ac349a", "Rg");    
    public static implicit operator TipoDocumento((string Id, string Key) data) =>
        new(new Guid(data.Id), data.Key);
}
