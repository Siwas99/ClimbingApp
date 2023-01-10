using AutoMapper;
using ClimbingApp.Contracts.Repositories;
using ClimbingApp.Models;

namespace ClimbingApp.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private DataContext dbContext;
        IDatabaseRepository databaseRepository;

        public RegionRepository(DataContext dbContext, IDatabaseRepository databaseRepository)
        {
            this.dbContext = dbContext;
            this.databaseRepository = databaseRepository;
        }

        public bool Insert(Region Region)
        {
            try
            {
                dbContext.Regions.Add(Region);

                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;

        }

        public bool Update(Region Region)
        {
            var existingRegion = GetById(Region.RegionId);

            if (existingRegion == null)
                return false;

            try
            {
                existingRegion.Name = Region.Name;
                existingRegion.Description = Region.Description;

                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool Delete(int id)
        {
            var existingRegion = GetById(id);

            if (existingRegion == null)
                return false;

            try
            {
                dbContext.Regions.Remove(existingRegion);
                dbContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public List<Region> List()
        {
            return dbContext.Regions.ToList();
        }

        public Region GetById(int id)
        {
            return dbContext.Regions.Where(x => x.RegionId == id).SingleOrDefault();
        }

    }
}
