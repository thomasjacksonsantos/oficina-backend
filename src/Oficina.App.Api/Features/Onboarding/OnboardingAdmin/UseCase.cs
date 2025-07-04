

using Oficina.Domain.Aggregates.UsuarioAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Domain.ValueObjects;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.App.Api.Features.Onboarding.OnboardingAdmin;

public sealed class UseCase(
    IRepository<SuperAdmin> superAdminRepository,
    IUnitOfWork unitOfWork
)
    : IUseCase<OnboardingAdminRequest, OnboardingAdminResponse>
{
    public async Task<Result<OnboardingAdminResponse>> Execute(
        OnboardingAdminRequest input,
        CancellationToken ct = default
    )
    {
        var superAdmin = SuperAdmin.Criar(
            input.UserId,
            input.Nome,
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
            return Result.Fail(superAdmin.Errors!);

        await superAdminRepository.AddAsync(superAdmin.Value!);
        await unitOfWork.SaveChangesAsync(ct);

        return Result.Success();
    }
}
