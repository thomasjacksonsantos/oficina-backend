using Oficina.App.Api;
using Oficina.Infrastructure;
using FastEndpoints;
using NSwag;
using NSwag.AspNetCore;
using Oficina.App.Api.Shared;
using NSwag.Generation.Processors.Security;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Oficina.Hosts.Http.Firebase;
using Oficina.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;

services.AddSingleton(
    FirebaseApp.Create(new AppOptions
    {
        Credential = GoogleCredential.FromFile(
            Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Firebase",
                "firebase-config.json"
            )
        ),
    })
);

services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddScheme<AuthenticationSchemeOptions, FirebaseAuthenticationHandler>(
        JwtBearerDefaults.AuthenticationScheme,
        (o) => { }
    );

services
    .AddApiModule()
    .AddInfrastructureModule(
        builder.Configuration
    );

services
    .AddAuthorization()
    .AddFastEndpoints(o =>
    {
        o.DisableAutoDiscovery = true;
        o.Assemblies = [AssemblyInfo.GetApiAssembly()];
    });

services.ConfigureHttpJsonOptions(c =>
{
    c.SerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    c.SerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
});

services.AddOpenApiDocument(document =>
{
    document.PostProcess = doc =>
    {
        doc.Info = new OpenApiInfo
        {
            Title = "Oficina Api",
            Version = "V1",
            Description = "Api Oficina",
            Contact = new OpenApiContact
            {
                Name = "Thomas Jackson",
                Url = "https://www.linkedin.com/in/thomasjacksonsantos/"
            }
        };
    };

    document.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("bearer"));
});

services.AddEndpointsApiExplorer();

services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://localhost:3002")
             .AllowAnyMethod()
             .AllowAnyHeader()
             .AllowCredentials();
    });
});

services.AddHttpContextAccessor();

WebApplication app = builder.Build();

if (!app.Environment.IsProduction())
{
    app.UseDeveloperExceptionPage();
}

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

app
    .UseAuthentication()
    .UseAuthorization()
    .UseOpenApi()
    .UseFastEndpoints(o =>
    {
        o.Endpoints.RoutePrefix = "api";
        o.Endpoints.Configurator = ep =>
        {
            ep.DontAutoSendResponse();
            ep.PostProcessor<GlobalResultResponseSender>(Order.Before);
        };
    })
    .UseCors("AllowAll")
    .UseSwaggerUi(settings =>
    {
        settings.SwaggerRoutes.Add(new SwaggerUiRoute("Oficina Api V1", "/swagger/v1/swagger.json"));
        settings.OperationsSorter = "method";
        settings.TagsSorter = "alpha";
    })
    .UseReDoc(options =>
    {
        options.Path = "/redoc";
    });

if (app.Environment.IsDevelopment() ||
    app.Environment.EnvironmentName == "Local")
{

    app.UseSwaggerUi(settings =>
    {
        settings.SwaggerRoutes.Add(new SwaggerUiRoute("Oficina Api V1", "/openapi/v1.json"));
        settings.OperationsSorter = "method";
        settings.TagsSorter = "alpha";
    });
}


app.Run();