using ClimbingApp.Contracts.Repositories;
using ClimbingApp.Data.DTO;
using ClimbingApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ClimbingApp.Repositories
{
    public class DominantRockFormationRepository : IDominantRockFormationRepository
    {
        private DataContext dbContext;
        IDatabaseRepository databaseRepository;

        public DominantRockFormationRepository(DataContext dbContext, IDatabaseRepository databaseRepository)
        {
            this.dbContext = dbContext;
            this.databaseRepository = databaseRepository;
        }
        public List<DominantRockFormation> List()
        {
            throw new NotImplementedException();
        }
        public bool Update(DominantRockFormation dominantRockFormation)
        {
            throw new NotImplementedException();
        }
        public bool Insert(DominantRockFormation dominantRockFormation)
        {
            throw new NotImplementedException();
        }
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
        public DominantRockFormation GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<DominantRockFormation> GetRockFormationsByRockId(int rockId)
        {
            return dbContext.DominantRockFormations.Where(x => x.RockId == rockId).Include(x => x.RockFormation).ToList();
        }
        
        public bool DeleteRockFormationsByRockId(int rockId)
        {
            try
            {
                var rockFormationsToDelete = dbContext.DominantRockFormations.Where(x => x.RockId == rockId).ToList();

                if(rockFormationsToDelete.IsNullOrEmpty())
                    return false;
                
                dbContext.RemoveRange(rockFormationsToDelete);
                dbContext.SaveChanges();

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public RockFormation GetRockFormationByName(string name)
        {
            return dbContext.RockFormations.Where(x => x.Name.Equals(name)).FirstOrDefault();
        }

        public bool InsertByRockId(int rockId, string rockFormationName)
        {
            var rockFormation = GetRockFormationByName(rockFormationName);

            var dominantRockFormation = new DominantRockFormation
            {
                RockId = rockId,
                RockFormationId = rockFormation.RockFormationId
            };
            try
            {
                dbContext.DominantRockFormations.Add(dominantRockFormation);
                dbContext.SaveChanges();

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool UpdateByRockId(int rockId, List<string> rockFormationNames)
        {
            if(!DeleteRockFormationsByRockId(rockId))
                return false;

            try
            {
                foreach(var rockFormationName in rockFormationNames)
                {
                    if(!InsertByRockId(rockId, rockFormationName))
                        return false;
                }
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }

        }

        public List<string> GetNewDominatRockFormations(RockDTO rock)
        {
            var newDominantRockFormations = new List<string>();

            if (rock.Slabs)
                newDominantRockFormations.Add("Slabs");

            if (rock.Vertical)
                newDominantRockFormations.Add("Vertical");

            if (rock.Overhang)
                newDominantRockFormations.Add("Overhang");

            if (rock.Roof)
                newDominantRockFormations.Add("Roof");

            return newDominantRockFormations;
        }
    }
}
