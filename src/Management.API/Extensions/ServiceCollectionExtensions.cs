using System.Reflection;
using Accounts.Contract.Repositories;
using Accounts.Contract.Services;
using Accounts.Core.Services;
using Accounts.Data.Context;
using Accounts.Data.Models;
using Accounts.Data.Repositories;
using Accounts.Domain.Models;
using Auth.Contract.Services;
using Auth.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.Account;
using AccountModel = Models.Account.AccountModel;

namespace ManagementApp.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static void SetupServices(this WebApplicationBuilder builder) => 
        builder.Services.AddTransient<IAccountService, AccountService>()
            .AddTransient<IAuthenticationService, AuthenticationService>();

    public static void SetupDatabase(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("MSSQL");
        
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(connectionString, optionsBuilder =>
            {
                optionsBuilder.MigrationsAssembly(typeof(AppDbContext).GetTypeInfo().Assembly.GetName().Name);
                optionsBuilder.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), null);
            });
        });
        
        builder.Services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
        
        builder.Services.AddTransient<IAccountRepository, AccountRepository>();
    }

    public static void SetupMapper(this WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(config =>
        {
            config.CreateMap<AccountModel, Accounts.Domain.Models.AccountModel>().ReverseMap();
            config.CreateMap<Accounts.Domain.Models.AccountModel, Account>().ReverseMap();
            config.CreateMap<AccountCreationModel, AccountRegistrationModel>().ReverseMap();
            config.CreateMap<AccountRegistrationModel, Accounts.Domain.Models.AccountModel>().ReverseMap();
            config.CreateMap<AccountRegistrationModel, IdentityUser>()
                .ForMember(dest => dest.UserName, options =>
                    options.MapFrom(src => src.Email));
        });
    }

    public static void SetupAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication()
            .AddJwtBearer(options =>
            {
                // Temporary
                options.Authority = "https://localhost:7088";
            });
    }
}