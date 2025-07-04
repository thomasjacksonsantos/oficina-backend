using System.Text.Json.Serialization;

namespace Oficina.App.Api.Shared;

public record AuthRequest
{
    [JsonIgnore]
    public string UserId { get; set; } = string.Empty;
    [JsonIgnore]
    public string? UserName { get; set; }
    [JsonIgnore]
    public string? Email { get; set; }
}

public record UsuarioPerfilRequest(
   int UsuarioId,
   string TipoUsuario
);