using Oficina.App.Api.Shared;
using Oficina.Infrastructure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Oficina.App.Api.Features.Pdfs.TranslatePdf;

public class Endpoint(
    IUseCase<PdfTranslateRequest, PdfTranslateResponse> useCase)
    : ResultBaseEndpoint<PdfTranslateRequest, PdfTranslateResponse>(useCase)
{
    public override void Configure()
    {
        Post("v1/pdf/upload");
        AllowFileUploads();
        AllowAnonymous();
        Description(c => c.Accepts<PdfTranslateRequest>()
                .Produces<PdfTranslateResponse>()
                .ProducesProblem(400)
                .WithTags("Pdfs")
            , clearDefaults: false);
    }
}