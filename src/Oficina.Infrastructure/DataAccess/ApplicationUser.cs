using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.Extensions.Options;

namespace Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser<int>
{
    public string FullName { get; private set; }

#pragma warning disable CS8618
    public ApplicationUser() { }
#pragma warning restore CS8618

    public ApplicationUser(
        string nomeCompleto,
        string email
    )
    {
        if (string.IsNullOrWhiteSpace(nomeCompleto))
            throw new ArgumentNullException(nameof(nomeCompleto));

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentNullException(nameof(email));

        var mail = new EmailAddressAttribute();
        if (!mail.IsValid(email))
            throw new ArgumentException("Invalid email", email);

        FullName = nomeCompleto;
        UserName = email;
        Email = email;
    }

    public static ApplicationUser Criar(
        string username,
        string email
    )
        => new ApplicationUser(username, email);
}

public class ApplicationRole : IdentityRole<int>
{
}

public class ApplicationUserRole : IdentityUserRole<int>
{
}

public class ApplicationUserClaim : IdentityUserClaim<int>
{
}

public class CustomUserClaimsPrincipalFactory
    : UserClaimsPrincipalFactory<ApplicationUser, ApplicationRole>
{
    public CustomUserClaimsPrincipalFactory(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        IOptions<IdentityOptions> optionsAccessor)
        : base(userManager, roleManager, optionsAccessor)
    {
    }

    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
    {
        var identity = await base.GenerateClaimsAsync(user);

        // Adicionar claims personalizadas
        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
        identity.AddClaim(new Claim(ClaimTypes.Email, user.Email!));

        return identity;
    }
}