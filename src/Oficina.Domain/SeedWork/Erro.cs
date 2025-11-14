namespace Oficina.Domain.SeedWork;

public record Erro
{
    private Erro() { }

    private Erro(
        string campo,
        string codigo,
        string descricao,
        TipoEnum tipo) => (Campo, Codigo, Descricao, Tipo) = (campo,codigo, descricao, tipo);

    public TipoEnum Tipo { get; set; }
    public string Campo { get; set; } = null!;
    public string Codigo { get; set; } = null!;
    public string Descricao { get; set; } = null!;

    public static Erro Validacao(
        string campo,
        string codigo,
        string descricao) =>
        new(campo, codigo, descricao, TipoEnum.Validacao);

    public static Erro NaoEncontrado(
        string entity) =>
        new(
            entity,
            $"{entity}.NaoEncontrado",
            $"{entity} não encontrado(a)",
            TipoEnum.NaoEncontrado);

    public static Erro NaoEncontrado(
        string entity,
        string valor) =>
        new(entity,
            $"{entity}.NaoEncontrado",
            $"{entity}({valor}) não encontrado(a)",
            TipoEnum.NaoEncontrado);

    public static Erro ValorNaoInformado(string campo) =>
        Validacao(
            campo,
            $"{campo}.ValorNaoInformado",
            $"Valor da(o) {campo} não informado(a)");

    public static Erro ValorInvalido(string campo) =>
        Validacao(
            campo,
            $"{campo}.ValorInvalido",
            $"Valor informado para o {campo} está inválido");

    public static Erro RegistroJaExcluido(string entity) =>
        Validacao(
            entity,
            $"{entity}.RegistroJaExcluido",
            $"Registro de {entity} já está excluído");

    public enum TipoEnum
    {
        Validacao,
        NaoEncontrado,

    }
}