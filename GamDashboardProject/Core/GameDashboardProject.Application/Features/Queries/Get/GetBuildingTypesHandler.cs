using GameDashboardProject.Application.Features.Queries.Building.GetBuildingTypes;
using GameDashboardProject.Application.UnitOfWorks;
using GameDashboardProject.Domain.Buildings;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class GetBuildingTypesHandler : IRequestHandler<GetBuildingTypesQuery, GetBuildingTypesResponse>
{
    private readonly IMongoUnitOfWorkk _unitOfWork;

    public GetBuildingTypesHandler(IMongoUnitOfWorkk unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetBuildingTypesResponse> Handle(GetBuildingTypesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var buildingTypes = await _unitOfWork.GetAllBuildingTypesAsync();

            if (buildingTypes == null || !buildingTypes.Any())
            {
                Console.WriteLine("No BuildingTypes found or error in fetching data.");
            }
            else
            {
                Console.WriteLine($"Retrieved {buildingTypes.Count} building types from the database.");
            }

            return new GetBuildingTypesResponse(buildingTypes.ToList());
        }
        catch (Exception ex)
        {
            // Hata işleme ve loglama
            Console.WriteLine($"Error occurred: {ex.Message}");
            throw;
        }
    }
}
