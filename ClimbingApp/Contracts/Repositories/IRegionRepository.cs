using ClimbingApp.Models;

namespace ClimbingApp.Contracts.Repositories
{
    public interface IRegionRepository : IRepository<Region>
    {
        public List<Region> Search(string phrase);
    }
}