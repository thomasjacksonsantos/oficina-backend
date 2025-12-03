using Oficina.Domain.Aggregates.ContaAggregates;
using Oficina.Domain.Aggregates.LojaAggregates;
using Oficina.Domain.SeedWork;

namespace Oficina.Domain.Aggregates.UsuarioAggregates
{
    public class UsuarioContexto
    {
        public int Id { get; private set; }
        public int UsuarioId { get; private set; }
        public Usuario Usuario { get; private set; }
        public int ContaId { get; private set; }
        public Conta Conta { get; private set; }
        public int LojaId { get; private set; }
        public Loja Loja { get; private set; }
        // public List<Permissao> Permissoes { get; set; }

#pragma warning disable CS8618
        private UsuarioContexto() { }
#pragma warning restore CS8618

        private UsuarioContexto(
            Usuario usuario,
            Conta conta,
            Loja loja
        )
        {
            UsuarioId = usuario.Id;
            Usuario = usuario;
            ContaId = conta.Id;
            LojaId = loja.Id;
            Conta = conta;
            Loja = loja;
        }

        public void AtualizarLojaPrincipal(
            int lojaId
        )
        {
            LojaId = lojaId;
        }

        public static Result<UsuarioContexto> Criar(
            Usuario usuario,
            Conta conta,
            Loja loja
        )
        {
            var result = new Result<UsuarioContexto>();

            if (usuario.Id <= 0)
                result.WithError(Erro.ValorInvalido(
                    "UsuarioContexto.UsuarioId",
                    "O ID do usuário é inválido."
                ));

            if (conta.Id <= 0)
                result.WithError(Erro.ValorInvalido(
                    "UsuarioContexto.ContaId",
                    "O ID da conta é inválido."
                ));

            if (loja.Id <= 0)
                result.WithError(Erro.ValorInvalido(
                    "UsuarioContexto.LojaId",
                    "O ID da loja é inválido."
                ));

            if (result.IsFailed)
                return result;

            return new UsuarioContexto(
                usuario,
                conta,
                loja
            );
        }
    }
}
