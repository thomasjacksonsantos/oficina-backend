using Microsoft.AspNetCore.Http;
using Oficina.App.Api.Shared;
using Oficina.Domain.Enums;

namespace Oficina.App.Api.Features.Pdfs.TranslatePdf;

public sealed record PdfTranslateRequest(
    IFormFile File,
    LangEnum Lang    
) : AuthRequest;