using System.Collections.Generic;
using System.Threading.Tasks;
using GameDashboardProject.Domain.Buildings;

namespace GameDashboardProject.Application.Repositories
{
    public interface IBuildingRepository
    {
        Task AddAsync(Building building);
        Task<List<Building>> GetAllAsync();
        Task<Building> GetByIdAsync(string id);
        Task UpdateAsync(Building building);
        List<Building> GetBuildingsByTypeIdAsync(string buildingTypeId);
    }
}
