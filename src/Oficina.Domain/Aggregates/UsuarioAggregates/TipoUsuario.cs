
using Oficina.Domain.Enumerations;

namespace Oficina.Domain.Aggregates.UsuarioAggregates;

public record TipoUsuario : DadoDominio
{
    private TipoUsuario(Guid id, string key) : base(id, key) { }

    public static TipoUsuario? Get(string key) =>
        key.ToLower() switch
       {
           "superadmin" => TipoUsuario.SuperAdmin,
           "funcionario" => TipoUsuario.Funcionario,
           _ => null
       };
    
    public static readonly TipoUsuario SuperAdmin = ("019606aa-ab94-75ac-9e99-86038e9a66a0", "SuperAdmin");
    public static readonly TipoUsuario Funcionario = ("019606aa-cb80-7529-8187-089720b6c556", "Funcionario");    
    public static implicit operator TipoUsuario((string Id, string Key) data) =>
        new(new Guid(data.Id), data.Key);
}
