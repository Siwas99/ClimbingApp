using ClimbingApp.Models;

namespace ClimbingApp.Contracts.Repositories
{
    public interface IDominantRockFormationRepository
    {
        public bool InsertByRockId(int rockId, string rockFormationName);
        public RockFormation GetRockFormationByName(string name);

    }
}