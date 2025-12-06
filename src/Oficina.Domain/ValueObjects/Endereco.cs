
using Oficina.Domain.SeedWork;

namespace Oficina.Domain.ValueObjects;

public sealed class Endereco
{
    public string Pais { get; set; }
    public string Estado { get; set; }
    public string Cidade { get; set; }
    public string Logradouro { get; set; }
    public string Bairro { get; set; }
    public string Complemento { get; set; }
    public Cep Cep { get; set; }
    public string Numero { get; set; }

#pragma warning disable CS8618
    public Endereco() { }
#pragma warning restore CS8618

    private Endereco(
        string pais,
        string estado,
        string cidade,
        string logradouro,
        string bairro,
        string complemento,
        Cep cep,
        string numero
    )
    {
        Pais = pais;
        Estado = estado;
        Cidade = cidade;
        Logradouro = logradouro;
        Bairro = bairro;
        Complemento = complemento;
        Cep = cep;
        Numero = numero;
    }

    public static Result<Endereco> Criar(
        string estado,
        string cidade,
        string logradouro,
        string bairro,
        string complemento,
        string cep,
        string numero
    )
    {
        var result = Validar(
            estado,
            cidade,
            logradouro,
            bairro,
            numero,
            cep
        );

        if (result.IsFailed)
            return result;

        return new Endereco(
            string.Empty,
            estado,
            cidade,
            logradouro,
            bairro,
            complemento,
            Cep.Criar(cep).Value!,
            numero);
    }

    public static Result<Endereco> Criar(
        string pais,
        string estado,
        string cidade,
        string logradouro,
        string bairro,
        string complemento,
        string cep,
        string numero
    )
    {
        if (string.IsNullOrWhiteSpace(pais))
            pais = "Brasil";

        var result = Validar(
            pais,
            estado,
            cidade,
            logradouro,
            bairro,
            numero
        );

        if (result.IsFailed)
            return result;

        return new Endereco(
            pais,
            estado,
            cidade,
            logradouro,
            bairro,
            complemento,
            Cep.Criar(cep).Value!,
            numero);
    }

    private static Result Validar(
        string pais,
        string estado,
        string cidade,
        string logradouro,
        string bairro,
        string numero
    )
    {
        var result = Validar(estado, cidade, logradouro, bairro, numero);

        if (string.IsNullOrWhiteSpace(pais))
            result.WithError(Erro.ValorInvalido($"{nameof(Endereco)}.{nameof(Pais)}", "Valor informado para o País está inválido"));

        return result;
    }

    private static Result Validar(
        string estado,
        string cidade,
        string logradouro,
        string bairro,
        string numero
    )
    {
        var result = new Result();

        if (string.IsNullOrWhiteSpace(estado))
            result.WithError(
                Erro.ValorInvalido($"{nameof(Endereco)}.{nameof(Estado)}", "Valor informado para o Estado está inválido"));

        if (string.IsNullOrWhiteSpace(cidade))
            result.WithError(
                Erro.ValorInvalido($"{nameof(Endereco)}.{nameof(Cidade)}", "Valor informado para a Cidade está inválido"));
        if (string.IsNullOrWhiteSpace(logradouro))
            result.WithError(
                Erro.ValorInvalido($"{nameof(Endereco)}.{nameof(Logradouro)}", "Valor informado para o Logradouro está inválido"));

        if (string.IsNullOrWhiteSpace(bairro))
            result.WithError(
                Erro.ValorInvalido($"{nameof(Endereco)}.{nameof(Bairro)}", "Valor informado para o Bairro está inválido"));
        if (string.IsNullOrWhiteSpace(numero))
            result.WithError(
                Erro.ValorInvalido($"{nameof(Endereco)}.{nameof(Numero)}", "Valor informado para o Número está inválido"));

        return result;
    }
}