using MediatR;

namespace GameDashboardProject.Application.Features.Commands
{
    public record RegisterCommand(string UserName, string Password, string ConfirmPassword, string Email) : IRequest<Unit>;
}
