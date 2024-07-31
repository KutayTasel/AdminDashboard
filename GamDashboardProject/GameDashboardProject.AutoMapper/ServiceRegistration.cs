
using GameDashboardProject.Application.Abstractions.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace GameDashboardProject.Mapper
{
    public static class ServiceRegistration
    {
        public static void AddMapperServices(this IServiceCollection services)
        {
            services.AddSingleton<IMyMapper, Mapper>();
        }
    }
}
