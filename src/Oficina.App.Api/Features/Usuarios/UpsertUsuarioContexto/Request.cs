
using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.Usuarios.UpsertUsuarioContexto;

public sealed record UpsertUsuarioContextoRequest(
    UsuarioContextoRequest Usuario,
    LojaContextoRequest Loja,
    ContaContextoRequest Conta
): AuthRequest;

public record UsuarioContextoRequest(
    string Id,
    string Nome
);

public sealed record LojaContextoRequest(
    string Id,
    string Nome
);

public sealed record ContaContextoRequest(
    string Id,
    string Nome
);