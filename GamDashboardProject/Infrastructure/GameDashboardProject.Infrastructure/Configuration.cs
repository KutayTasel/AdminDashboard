using Microsoft.Extensions.Configuration;

namespace YourNamespace.Infrastructure
{
    public static class Configuration
    {
        public static IConfigurationRoot Token
        {
            get
            {
                var envConfigBuild = new ConfigurationBuilder().AddEnvironmentVariables().Build();
                var env = envConfigBuild["ASPNETCORE_ENVIRONMENT"];
                env = string.IsNullOrEmpty(env) ? "Production" : env;
                var manager = new ConfigurationBuilder();
                manager.AddJsonFile("appsettings.json")
                       .AddJsonFile($"appsettings.{env}.json", optional: true);
                return manager.Build();
            }
        }
    }
}
