using IdentityServer4;
using IdentityServer4.Models;

namespace Management.IdentityServer.Configs;

internal static class IdentityServerConfig
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
                
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                
                AllowAccessTokensViaBrowser = true,
                AllowOfflineAccess = true,

                // Must be stored in a secured storage
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                
                AllowedScopes = new List<string>
                {
                    "managementAPI",
                }
            }
        };
    
    public static IEnumerable<ApiScope> Scopes => new []
        {
            new ApiScope("managementAPI"),
        };
}