
using Oficina.Domain.Enumerations;

namespace Oficina.Domain.Aggregates.UsuarioAggregates;

public record TipoUsuario : DadoDominio
{
    private TipoUsuario(Guid id, string key, string nome, string dominio) : base(id, key, nome, dominio) { }

    public static TipoUsuario? Get(string key) =>
        key.ToLower() switch
        {
            "superadmin" => TipoUsuario.SuperAdmin,
            "funcionario" => TipoUsuario.Funcionario,
            _ => null
        };

    public static readonly TipoUsuario SuperAdmin = ("019606aa-ab94-75ac-9e99-86038e9a66a0", "SuperAdmin", "SuperAdmin", "TipoUsuario");
    public static readonly TipoUsuario Funcionario = ("019606aa-cb80-7529-8187-089720b6c556", "Funcionario", "Funcionario", "TipoUsuario");

    public static implicit operator TipoUsuario((string Id, string Key, string nome, string dominio) data) =>
        new(new Guid(data.Id), data.Key, data.nome, data.dominio);
}
