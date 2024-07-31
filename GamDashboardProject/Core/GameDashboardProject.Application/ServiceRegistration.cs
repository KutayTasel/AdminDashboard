using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using GameDashboardProject.Application.Behaviors;
using GameDashboardProject.Application.Exceptions;
using MediatR;

namespace GameDashboardProject.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(ServiceRegistration).Assembly));
            services.AddValidatorsFromAssembly(typeof(ServiceRegistration).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehavior<,>));
            services.AddExceptionHandler<ExceptionMiddleware>();
        }
    }
}
