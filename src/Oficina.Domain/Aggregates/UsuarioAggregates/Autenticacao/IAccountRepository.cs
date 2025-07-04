
using Oficina.Domain.Aggregates.UsuarioAggregates.Autenticacao;
using Oficina.Domain.SeedWork;

namespace Oficina.Domain.Aggregates.AuthAggregates;

public interface IAccountRepository
{
    ValueTask<Result<int>> CriarUserApplicationAsync(string nome, string email, string senha);

    ValueTask<string> LoginAsync(string email, string senha);
    ValueTask DeletarAsync(int Id);
    ValueTask AtualizarSenha(int userId, string senha);
    ValueTask<UserApplication?> FindByEmailAsync(string email);

}