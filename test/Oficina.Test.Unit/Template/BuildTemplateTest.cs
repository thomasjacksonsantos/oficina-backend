using System.Reflection;
using Oficina.Infrastructure.IO.Templates;

namespace Oficina.Test.Unit.Template;

public class BuildTemplateTest
{
    private string CaminhoTemplate =
        Path.Combine(
            "EnviarSenhaAcesso.html"
        );

    private readonly IBuildTemplate buildTemplate;

    public BuildTemplateTest()
    {
        buildTemplate = new BuildTemplate();
    }

    [Fact]
    public async Task Build_ValidModel_ReturnsRenderedTemplate()
    {
        // Arrange
        var model = new { Email = "thomas.j.santos@gmail.com", Senha = "123456" };

        // Act
        var result = await buildTemplate.Build(model, CaminhoTemplate);

        // Assert
        Assert.NotNull(result);
        Assert.Null(result.erro);
        Assert.Contains("thomas.j.santos@gmail.com", result.template);
        Assert.Contains("123456", result.template);
    }
}