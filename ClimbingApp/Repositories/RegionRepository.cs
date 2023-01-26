using AutoMapper;
using ClimbingApp.Contracts.Repositories;
using ClimbingApp.Data.DTO;
using ClimbingApp.Models;
using Microsoft.AspNetCore.Identity;

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

        public bool Update(Region region)
        {
            var existingRegion = GetById(region.RegionId);

            if (existingRegion == null)
                return false;

            try
            {
                if(!region.Name.Equals(existingRegion.Name) && !String.IsNullOrEmpty(region.Name))
                    existingRegion.Name = region.Name;

                if (!region.Description.Equals(existingRegion.Description) && !String.IsNullOrEmpty(region.Description))
                    existingRegion.Description = region.Description;

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

        public List<Region> Search(string phrase)
        {
            return dbContext.Regions.Where(x => x.Name.Contains(phrase)).ToList();
        }

        public List<GenericNumberOfRoutes<Region>> ListRegionsWithNumberOfRoutes()
        {
            var regions = dbContext.Regions.ToList();
            var result = new List<GenericNumberOfRoutes<Region>>();

            foreach (var region in regions)
            {
                result.Add(new GenericNumberOfRoutes<Region>(region, CountRoutesInRegion(region.RegionId)));
            }

            return result;
        }

        public NumberOfRoutes CountRoutesInRegion(int regionId)
        {
            var areas = databaseRepository.AreaRepository.ListAreasByRegionId(regionId);
            var rocks = new List<Rock>();
            foreach(var area in areas)
            {
                var areaRocks = databaseRepository.RockRepository.ListRocksByAreaId(area.AreaId);
                rocks.AddRange(areaRocks);
            }
            var numberOfRoutes = new NumberOfRoutes();

            foreach (var rock in rocks)
                databaseRepository.RockRepository.AggreggateRoutes(rock.RockId, numberOfRoutes);

            return numberOfRoutes;
        }

    }
}
