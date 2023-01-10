using AutoMapper;
using ClimbingApp.Contracts.Repositories;
using ClimbingApp.Data.DTO;
using ClimbingApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ClimbingApp.Repositories
{
    public class RockRepository : IRockRepository
    {
        private DataContext dbContext;
        IDatabaseRepository databaseRepository;
        
        public RockRepository(DataContext dbContext, IDatabaseRepository databaseRepository)
        {
            this.dbContext = dbContext; 
            this.databaseRepository = databaseRepository;
        }

        public bool Insert(Rock rock)
        {
            try
            {
                dbContext.Rocks.Add(rock);

                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;

        }

        public bool Update(Rock rock)
        {
            var existingRock = GetById(rock.RockId);
             if (existingRock == null)
                return false;

            try
            {
                if (!existingRock.Name.Equals(rock.Name) && !String.IsNullOrEmpty(rock.Name))
                    existingRock.Name = rock.Name;

                if (!existingRock.Description.Equals(rock.Description) && !rock.Description.IsNullOrEmpty())
                    existingRock.Description = rock.Description;

                if (existingRock.Distance != rock.Distance && rock.Distance >= 1)
                    existingRock.Distance = rock.Distance;
                
                if (existingRock.Height != rock.Height && rock.Height >= 1)
                    existingRock.Height = rock.Height;    
                
                if (existingRock.Popularity != rock.Popularity && rock.Popularity >= 1)
                    existingRock.Popularity = rock.Popularity;

                if(existingRock.isLoose != rock.isLoose)
                    existingRock.isLoose = rock.isLoose;

                if(existingRock.isRecommended != rock.isRecommended)
                    existingRock.isRecommended = rock.isRecommended;

                if(existingRock.isShadedFromTrees != rock.isShadedFromTrees)
                    existingRock.isShadedFromTrees = rock.isShadedFromTrees;

                if (existingRock.positionLatitude != rock.positionLatitude && rock.positionLatitude >= 0)
                    existingRock.positionLatitude = rock.positionLatitude;

                if (existingRock.positionLogitude != rock.positionLogitude && rock.positionLogitude >= 0)
                    existingRock.positionLogitude = rock.positionLogitude;

                if (existingRock.RockFaceExposureId != rock.RockFaceExposureId && rock.RockFaceExposureId > 0)
                    existingRock.RockFaceExposureId = rock.RockFaceExposureId;

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
            var existingRock = GetById(id);

            if (existingRock == null)
                return false;

            try
            {
                dbContext.Rocks.Remove(existingRock);
                dbContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public List<Rock> List()
        {
            return dbContext.Rocks.ToList();
        }

        public List<Rock> ListRocksByAreaId(int AreaId)
        {
            return dbContext.Rocks.Where(x => x.Area.AreaId == AreaId).ToList();
        }

        public List<RockWithNumberOfRoutes> ListRocksWithNumberOfRoutesByAreaId(int AreaId)
        {
            var rocks = dbContext.Rocks.Where(x => x.Area.AreaId == AreaId).ToList();
            var result = new List<RockWithNumberOfRoutes>();

            foreach (var rock in rocks)
            {
                result.Add(new RockWithNumberOfRoutes(rock, CountRoutesInRock(rock.RockId)));
            }

            return result;
        }


        public Rock GetById(int id)
        {
            return dbContext.Rocks.Where(x => x.RockId == id).Include(x => x.RockFaceExposure).SingleOrDefault();
        }

        public NumberOfRoutes CountRoutesInRock(int rockId)
        {
            var rock = GetById(rockId);
            var numberOfRoutes= new NumberOfRoutes();

            AggreggateRoutes(rock.RockId, numberOfRoutes);
            return numberOfRoutes;
        }

        public void AggreggateRoutes(int rockId, NumberOfRoutes numberOfRoutes)
        {
            var grades = new RoutesGradesClassification();

            var routes = dbContext.Routes.Where(x => x.Rock.RockId == rockId);

            numberOfRoutes.VeryEasyRoutes += routes.Where(x => grades.VeryEasyGrades.Contains(x.Difficulty)).Count();
            numberOfRoutes.EasyRoutes += routes.Where(x => grades.EasyGrades.Contains(x.Difficulty)).Count();
            numberOfRoutes.MediumRoutes += routes.Where(x => grades.MediumGrades.Contains(x.Difficulty)).Count();
            numberOfRoutes.HardRoutes += routes.Where(x => grades.HardGrades.Contains(x.Difficulty)).Count();
            numberOfRoutes.VeryHardRoutes += routes.Where(x => grades.VeryHardGrades.Contains(x.Difficulty)).Count();
            numberOfRoutes.Projects += routes.Where(x => x.Name.Equals("Projekt")).Count();

        }

        public int GetRockIdByName(string name)
        {
            var rock = dbContext.Rocks.Where(x => x.Name.Equals(name)).SingleOrDefault();
            return rock.RockId;
        }
    }
}
