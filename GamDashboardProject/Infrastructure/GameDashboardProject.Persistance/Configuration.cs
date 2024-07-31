using Microsoft.Extensions.Configuration;
using System.IO;

namespace GameDashboardProject.Persistence
{
    public static class Configuration
    {
        private static IConfigurationRoot configuration;

        static Configuration()
        {
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        }

        public static string DefaultConnectionString => configuration.GetConnectionString("DefaultConnection");

        public static string MongoConnectionString => configuration.GetConnectionString("MongoConnection");
    }
}
