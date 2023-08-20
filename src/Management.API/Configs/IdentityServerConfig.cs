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
                ClientName = "Frontend",

                AllowedGrantTypes = GrantTypes.Code,
                
                // For debug only. Must be stored in a secured storage
                ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                AllowedScopes = { "managementAPI" }
            }
        };
}