using AutoMapper;
using ClimbingApp.Contracts.Repositories;
using ClimbingApp.Data.DTO;
using ClimbingApp.Models;
using Microsoft.IdentityModel.Tokens;

namespace ClimbingApp.Repositories
{
    public class AreaRepository : IAreaRepository
    {
        private DataContext dbContext;
        IDatabaseRepository databaseRepository;
        
        public AreaRepository(DataContext dbContext, IDatabaseRepository databaseRepository)
        {
            this.dbContext = dbContext;
            this.databaseRepository = databaseRepository;
        }

        public bool Insert(Area area)
        {
            try
            {
                dbContext.Areas.Add(area);

                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;

        }

        public bool Update(Area area)
        {
            var existingArea = GetById(area.AreaId);
            if (existingArea == null)
                return false;
            
            try
            {
                if (!existingArea.Name.Equals(area.Name) && !String.IsNullOrEmpty(area.Name))
                    existingArea.Name = area.Name;

                if (!existingArea.Description.Equals(area.Description) && !area.Description.IsNullOrEmpty())
                    existingArea.Description = area.Description;

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
            var existingArea = GetById(id);

            if (existingArea == null)
                return false;

            try
            {
                dbContext.Areas.Remove(existingArea);
                dbContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public List<Area> List()
        {
            return dbContext.Areas.ToList();
        }

        public List<Area> ListAreasByRegionId(int regionId)
        {
            return dbContext.Areas.Where(x => x.Region.RegionId == regionId).ToList();
        }

        public List<GenericNumberOfRoutes<Area>> ListAreasWithNumberOfRoutesByRegionId(int regionId)
        {
            var areas = dbContext.Areas.Where(x => x.Region.RegionId == regionId).ToList();
            var result = new List<GenericNumberOfRoutes<Area>>();

            foreach (var area in areas)
            {
                result.Add(new GenericNumberOfRoutes<Area>(area, CountRoutesInArea(area.AreaId)));
            }

            return result;
        }

        public Area GetById(int id)
        {
            return dbContext.Areas.Where(x => x.AreaId == id).SingleOrDefault();
        }

        public NumberOfRoutes CountRoutesInArea(int areaId)
        {
            var rocks = databaseRepository.RockRepository.ListRocksByAreaId(areaId);
            var numberOfRoutes = new NumberOfRoutes();

            foreach (var rock in rocks)
                databaseRepository.RockRepository.AggreggateRoutes(rock.RockId, numberOfRoutes);

            return numberOfRoutes;
        }

        public List<Area> Search(string phrase)
        {
            return dbContext.Areas.Where(x => x.Name.Contains(phrase)).ToList();
        }

    }
}
