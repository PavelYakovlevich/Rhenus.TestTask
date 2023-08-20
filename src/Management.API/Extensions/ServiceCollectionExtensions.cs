using ManagementApp.API.Configs;
using Microsoft.EntityFrameworkCore;
using Accounts.Contract.Repositories;
using Accounts.Contract.Services;
using Accounts.Core.Services;
using Accounts.Data.Context;
using Accounts.Data.Models;
using Accounts.Data.Repositories;
using Accounts.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace ManagementApp.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection SetupServices(this IServiceCollection collection)
    {
        return collection.AddTransient<IAccountService, AccountService>();
    }

    public static IServiceCollection SetupDatabase(this IServiceCollection collection)
    {
        collection.AddDbContext<AppDbContext>(options =>
        {
            options.UseInMemoryDatabase("ManagementApp");
        });
        
        return collection.AddScoped<IAccountRepository, AccountRepository>();
    }

    public static IServiceCollection SetupMapper(this IServiceCollection collection)
    {
        return collection.AddAutoMapper(config =>
        {
            config.CreateMap<AccountModel, Models.Account.AccountModel>().ReverseMap();
            config.CreateMap<AccountModel, Account>().ReverseMap();
            config.CreateMap<AccountRegistrationModel, AccountModel>().ReverseMap();
        });
    }

    public static IServiceCollection SetupAuthentication(this IServiceCollection collection)
    {
        collection.AddAuthentication(options =>
        {
            options.DefaultScheme = "Cookies";
            options.DefaultChallengeScheme = "oidc";
        })
        .AddCookie("Cookies")
        .AddOpenIdConnect("oidc", options =>
        {
            options.Authority = "https://localhost:5001";

            options.ClientId = "frontend";
            options.ClientSecret = "secret";
            options.ResponseType = "code";

            options.Scope.Clear();
            options.Scope.Add("openid");
            options.Scope.Add("profile");

            options.SaveTokens = true; 
        });

        collection.AddIdentityServer()
            .AddInMemoryIdentityResources(IdentityServerConfig.IdentityResources)
            .AddInMemoryApiResources(IdentityServerConfig.Apis)
            .AddInMemoryClients(IdentityServerConfig.Clients)
            .AddAspNetIdentity<IdentityUser>();

        return collection;
    }
}