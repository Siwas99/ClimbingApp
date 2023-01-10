using ClimbingApp.Models;

namespace ClimbingApp.Contracts.Repositories
{
    public interface IRouteRepository : IRepository<Models.Route>
    {
        public List<Models.Route> GetByRockId(int rockId);
    }
}
