using Accounts.Data.Context;
using Management.IdentityServer.Configs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Management.IdentityServer.Extensions;

public static class ServiceCollectionExtensions
{
    public static void SetupIdentityServer(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("MSSQL");
        
        builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
        
        builder.Services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
        
        builder.Services.AddIdentityServer()
            .AddDeveloperSigningCredential()
            .AddInMemoryIdentityResources(IdentityServerConfig.IdentityResources)
            .AddInMemoryApiScopes(IdentityServerConfig.Scopes)
            .AddInMemoryApiResources(IdentityServerConfig.Apis)
            .AddInMemoryClients(IdentityServerConfig.Clients)
            .AddAspNetIdentity<IdentityUser>();
    }
}