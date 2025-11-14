

namespace Oficina.App.Api.Features.Usuarios.ValidarDocumento;

public sealed record UpsertUsuarioResponse(
    bool DocumentoValido,
    string Mensagem
);
