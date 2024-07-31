using GameDashboardProject.Application.Features.Command.AddBuilding;
using MediatR;

public record AddBuildingCommand(
    string BuildingTypeId,
    decimal BuildingCost,
    int ConstructionTime
) : IRequest<AddBuildingResponse>;
