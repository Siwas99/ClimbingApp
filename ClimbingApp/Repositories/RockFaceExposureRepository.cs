using ClimbingApp.Contracts.Repositories;
using ClimbingApp.Models;

namespace ClimbingApp.Repositories
{
    public class RockFaceExposureRepository : IRockFaceExposureRepository
    {
        private DataContext dbContext;
        IDatabaseRepository databaseRepository;

        public RockFaceExposureRepository(DataContext dbContext, IDatabaseRepository databaseRepository)
        {
            this.dbContext = dbContext;
            this.databaseRepository = databaseRepository;
        }

        public RockFaceExposure GetRockFaceExposureByName(string name)
        {
            return dbContext.RockFaceExposures.Where(x => x.Name.Equals(name)).FirstOrDefault();
        }

        public RockFaceExposure GetRockFaceExposureById(string name)
        {
            return dbContext.RockFaceExposures.Where(x => x.Name.Equals(name)).FirstOrDefault();
        }

    }
}
