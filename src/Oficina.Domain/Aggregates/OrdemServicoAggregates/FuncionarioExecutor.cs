
using Oficina.Domain.Aggregates.UsuarioAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Domain.ValueObjects;

namespace Oficina.Domain.Aggregates.OrdemServicoAggregates;

public class FuncionarioExecutor
{
    public int Id { get; private set; }
    public int UsuarioId { get; private set; }
    public Usuario Usuario { get; private set; }
    public DataHora Criado { get; private set; }
    public DataHora Atualizado { get; private set; }

    #pragma warning disable CS8618
    private FuncionarioExecutor() { }
#pragma warning restore CS8618
    // Construtor com parâmetros
    public FuncionarioExecutor(
        Usuario usuario
    )
    {
        UsuarioId = usuario.Id;
        Usuario = usuario;
        Criado = DataHora.Criar().Value!;
        Atualizado = DataHora.Criar().Value!;
    }

    // Método estático Criar
    public static Result<FuncionarioExecutor> Criar(
        Usuario usuario
    )
    {
        // Adicione validações se necessário
        var funcionario = new FuncionarioExecutor(
            usuario
        );

        return Result.Success(funcionario);
    }
}