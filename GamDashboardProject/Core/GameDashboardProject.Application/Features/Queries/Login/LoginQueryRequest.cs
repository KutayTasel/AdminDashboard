using MediatR;

namespace GameDashboardProject.Application.Features.Queries.Login
{
    public record LoginQueryRequest(string UserName, string Password) : IRequest<LoginQueryResponse>;
}
