using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Oficina.App.Api.Features.Onboarding.OnboardingAdmin;

public class Endpoint(
    IUseCase<OnboardingAdminRequest, OnboardingAdminResponse> useCase)
    : ResultBaseEndpoint<OnboardingAdminRequest, OnboardingAdminResponse>(useCase)
{
    public override void Configure()
    {
        Post("v1/onboarding/admin");
        Description(c => c.Accepts<OnboardingAdminRequest>()
                .Produces<OnboardingAdminResponse>()
                .ProducesProblem(400)
                .WithTags("Onboarding")
            , clearDefaults: false);
    }
}