using GameDashboardProject.Application.UnitOfWorks;
using GameDashboardProject.Domain.Buildings;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameDashboardProject.Application.Features.Queries.GetBuildings
{
    public class GetBuildingsHandler : IRequestHandler<GetBuildingsQuery, GetBuildingsResponse>
    {
        private readonly IMongoUnitOfWorkk _unitOfWork;

        public GetBuildingsHandler(IMongoUnitOfWorkk unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetBuildingsResponse> Handle(GetBuildingsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var buildings = await _unitOfWork.GetAllAsync<GameDashboardProject.Domain.Buildings.Building>();

                if (buildings == null || !buildings.Any())
                {
                    Console.WriteLine("No buildings found or error in fetching data.");
                }
                else
                {
                    Console.WriteLine($"Retrieved {buildings.Count} buildings from the database.");
                }

                return new GetBuildingsResponse(buildings.ToList());
            }
            catch (Exception ex)
            {
                // Hata işleme ve loglama
                Console.WriteLine($"Error occurred: {ex.Message}");
                throw;
            }
        }
    }
}
