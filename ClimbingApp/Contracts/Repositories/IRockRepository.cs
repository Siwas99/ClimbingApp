using ClimbingApp.Data.DTO;
using ClimbingApp.Models;

namespace ClimbingApp.Contracts.Repositories
{
    public interface IRockRepository : IRepository<Rock>
    {
        public List<Rock> ListRocksByAreaId(int areaId);
        public List<RockWithNumberOfRoutes> ListRocksWithNumberOfRoutesByAreaId(int areaId);
        public void AggreggateRoutes(int rockId, NumberOfRoutes numberOfRoutes);
        public int GetRockIdByName(string name);

    }
}