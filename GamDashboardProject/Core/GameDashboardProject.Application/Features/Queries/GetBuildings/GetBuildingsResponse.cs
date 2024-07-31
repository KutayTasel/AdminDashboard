using System.Collections.Generic;

namespace GameDashboardProject.Application.Features.Queries.GetBuildings
{
    public record GetBuildingsResponse(List<GameDashboardProject.Domain.Buildings.Building> Buildings);
}
