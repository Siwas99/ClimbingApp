using ClimbingApp.Contracts.Repositories;
using ClimbingApp.Models;

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
    }
}
