

namespace Oficina.App.Api.Features.Usuarios.GetUsuarioContexto;

public sealed record GetUsuarioContextoResponse(
    UsuarioContextoResponse Usuario,
    LojaContextoResponse Loja,
    ContaContextoResponse Conta
);

public record UsuarioContextoResponse(
    string Id,
    string Nome
);

public sealed record LojaContextoResponse(
    string Id,
    string Nome
);

public sealed record ContaContextoResponse(
    string Id,
    string Nome
);