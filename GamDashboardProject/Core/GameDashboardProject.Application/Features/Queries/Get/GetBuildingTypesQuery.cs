using MediatR;

namespace GameDashboardProject.Application.Features.Queries.Building.GetBuildingTypes
{
    public record GetBuildingTypesQuery : IRequest<GetBuildingTypesResponse>;
}
