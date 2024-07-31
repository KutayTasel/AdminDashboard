using GameDashboardProject.Domain.Buildings;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameDashboardProject.Application.Repositories
{
    public interface IBuildingTypeRepository
    {
        Task AddAsync(BuildingType buildingType);
        Task<List<BuildingType>> GetAllAsync();
        Task<BuildingType> GetByIdAsync(string id);
        Task UpdateAsync(BuildingType buildingType);
    }
}
