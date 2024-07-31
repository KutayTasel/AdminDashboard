using GameDashboardProject.Domain.Buildings;
using System.Collections.Generic;

namespace GameDashboardProject.Application.Features.Queries.Building.GetBuildingTypes
{
    public record GetBuildingTypesResponse(List<BuildingType> BuildingTypes);
}
