

using FirebaseAdmin.Auth;
using Oficina.Domain.Aggregates.ContaAggregates;
using Oficina.Domain.Aggregates.UsuarioAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Domain.ValueObjects;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;
using Oficina.Infrastructure.IO;

namespace Oficina.App.Api.Features.Onboarding.OnboardingAdmin;

public sealed class UseCase(
    IRepository<Conta> contaRepository,
    IEmailSend emailSend,
    IUnitOfWork unitOfWork
)
    : IUseCase<OnboardingAdminRequest, OnboardingAdminResponse>
{
    public async Task<Result<OnboardingAdminResponse>> Execute(
        OnboardingAdminRequest input,
        CancellationToken ct = default
    )
    {
        UserRecord? userRecordArgs = null;

        try
        {
            if (input.Senha != input.ConfirmarSenha)
                return Result.Fail("As senhas não conferem");

            var user = await TryGetUserByEmailAsync(input.Email);

            if (user != null)
                return Result.Fail("Usuário já cadastrado");

            userRecordArgs = await FirebaseAuth.DefaultInstance.CreateUserAsync(new UserRecordArgs
            {
                Email = input.Email.ToLower(),
                Password = input.Senha.ToLower(),
                DisplayName = input.Nome.ToLower()
            });

            var superAdmin = SuperAdmin.Criar(
                userRecordArgs.Uid,
                input.Nome,
                input.TipoDocumento,
                input.Documento,
                input.Sexo,
                input.DataNascimento,
                input.Contatos.Select(c => Contato.Criar(c.DDD, c.Numero, c.TipoTelefone).Value).ToList()!,
                Endereco.Criar(
                    input.Endereco.Pais,
                    input.Endereco.Estado,
                    input.Endereco.Cidade,
                    input.Endereco.Bairro,
                    input.Endereco.Complemento,
                    Cep.Criar(input.Endereco.Cep).Value!,
                    input.Endereco.Numero
                ).Value!
            );

            if (superAdmin.IsFailed)
                throw new Exception(string.Join("-", superAdmin.Errors!.Select(e => $"{e.Descricao}")));

            var conta = Conta.Criar("Default", true);
            conta.AddUsuario(superAdmin.Value!);

            var emailResult = await emailSend.SendAsync(
                new EmailParams(
                    To: input.Email!,
                    Subject: "Bem-vindo ao sistema",
                    Body: $"Olá {superAdmin.Value!.Nome},\n\nSeu cadastro foi realizado com sucesso! \n\nAtenciosamente,\nEquipe Oficina"
                ),
                ct
            );

            if (!emailResult.Success)
                throw new Exception(emailResult.Message);

            await contaRepository.AddAsync(conta);
            await unitOfWork.SaveChangesAsync(ct);

            var token = await FirebaseAuth.DefaultInstance.CreateCustomTokenAsync(userRecordArgs.Uid);

            return Result.Success(new OnboardingAdminResponse(token));
        }
        catch (Exception ex)
        {
            if (userRecordArgs != null)
                FirebaseAuth.DefaultInstance.DeleteUserAsync(userRecordArgs.Uid).Wait();

            return Result.Fail($"Erro ao criar o usuário: {ex.Message}");
        }
    }

    async Task<UserRecord?> TryGetUserByEmailAsync(string email)
    {
        try
        {
            return await FirebaseAuth.DefaultInstance.GetUserByEmailAsync(email);
        }
        catch (FirebaseAuthException ex) when (ex.AuthErrorCode == AuthErrorCode.UserNotFound)
        {
            return null;
        }
    }
}
