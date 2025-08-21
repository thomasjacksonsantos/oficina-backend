


using System.Collections.ObjectModel;
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
        TipoDocumento tipoDocumento,
        Documento documento,
        Sexo sexo,
        Collection<Contato> contatos,
        DataNascimento dataNascimento
    ) : base(
        userId,
        nome,
        tipoDocumento,
        documento,
        TipoUsuario.SuperAdmin,
        sexo,
        dataNascimento,
        contatos
    )
    { }

    public static Result<SuperAdmin> Criar(
        string userId,
        string nome,
        string tipoDocumento,
        string documento,
        string sexo,
        DateTime dataNascimento,    
        Collection<Contato> contatos
    )
    {
        var resultUsuario = Usuario.Criar(
            userId,
            nome,
            TipoUsuario.SuperAdmin.Key,
            tipoDocumento,
            documento,
            sexo,
            dataNascimento,
            contatos
        );

        if (resultUsuario.IsFailed)
            return Result.Fail(resultUsuario.Errors!);

        
        var superAdmin = new SuperAdmin(
            userId,
            nome,
            resultUsuario.Value!.TipoDocumento,
            resultUsuario.Value!.Documento,
            resultUsuario.Value.Sexo,
            contatos,
            resultUsuario.Value.DataNascimento
        );

        return Result.Success(superAdmin!);
    }
}