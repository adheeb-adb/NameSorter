using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NameSorter.Domain.Contracts.Application;
using NameSorter.Domain.Contracts.Helper;
using NameSorter.Domain.Contracts.Sort;
using NameSorter.Domain.Models;
using NameSorter.Services.Helper;
using NameSorter.Services.Sort;
using System;
using System.IO;
using System.Threading.Tasks;

namespace NameSorter.Application
{
    class Program
    {
        private static ServiceProvider _serviceProvider;

        /// <summary>
        /// Method to read configurations and register services
        /// </summary>
        private static void Startup()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json");
            IConfiguration configs = configurationBuilder.Build();

            // Bind configurations to models
            NameSorterConfiguration nameSorterConfigurations = configs.GetSection("NameSorterConfigurations").Get<NameSorterConfiguration>();
            ConsoleText consoleTexts = configs.GetSection("ConsoleTexts").Get<ConsoleText>();

            var services = new ServiceCollection();
            
            // Singleton services and objects
            services.AddSingleton<IFileService, FileService>();
            services.AddSingleton<ITextLineUtilityService, TextLineUtilityService>();
            services.AddSingleton(nameSorterConfigurations);
            services.AddSingleton(consoleTexts);

            // Transient services
            services.AddTransient<IApplication, Application>();

            // Scoped services
            services.AddScoped<INameSorterService, NameSorterService>();

            _serviceProvider = services.BuildServiceProvider(true);
        }

        static async Task Main(string[] args)
        {
            // Read configurations and register services
            Startup();

            // Start the Name Sorter Application
            using (var scope = _serviceProvider.CreateScope())
            {
                var application = scope.ServiceProvider.GetRequiredService<IApplication>();

                await application.Run();
            }
        }
    }
}
