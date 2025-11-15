using Oficina.Domain.Aggregates.ContaAggregates;
using Oficina.Domain.Aggregates.LojaAggregates;
using Oficina.Domain.SeedWork;

namespace Oficina.Domain.Aggregates.UsuarioAggregates
{
    public class UsuarioContexto
    {
        public int Id { get; private set; }
        public int UsuarioId { get; private set; }
        public Usuario? Usuario { get; private set; }
        public int ContaId { get; private set; }
        public Conta? Conta { get; private set; }
        public int LojaId { get; private set; }
        public Loja? Loja { get; private set; }
        // public List<Permissao> Permissoes { get; set; }

#pragma warning disable CS8618
        private UsuarioContexto() { }
#pragma warning restore CS8618

        private UsuarioContexto(
            int usuarioId,
            int contaId,
            int lojaId
        )
        {
            UsuarioId = usuarioId;
            ContaId = contaId;
            LojaId = lojaId;
        }

        public void AtualizarLojaPrincipal(
            int lojaId
        )
        {
            LojaId = lojaId;
        }

        public static Result<UsuarioContexto> Criar(
            int usuarioId,
            int contaId,
            int lojaId
        )
        {
            var result = new Result<UsuarioContexto>();

            if (usuarioId <= 0)
                result.WithError(Erro.ValorInvalido(
                    "UsuarioContexto.UsuarioId",
                    "O ID do usuário é inválido."
                ));

            if (contaId <= 0)
                result.WithError(Erro.ValorInvalido(
                    "UsuarioContexto.ContaId",
                    "O ID da conta é inválido."
                ));

            if (lojaId <= 0)
                result.WithError(Erro.ValorInvalido(
                    "UsuarioContexto.LojaId",
                    "O ID da loja é inválido."
                ));

            if (result.IsFailed)
                return result;

            return new UsuarioContexto(
                usuarioId,
                contaId,
                lojaId
            );
        }
    }
}
