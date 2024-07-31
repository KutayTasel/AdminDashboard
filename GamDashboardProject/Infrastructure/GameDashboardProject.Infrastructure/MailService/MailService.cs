using GameDashboardProject.Application.Abstractions.Services;
using GameDashboardProject.Domain.Identities;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace GameDashboardProject.Infrastructure.MailService
{
    public class EMailService : IMailServices
    {
        private readonly IConfiguration _configuration;

        public EMailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendMessageAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendMessageAsync(new[] { to }, subject, body, isBodyHtml);
        }

        public async Task SendMessageAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
        {
            var mail = new MailMessage
            {
                IsBodyHtml = isBodyHtml,
                Subject = subject,
                Body = body,
                From = new MailAddress(_configuration["SmtpSettings:Email"], "Game Dashboard", Encoding.UTF8)
            };

            foreach (var to in tos)
            {
                mail.To.Add(to);
            }

            var smtpClient = new SmtpClient
            {
                Credentials = new NetworkCredential(_configuration["SmtpSettings:Email"], _configuration["SmtpSettings:Password"]),
                Port = int.Parse(_configuration["SmtpSettings:Port"]),
                EnableSsl = bool.Parse(_configuration["SmtpSettings:EnableSsl"]),
                Host = _configuration["SmtpSettings:Host"]
            };

            await smtpClient.SendMailAsync(mail);
        }

        public async Task SendPasswordResetMailAsync(AppUser user, string resetToken)
        {
            var mail = new StringBuilder();
            mail.AppendLine($"Hello {user.UserName},");
            mail.AppendLine("To reset your password, please click the link below:");
            mail.AppendLine($"<a href='https://yourdomain.com/reset-password?userId={user.Id}&token={resetToken}'>Reset Password</a>");
            mail.AppendLine("If you did not request a password reset, please ignore this email.");

            await SendMessageAsync(user.Email, "Password Reset Request", mail.ToString());
        }
    }
}
