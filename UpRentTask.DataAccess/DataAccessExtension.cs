using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UpRentTask.DataAccess.Services;

namespace UpRentTask.DataAccess;

public static class DataAccessExtension
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")))
            .AddScoped<IUserService, UserService>()
            .AddScoped<IRoleService, RoleService>();
        
        return services;
    }
}