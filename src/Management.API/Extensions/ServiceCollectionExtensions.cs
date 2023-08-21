using System.Reflection;
using Accounts.Contract.Repositories;
using Accounts.Contract.Services;
using Accounts.Core.Services;
using Accounts.Data.Context;
using Accounts.Data.Models;
using Accounts.Data.Repositories;
using Accounts.Domain.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using ManagementApp.API.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Models.Account;
using AccountModel = Models.Account.AccountModel;

namespace ManagementApp.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static void SetupServices(this WebApplicationBuilder builder) =>
        builder.Services.AddTransient<IAccountService, AccountService>();

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
            config.CreateMap<CreateAccountModel, AccountRegistrationModel>().ReverseMap();
            config.CreateMap<AccountRegistrationModel, Accounts.Domain.Models.AccountModel>().ReverseMap();
            config.CreateMap<AccountRegistrationModel, IdentityUser>()
                .ForMember(dest => dest.UserName, options =>
                    options.MapFrom(src => src.Email));
        });
    }

    public static void SetupAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.Authority = "https://localhost:7088";

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false
                };
            });
    }

    public static void SetupModelsValidation(this WebApplicationBuilder builder)
    {
        builder.Services.AddValidatorsFromAssemblyContaining<AccountFiltersValidator>();
        builder.Services.AddFluentValidationAutoValidation();
    }
}