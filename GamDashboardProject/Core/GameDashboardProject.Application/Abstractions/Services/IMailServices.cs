using GameDashboardProject.Domain.Identities;
using System.Threading.Tasks;

namespace GameDashboardProject.Application.Abstractions.Services
{
    public interface IMailServices
    {
        Task SendMessageAsync(string to, string subject, string body, bool isBodyHtml = true);
        Task SendMessageAsync(string[] tos, string subject, string body, bool isBodyHtml = true);
        Task SendPasswordResetMailAsync(AppUser user, string resetToken);
    }
}
