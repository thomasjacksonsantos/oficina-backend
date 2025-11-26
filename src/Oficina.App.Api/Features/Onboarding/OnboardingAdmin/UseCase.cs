

using System.Collections.ObjectModel;
using FirebaseAdmin.Auth;
using Oficina.Domain.Aggregates.ContaAggregates;
using Oficina.Domain.Aggregates.LojaAggregates;
using Oficina.Domain.Aggregates.UsuarioAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Domain.ValueObjects;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;
using Oficina.Infrastructure.IO;

namespace Oficina.App.Api.Features.Onboarding.OnboardingAdmin;

public sealed class UseCase(
    IRepository<Conta> contaRepository,
    IRepository<UsuarioContexto> usuarioContextoRepository,
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
                return Result.Fail(Erro.ValorInvalido("senha"));

            var user = await TryGetUserByEmailAsync(input.Email);

            if (user != null)
                return Result.Fail(Erro.ValorInvalido("Usuário já cadastrado"));

            userRecordArgs = await FirebaseAuth.DefaultInstance.CreateUserAsync(new UserRecordArgs
            {
                Email = input.Email,
                Password = input.Senha,
                DisplayName = input.Nome
            });

            var superAdmin = SuperAdmin.Criar(
                userRecordArgs.Uid,
                input.Nome,
                input.TipoDocumento,
                input.Documento,
                input.Sexo,
                input.DataNascimento,
                new Collection<Contato>(input.Contatos.Select(c => Contato.Criar(c.Numero, c.TipoTelefone).Value).ToList()!)
            );

            if (superAdmin.IsFailed)
                throw new Exception(string.Join("-", superAdmin.Errors!.Select(e => $"{e.Descricao}")));

            var conta = Conta.Criar("Default", true);
            conta.AddUsuario(superAdmin.Value!);

            var enderecoLoja = Endereco.Criar(
                input.Loja.Endereco.Estado,
                input.Loja.Endereco.Cidade,
                input.Loja.Endereco.Logradouro,
                input.Loja.Endereco.Bairro,
                input.Loja.Endereco.Complemento,
                input.Loja.Endereco.Cep,
                input.Loja.Endereco.Numero
            );

            if (enderecoLoja.IsFailed)
                throw new Exception(string.Join("-", enderecoLoja.Errors!.Select(e => $"{e.Descricao}")));

            var contatos = new List<Contato>();

            foreach (var item in input.Loja.Contatos)
            {
                var contato = Contato.Criar(
                    item.Numero,
                    item.TipoTelefone
                );

                if (contato.IsFailed)
                    throw new Exception(string.Join("-", contato.Errors!.Select(e => $"{e.Descricao}")));

                contatos.Add(contato.Value!);
            }

            var loja = Loja.Criar(
                input.Loja.NomeFantasia,
                input.Loja.RazaoSocial,
                input.Loja.InscricaoEstadual,
                input.Loja.Site,
                input.Loja.LogoTipo,
                input.Loja.Cnpj,
                conta,
                enderecoLoja.Value!,
                contatos
            );

            if (loja.IsFailed)
                throw new Exception(string.Join("-", loja.Errors!.Select(e => $"{e.Descricao}")));

            conta.AddLoja(loja.Value!);

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
            
            // Create UsuarioContexto for the super admin
            var usuarioContext = UsuarioContexto.Criar(
                superAdmin.Value!.Id,
                conta.Id,
                loja.Value!.Id
            );

            await usuarioContextoRepository.AddAsync(usuarioContext.Value!);
            await unitOfWork.SaveChangesAsync(ct);

            return Result.Success(new OnboardingAdminResponse(
                Sucesso: true,
                Mensagem: "Usuário criado com sucesso"
            ));
        }
        catch (Exception ex)
        {
            if (userRecordArgs != null)
                FirebaseAuth.DefaultInstance.DeleteUserAsync(userRecordArgs.Uid).Wait();

            return Result.Fail(Erro.ValorInvalido($"Erro ao criar o usuário: {ex.Message}"));
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
