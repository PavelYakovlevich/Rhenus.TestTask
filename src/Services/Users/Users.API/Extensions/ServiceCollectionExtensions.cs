using Models.Users;
using Users.Contract.Repositories;
using Users.Contract.Services;
using Users.Core.Services;
using Users.Data.Models;
using Users.Data.Repositories;
using Users.Domain.Models;

namespace ManagementApp.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection SetupServices(this IServiceCollection collection)
    {
        return collection.AddTransient<IUserService, UserService>();
    }

    public static IServiceCollection SetupDatabase(this IServiceCollection collection)
    {
        return collection.AddSingleton<IUserRepository, InMemoryUserRepository>();
    }

    public static IServiceCollection SetupMapper(this IServiceCollection collection)
    {
        return collection.AddAutoMapper(config =>
        {
            config.CreateMap<User, UserModel>().ReverseMap();
            config.CreateMap<User, UserDbModel>().ReverseMap();
        });
    }
}