

namespace Oficina.App.Api.Features.Usuarios.ValidarEmailExistente;

public sealed record ValidarEmailExistenteResponse(
    bool EmailExistente,
    string Mensagem
);
