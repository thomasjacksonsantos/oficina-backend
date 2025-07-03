using Oficina.Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Oficina.Infrastructure.DomainImplementation;
using Oficina.Infrastructure.IO;
using Oficina.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Oficina.Infrastructure;

public static class DependencyBuilderModule
{
    public static IServiceCollection AddInfrastructureModule
    (
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext(configuration);
        services.AddRepositories();
        services.AddClients(configuration);
        services.AddConfigurations(configuration);
        services.AddIO();
        return services;
    }

    private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("Database")));

        /// slice is configuration Identity AspNet Core
        services.AddIdentity<ApplicationUser, ApplicationRole>(c =>
        {
            // c.Password.RequireNonAlphanumeric = false;
            c.Password.RequiredLength = 8;
            c.Password.RequireLowercase = false;
            c.Password.RequireUppercase = false;
            c.Password.RequiredUniqueChars = 0;
            c.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            c.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

        return services;
    }

    private static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ApiConfig>(configuration.GetSection("AppSettings"));
        services.AddSingleton(c => c.GetService<IOptions<ApiConfig>>()!.Value);

        return services;
    }

    private static IServiceCollection AddClients(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient(configuration.GetSection("AppSettings:Cep:ServiceName").Value!, c => c.BaseAddress = new Uri(
            configuration.GetSection("AppSettings:Cep:Url").Value!
        ));

        return services;
    }

    public static async Task SeedSystemUser(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var scopedServiceProvider = scope.ServiceProvider;

        var context = scopedServiceProvider.GetRequiredService<ApplicationDbContext>();

        // Obtém o UserManager do serviço
        var userManager = scopedServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scopedServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

        // Defina os detalhes do usuário do sistema
        var user = ApplicationUser.Criar(
            "superadmin",
            "superadmin@PTCode.com.br"
        );

        // Verifique se o usuário já existe
        if (await userManager.FindByEmailAsync(user.Email!) == null)
        {
            // Cria o usuário com uma senha
            var result = await userManager.CreateAsync(user, "careapath2025@");

            if (result.Succeeded)
            {
                Console.WriteLine("Usuário do sistema criado com sucesso!");
            }
            else
            {
                Console.WriteLine("Erro ao criar o usuário:");
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"- {error.Description}");
                }
            }
        }
        else
        {
            Console.WriteLine("O usuário do sistema já existe.");
        }
    }
}