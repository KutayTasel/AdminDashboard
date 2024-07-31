using MediatR;

namespace GameDashboardProject.Application.Features.Queries.GetBuildings
{
    public record GetBuildingsQuery : IRequest<GetBuildingsResponse>;
}
