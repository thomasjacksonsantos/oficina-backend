
using ImageMagick;
using Tesseract;
using Oficina.Domain.Aggregates.PdfAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Infrastructure.Core;

namespace Oficina.App.Api.Features.Pdfs.TranslatePdf;

public sealed class UseCase(
// IRepository<PdfTranslate> repository
)
    : IUseCase<PdfTranslateRequest, PdfTranslateResponse>
{
    public async Task<Result<PdfTranslateResponse>> Execute(
        PdfTranslateRequest input,
        CancellationToken ct = default
    )
    {
        if (input.File == null || input.File.Length == 0)
            return Result.Fail([Erro.ValorNaoInformado(nameof(input.File))]);

        string fileName = Path.GetFileNameWithoutExtension(input.File.FileName);

        using var memoryStream = new MemoryStream();

        await input.File.CopyToAsync(memoryStream);

        using var images = new MagickImageCollection();

        // Lê o PDF diretamente do stream
        images.Read(memoryStream.ToArray());

        int pageNumber = 1;

        var pdfRoot = PdfRoot.Create(fileName, images.Count);

        var tessDataPath = Path.Combine(Directory.GetCurrentDirectory(), "tessdata");

        foreach (var image in images)
        {
            image.Format = MagickFormat.Png;

            using (var imageStream = new MemoryStream())
            {
                image.Write(imageStream);

                using var engine = new TesseractEngine(tessDataPath, "eng", EngineMode.Default);
                {
                    imageStream.Position = 0; // Resetar o stream para leitura

                    using var pix = Pix.LoadFromMemory(imageStream.ToArray());

                    using var result = engine.Process(pix);

                    var extractedText = result.GetText();

                    var page = PageConverted.Create(
                        pageNumber,
                        extractedText,
                        imageStream.ToArray()
                    );

                    pdfRoot.AddPageConverted(page);
                }
            }

            pageNumber++;
        }

        // await _dbContext.SaveChangesAsync();



        await Task.CompletedTask;
        return Result.Success();
    }
}
