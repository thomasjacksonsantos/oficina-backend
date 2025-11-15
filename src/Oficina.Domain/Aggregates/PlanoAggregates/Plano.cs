using Oficina.Domain.SeedWork;

namespace Oficina.Domain.Aggregates.PlanoAggregates;

public sealed class Plano
{
    public int Id { get; private set; }
    public string Nome { get; private set; } // Plan Name
    public decimal ValorMensal { get; private set; } // Monthly Price
    public int LimiteLojas { get; private set; } // Store Limit
    public Guid PlanoStatusId { get; private set; }
    public PlanoStatus? PlanoStatus { get; private set; }

#pragma warning disable CS8618
    private Plano() { }
#pragma warning restore CS8618

    private Plano(
        string nome,
        decimal valorMensal,
        int limiteLojas
    )
    {
        Nome = nome;
        ValorMensal = valorMensal;
        LimiteLojas = limiteLojas;
        PlanoStatusId = PlanoStatus.Ativo.Id;
    }

    public static Result<Plano> Criar(
        string nome,
        decimal valorMensal,
        int limiteLojas
    )
    {
        var result = new Result<Plano>();
        if (string.IsNullOrWhiteSpace(nome))
            result.WithError(Erro.ValorInvalido(
                "Plano.Nome",
                "O nome do plano é obrigatório."
            ));
        if (valorMensal <= 0)
            result.WithError(Erro.ValorInvalido(
                "Plano.ValorMensal",
                "O valor mensal do plano deve ser maior que zero."
            ));
        if (limiteLojas < 0)
            result.WithError(Erro.ValorInvalido(
                "Plano.LimiteLojas",
                "O limite de lojas não pode ser negativo."
            ));

        if (result.IsFailed)
            return result;

        return new Plano(
            nome,
            valorMensal,
            limiteLojas
        );
    }
}