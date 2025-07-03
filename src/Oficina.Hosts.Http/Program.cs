using Oficina.App.Api;
using Oficina.Infrastructure;
using FastEndpoints;
using NSwag;
using NSwag.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Oficina.App.Api.Shared;
using NSwag.Generation.Processors.Security;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;

services
    .AddApiModule()
    .AddInfrastructureModule(
        builder.Configuration
    );

services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        var apiConfig = builder.Configuration.GetSection("AppSettings");
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration.GetSection("AppSettings:AuthenticationToken:Issuer").Value,
            ValidAudience = builder.Configuration.GetSection("AppSettings:AuthenticationToken:Aud").Value,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:AuthenticationToken:SecretKey").Value!))
        };
    });

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

services.AddOpenApi();
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
        policy.WithOrigins("http://localhost:5174", "https://portal-Oficina-dev.azurewebsites.net")
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
    app.ApplyMigrations();
}

app
    .UseAuthentication()
    .UseAuthorization()
    .UseFastEndpoints(o =>
    {
        o.Endpoints.RoutePrefix = "api";
        o.Endpoints.Configurator = ep =>
        {
            ep.DontAutoSendResponse();
            ep.PostProcessor<GlobalResultResponseSender>(Order.Before);
        };
    })
    .UseCors("AllowAll");

    if (app.Environment.IsDevelopment() || 
        app.Environment.EnvironmentName == "Local")
    {
        app.MapOpenApi();
        app.UseSwaggerUi(settings =>
        {
            settings.SwaggerRoutes.Add(new SwaggerUiRoute("Oficina Api V1", "/openapi/v1.json"));
            settings.OperationsSorter = "method";
            settings.TagsSorter = "alpha";
        });    
    }


app.Run();