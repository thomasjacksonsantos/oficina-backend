using Oficina.Domain.Enumerations;

namespace Oficina.Domain.Aggregates.UsuarioAggregates;

public record Sexo : DadoDominio
{
    private Sexo(Guid id, string key) : base(id, key) { }
    private Sexo() { }
    public static Sexo Get(string key) =>
        key.ToLower() switch
        {
            "masculino" => Masculino,
            "feminino" => Feminino,
            _ => throw new NotImplementedException("Sexo não encontrado.")
        };

    public static readonly Sexo Masculino = ("01963629-5d16-75e4-a596-295d8ccd46fa", "Masculino");
    public static readonly Sexo Feminino = ("01963629-7cfa-7cb2-9607-c4ac227dda6c", "Feminino");

    public static implicit operator Sexo((string Id, string Key) data) =>
        new(new Guid(data.Id), data.Key);
}
