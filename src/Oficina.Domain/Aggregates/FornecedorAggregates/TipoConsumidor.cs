using Oficina.Domain.Enumerations;

namespace Oficina.Domain.Aggregates.FornecedorAggregates;

public record TipoConsumidor : DadoDominio
{
private TipoConsumidor() : base() { }

    private TipoConsumidor(Guid id, string key, string nome, string dominio) : base(id, key, nome, dominio) { }

    public static TipoConsumidor Get(string key) =>
        key.ToLower() switch
        {
            "consumidorfinal" => ConsumidorFinal,
            "revenda" => Revenda,
            _ => throw new ArgumentOutOfRangeException($"TipoConsumidor inválido: {key}")
        };

    public static TipoConsumidor Get(Guid id) =>
        id == ConsumidorFinal.Id ? ConsumidorFinal :
        id == Revenda.Id ? Revenda :
        throw new ArgumentOutOfRangeException($"TipoConsumidor inválido: {id}");

    public static readonly TipoConsumidor ConsumidorFinal = ("f3d8f4d2-4fb3-4b8a-9d61-9a3f5f77d3c2", "ConsumidorFinal", "Consumidor Final", "TipoConsumidor");
    public static readonly TipoConsumidor Revenda = ("0c6a2bb1-6eac-4e21-bb94-2c0d35363f9c", "Revenda", "Revenda", "TipoConsumidor");


    public static implicit operator TipoConsumidor((string Id, string Key, string Nome, string Dominio) data) =>
        new(new Guid(data.Id), data.Key, data.Nome, data.Dominio);
}
