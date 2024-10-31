using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
