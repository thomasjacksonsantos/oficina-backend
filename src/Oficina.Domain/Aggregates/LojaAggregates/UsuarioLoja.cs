

using Oficina.Domain.ValueObjects;

namespace Oficina.Domain.Aggregates.LojaAggregates;

public sealed class UsuarioLoja
{
    public int Id { get; private set; }
    public int UsuarioId { get; private set; }
    public DataHora Criado { get; private set; }
    public DataHora Atualizado { get; private set; }

    public UsuarioLoja(
        int usuarioId
    )
    {
        if (usuarioId <= 0)
            throw new ArgumentOutOfRangeException($"O ${nameof(usuarioId)} informado está inválido id:{usuarioId}");

        UsuarioId = usuarioId;
        Criado = DateTime.Now;
        Atualizado = DateTime.Now;
    }
}