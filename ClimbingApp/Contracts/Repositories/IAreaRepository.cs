using ClimbingApp.Data.DTO;
using ClimbingApp.Models;

namespace ClimbingApp.Contracts.Repositories
{
    public interface IAreaRepository : IRepository<Area>
    {
        public List<Area> ListAreasByRegionId(int regionId);
        public List<GenericNumberOfRoutes<Area>> ListAreasWithNumberOfRoutesByRegionId(int regionId);
        public NumberOfRoutes CountRoutesInArea(int areaId);
        public List<Area> Search(string phrase);

    }
}