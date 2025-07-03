using System.Reflection;
using Fluid;

namespace Oficina.Infrastructure.IO.Templates;

public record TemplateResponse(string template, string? erro = null);

public interface IBuildTemplate
{
    ValueTask<TemplateResponse> Build<T>(T model, string caminhoTemplate);
}

public class BuildTemplate : IBuildTemplate
{
    private static readonly FluidParser parse = new FluidParser();

    public ValueTask<TemplateResponse> Build<T>(T model, string caminhoTemplate)
    {
        var source = File.ReadAllText(
            Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, 
            caminhoTemplate)
        );

        if (parse.TryParse(source, out var template, out var erro))
        {
            var options = new TemplateOptions();
            options.MemberAccessStrategy.Register(model!.GetType());

            var context = new TemplateContext(model, options);
            context.SetValue("model", model);

            var render = template.Render(context);
            return ValueTask.FromResult(new TemplateResponse(render));
        }
        return ValueTask.FromResult(
            new TemplateResponse(template: string.Empty, erro: erro)
        );
    }
}