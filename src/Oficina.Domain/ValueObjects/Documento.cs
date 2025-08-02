using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using Oficina.Domain.Aggregates.UsuarioAggregates;
using Oficina.Domain.SeedWork;

namespace Oficina.Domain.ValueObjects;

public sealed class Documento
{
    private static readonly int[] Multiplicador1 = [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
    private static readonly int[] Multiplicador2 = [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
    public string Numero { get; private set; }
    [NotMapped]
    public TipoDocumento TipoDocumento { get; private set; }
#pragma warning disable CS8618
    private Documento() { }
#pragma warning restore CS8618
    private Documento(string numero, TipoDocumento tipoDocumento)
    {
        TipoDocumento = tipoDocumento;
        Numero = numero;
    }

    public static Result<Documento> Criar(string numero)
    {
        var result = Validar(numero, out TipoDocumento? tipoDocumento);

        if (!result.IsSuccess)
            return result;

        return Result.Success(new Documento(numero, tipoDocumento!));
    }

    private static Result Validar(
        string numero,
        out TipoDocumento? tipoDocumento
    )
    {
        tipoDocumento = null;

        var result = new Result();

        if (string.IsNullOrWhiteSpace(numero))
            result.WithError(Erro.ValorInvalido(nameof(Documento)));

        var n = string.Join("", new Regex(@"\d+").Matches(numero));

        if (n.Length != 11 && n.Length != 16)
            result.WithError(DocumentoErros.ValorInvalido(numero));

        if (result.IsFailed)
            return result;

        if (n.Length == 11)
            tipoDocumento = Aggregates.UsuarioAggregates.TipoDocumento.Cpf;
        else
            tipoDocumento = Aggregates.UsuarioAggregates.TipoDocumento.Cnpj;

        var isValidDocument = IsValidDocument(
            numero,
            tipoDocumento
        );

        if (isValidDocument is false)
            result.WithError(DocumentoErros.ValorInvalido(numero));

        return result;
    }

    private static bool IsValidCpf(string numero)
    {
        if (numero == null) return false;

        var posicao = 0;
        var totalDigito1 = 0;
        var totalDigito2 = 0;
        var dv1 = 0;
        var dv2 = 0;

        bool digitosIdenticos = true;
        var ultimoDigito = -1;

        foreach (var c in numero)
        {
            if (char.IsDigit(c))
            {
                var digito = c - '0';
                if (posicao != 0 && ultimoDigito != digito)
                {
                    digitosIdenticos = false;
                }

                ultimoDigito = digito;
                if (posicao < 9)
                {
                    totalDigito1 += digito * (10 - posicao);
                    totalDigito2 += digito * (11 - posicao);
                }
                else if (posicao == 9)
                {
                    dv1 = digito;
                }
                else if (posicao == 10)
                {
                    dv2 = digito;
                }

                posicao++;
            }
        }

        if (posicao > 11)
        {
            return false;
        }

        if (digitosIdenticos)
        {
            return false;
        }

        var digito1 = totalDigito1 % 11;
        digito1 = digito1 < 2
            ? 0
            : 11 - digito1;

        if (dv1 != digito1)
        {
            return false;
        }

        totalDigito2 += digito1 * 2;
        var digito2 = totalDigito2 % 11;
        digito2 = digito2 < 2
            ? 0
            : 11 - digito2;

        return dv2 == digito2;
    }

    private static bool IsValidCnpj(string numero)
    {
        if (numero == null)
            return false;

        var digitosIdenticos = true;
        var ultimoDigito = -1;
        var posicao = 0;
        var totalDigito1 = 0;
        var totalDigito2 = 0;

        foreach (var c in numero)
        {
            if (char.IsDigit(c))
            {
                var digito = c - '0';
                if (posicao != 0 && ultimoDigito != digito)
                {
                    digitosIdenticos = false;
                }

                ultimoDigito = digito;
                if (posicao < 12)
                {
                    totalDigito1 += digito * Multiplicador1[posicao];
                    totalDigito2 += digito * Multiplicador2[posicao];
                }
                else if (posicao == 12)
                {
                    var dv1 = (totalDigito1 % 11);
                    dv1 = dv1 < 2
                        ? 0
                        : 11 - dv1;

                    if (digito != dv1)
                        return false;

                    totalDigito2 += dv1 * Multiplicador2[12];
                }
                else if (posicao == 13)
                {
                    var dv2 = (totalDigito2 % 11);

                    dv2 = dv2 < 2
                        ? 0
                        : 11 - dv2;

                    if (digito != dv2)
                        return false;
                }

                posicao++;
            }
        }

        return (posicao == 14) && !digitosIdenticos;
    }

    private static bool IsValidDocument(string numero, TipoDocumento tipoDocumento)
    {
        if (tipoDocumento.Id == Aggregates.UsuarioAggregates.TipoDocumento.Cnpj.Id)
            return IsValidCnpj(numero);

        return IsValidCpf(numero);
    }

    private static class DocumentoErros
    {
        public static Erro CpfQuantidadeCaracteresInvalido =>
            Erro.Validacao(
                "Documento.QuantidadeCaracteresInvalido",
                "O documento tipo Cpf deve conter 11 caracteres");

        public static Erro CnpjQuantidadeCaracteresInvalido =>
            Erro.Validacao(
                "Documento.QuantidadeCaracteresInvalido",
                "O documento tipo Cnpj deve conter 16 caracteres");

        public static Erro ValorInvalido(string numero) =>
            Erro.Validacao(
                "Documento.ValorInvalido",
                $"O {numero} informado esta inválido");
    }
}