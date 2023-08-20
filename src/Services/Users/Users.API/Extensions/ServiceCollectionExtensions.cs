using Microsoft.EntityFrameworkCore;
using Users.Contract.Repositories;
using Users.Contract.Services;
using Users.Core.Services;
using Users.Data.Context;
using Users.Data.Models;
using Users.Data.Repositories;
using UserModel = Users.Domain.Models.UserModel;

namespace ManagementApp.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection SetupServices(this IServiceCollection collection)
    {
        return collection.AddTransient<IUserService, UserService>();
    }

    public static IServiceCollection SetupDatabase(this IServiceCollection collection)
    {
        collection.AddDbContext<AppDbContext>(options =>
        {
            options.UseInMemoryDatabase("ManagementApp");
        });
        
        return collection.AddSingleton<IUserRepository, UserRepository>();
    }

    public static IServiceCollection SetupMapper(this IServiceCollection collection)
    {
        return collection.AddAutoMapper(config =>
        {
            config.CreateMap<UserModel, Models.Users.UserModel>().ReverseMap();
            config.CreateMap<UserModel, UserDbModel>().ReverseMap();
        });
    }
}