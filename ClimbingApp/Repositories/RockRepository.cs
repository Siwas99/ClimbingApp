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
            throw new NotImplementedException();
        }

        public bool Update(Rock rock, bool changes)
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

                if(existingRock.IsLoose != rock.IsLoose && changes)
                    existingRock.IsLoose = rock.IsLoose;

                if(existingRock.IsRecommended != rock.IsRecommended && changes)
                    existingRock.IsRecommended = rock.IsRecommended;

                if(existingRock.IsShadedFromTrees != rock.IsShadedFromTrees && changes)
                    existingRock.IsShadedFromTrees = rock.IsShadedFromTrees;

                if (existingRock.Latitude != rock.Latitude && rock.Latitude >= 1)
                    existingRock.Latitude = rock.Latitude;

                if (existingRock.Longitude != rock.Longitude && rock.Longitude >= 1)
                    existingRock.Longitude = rock.Longitude;

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

        public List<GenericNumberOfRoutes<Rock>> ListRocksWithNumberOfRoutesByAreaId(int AreaId)
        {
            var rocks = dbContext.Rocks.Where(x => x.Area.AreaId == AreaId).ToList();
            var result = new List<GenericNumberOfRoutes<Rock>>();

            foreach (var rock in rocks)
            {
                result.Add(new GenericNumberOfRoutes<Rock>(rock, CountRoutesInRock(rock.RockId)));
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

        public List<Rock> Search(string phrase)
        {
            return dbContext.Rocks.Where(x => x.Name.Contains(phrase)).ToList();
        }

        public bool UploadImage(IFormFile image, string name)
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory;
                var fileName = $"{name}.jpg";
                var newPath = Path.GetFullPath(Path.Combine(path, @"..\..\..\..\", @"Frontend\climbing-app\public\img", fileName));

                if (System.IO.File.Exists(newPath))
                    System.IO.File.Delete(newPath);

                using (var fileStream = new FileStream(newPath, FileMode.Create))
                {
                    image.CopyToAsync(fileStream);
                }
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
