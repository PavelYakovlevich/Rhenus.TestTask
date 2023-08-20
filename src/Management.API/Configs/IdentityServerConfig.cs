using IdentityServer4;
using IdentityServer4.Models;

namespace ManagementApp.API.Configs;

public static class IdentityServerConfig
{
    public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiResource> Apis  => new []
        {
            new ApiResource("managementAPI", "Management API")
        };

    public static IEnumerable<Client> Clients => new[]
        {
            new Client
            {
                ClientId = "frontend",
                ClientSecrets = { new Secret("secret".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,
            
                RedirectUris = { "https://localhost:7296/signin-oidc" },

                PostLogoutRedirectUris = { "https://localhost:7296/signout-callback-oidc" },

                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                }
            }
        };
    
    public static IEnumerable<ApiScope> Scopes => new []
        {
            new ApiScope("openid"),
            new ApiScope("profile"),
            new ApiScope("email"),
            new ApiScope("read"),
            new ApiScope("write"),
            new ApiScope("identity-server-demo-api")
        };
}