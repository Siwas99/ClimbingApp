using ClimbingApp.Data.DTO;
using ClimbingApp.Models;

namespace ClimbingApp.Contracts.Repositories
{
    public interface IDominantRockFormationRepository
    {
        public bool InsertByRockId(int rockId, string rockFormationName);
        public RockFormation GetRockFormationByName(string name);
        public bool UpdateByRockId(int rockId, List<string> rockFormationNames);
        public bool DeleteRockFormationsByRockId(int rockId);
        public List<DominantRockFormation> GetRockFormationsByRockId(int rockId);
        public List<string> GetNewDominatRockFormations(RockDTO rock);

    }
}