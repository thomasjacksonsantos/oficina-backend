

using Oficina.Domain.SeedWork;
using Oficina.Domain.ValueObjects;
using Oficina.Infrastructure.Core;

namespace Oficina.App.Api.Features.Usuarios.ValidarDocumento;

public sealed class UseCase(
)
    : IUseCase<UpsertUsuarioRequest, UpsertUsuarioResponse>
{
    public async Task<Result<UpsertUsuarioResponse>> Execute(
        UpsertUsuarioRequest input,
        CancellationToken ct = default
    )
    {

        var documento = Documento.Criar(input.Documento);

        if (documento.IsFailed)
        {
            return Result.Success(new UpsertUsuarioResponse(
                DocumentoValido: false,
                Mensagem: "Documento inválido."
            ));
        }

        return await Task.FromResult(Result.Success(new UpsertUsuarioResponse(
            DocumentoValido: true,
            Mensagem: "Documento válido."
        )));
    }
}
