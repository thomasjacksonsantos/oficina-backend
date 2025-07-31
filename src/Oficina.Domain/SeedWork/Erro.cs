namespace Oficina.Domain.SeedWork;

public record Erro
{
    private Erro() { }

    private Erro(
        string codigo,
        string descricao,
        TipoEnum tipo) => (Codigo, Descricao, Tipo) = (codigo, descricao, tipo);

    public TipoEnum Tipo { get; set; }
    public string Codigo { get; set; } = null!;
    public string Descricao { get; set; } = null!;

    public static Erro Validacao(
        string codigo,
        string descricao) =>
        new(codigo, descricao, TipoEnum.Validacao);

    public static Erro NaoEncontrado(
        string entity) =>
        new(
            $"{entity}.NaoEncontrado",
            $"{entity} não encontrado(a)",
            TipoEnum.NaoEncontrado);

    public static Erro ValorNaoInformado(string campo) =>
        Validacao(
            $"{campo}.ValorNaoInformado",
            $"Valor da(o) {campo} não informado(a)");

    public static Erro ValorInvalido(string campo) =>
        Validacao(
            $"{campo}.ValorInvalido",
            $"Valor informado para o {campo} está inválido");
    public static Erro Error(string codigo, string message) =>
        Validacao(
            codigo,
            message);
    public enum TipoEnum
    {
        Validacao,
        NaoEncontrado,

    }
}