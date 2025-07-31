


using Oficina.Domain.SeedWork;
using Oficina.Domain.ValueObjects;

namespace Oficina.Domain.Aggregates.UsuarioAggregates;

public sealed class SuperAdmin : Usuario
{

#pragma warning disable CS8618
    public SuperAdmin() : base() { }
#pragma warning restore CS8618

    public SuperAdmin(
        string userId,
        string nome,
        Documento documento,
        Sexo sexo,
        ICollection<Contato> contatos,
        DataNascimento dataNascimento,
        Endereco endereco
    ) : base(
        userId,
        nome,
        documento,
        TipoUsuario.SuperAdmin,
        sexo,
        dataNascimento,
        contatos,
        endereco
    )
    { }

    public static Result<SuperAdmin> Criar(
        string userId,
        string nome,
        string documento,
        string sexo,
        DateTime dataNascimento,    
        ICollection<Contato> contatos,
        Endereco endereco
    )
    {
        var resultUsuario = Usuario.Criar(
            userId,
            nome,
            TipoUsuario.SuperAdmin.ToString(),
            documento,
            sexo,
            dataNascimento,
            contatos,
            endereco
        );

        if (resultUsuario.IsFailed)
            return Result.Fail(resultUsuario.Errors!);

        var superAdmin = resultUsuario.Value as SuperAdmin;

        return Result.Success(superAdmin!);
    }
}