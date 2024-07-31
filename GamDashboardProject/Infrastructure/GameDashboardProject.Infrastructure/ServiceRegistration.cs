using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using GameDashboardProject.Application.Abstractions.Services;
using GameDashboardProject.Infrastructure.MailService;
using GameDashboardProject.Infrastructure.TokenService;
using Microsoft.Extensions.Configuration;

namespace YourNamespace.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var tokenConfig = configuration.GetSection("Token");
            services.Configure<TokenSettings>(tokenConfig);

            var tokenSettings = tokenConfig.Get<TokenSettings>();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.SecurityKey));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = tokenSettings.Issuer,
                        ValidAudience = tokenSettings.Audience,
                        IssuerSigningKey = key,
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddTransient<ITokenServices, TokenServices>();
            services.AddTransient<IMailServices, EMailService>();
        }
    }
}
