using GameDashboardProject.Application.Exceptions;
using Microsoft.AspNetCore.Builder;

namespace GameDashboardProject.Application
{
    public static class ConfigureExceptionMiddleware
    {
        public static void ConfigureExpectionHandleingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
