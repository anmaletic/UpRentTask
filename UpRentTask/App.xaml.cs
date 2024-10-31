
using Microsoft.EntityFrameworkCore;
using UpRentTask.DataAccess.Context;
using UpRentTask.DataAccess.Services;

namespace UpRentTask
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            ConfigureServices();
        }
        
        private void ConfigureServices()
        {
            var config = AddConfiguration();
            
            Ioc.Default.ConfigureServices(new ServiceCollection()
                .AddSingleton(config)
                
                .AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(config.GetConnectionString("DefaultConnection")))
                .AddScoped<IUserService, UserService>()
                
                .AddTransient<MainViewModel>()
                .AddTransient<UsersViewModel>()
                .AddTransient<EditUsersViewModel>()
                
                .BuildServiceProvider());
        }
        
        private IConfiguration AddConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
        
            return builder.Build();
        }
    }

}
