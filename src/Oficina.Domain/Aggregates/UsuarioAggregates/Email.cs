using System.ComponentModel.DataAnnotations;
using Oficina.Domain.SeedWork;

namespace Oficina.Domain.ValueObjects;

public sealed class Email
{
    public string Valor { get; set; }
    public bool Principal { get; set; }

#pragma warning disable CS8618
    public Email() { }
#pragma warning restore CS8618

    private Email(
        string valor,
        bool principal
    )
    {
        Valor = valor;
        Principal = principal;
    }

    public static Result<Email> Criar(
        string valor
    )
        => Criar(valor, false);

    public static Result<Email> Criar(
        string valor,
        bool principal
    )
    {
        var result = Validar(valor);

        if (result.IsFailed)
            return result;

        return new Email(valor, principal);
    }

    private static Result Validar(
        string valor
    )
    {
        var result = new Result();

        if (string.IsNullOrWhiteSpace(valor))
            result.WithError(Erro.ValorNaoInformado(nameof(Email)));

        // Caso seja nulo nao faz sentido validar se o email é válido.
        if (result.IsFailed)
            return result;

        var valid = new EmailAddressAttribute();

        if (!valid.IsValid(valor))
            result.WithError(Erro.ValorInvalido(nameof(Email)));

        return result;
    }
}