using AutoMapper;
using ClimbingApp.Contracts.Repositories;
using ClimbingApp.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Route = ClimbingApp.Models.Route;

namespace ClimbingApp.Repositories
{
    public class RouteRepository : IRouteRepository
    {
        private DataContext dbContext;
        IDatabaseRepository databaseRepository;
        

        public RouteRepository(DataContext dbContext, IDatabaseRepository databaseRepository)
        {
            this.dbContext = dbContext;
            this.databaseRepository = databaseRepository;            
        }

        public bool Insert(Route Route)
        {
            try
            {
                dbContext.Routes.Add(Route);

                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;

        }

        public bool Update(Route route)
        {
            var existingRoute = GetById(route.RouteId);

            if (existingRoute == null)
                return false;

            try
            {
                if (!existingRoute.Name.Equals(route.Name) && !String.IsNullOrEmpty(route.Name))
                    existingRoute.Name = route.Name;
                
                if (!existingRoute.Description.Equals(route.Description) && !String.IsNullOrEmpty(route.Description))
                    existingRoute.Description = route.Description;
                
                if (!existingRoute.Difficulty.Equals(route.Difficulty) && !String.IsNullOrEmpty(route.Difficulty))
                    existingRoute.Difficulty = route.Difficulty;
                
                if (!existingRoute.Author.Equals(route.Author) && !String.IsNullOrEmpty(route.Author))
                    existingRoute.Author = route.Author;
                
                if (!existingRoute.Year.Equals(route.Year) && route.Year > 1900)
                    existingRoute.Year = route.Year;
                
                if (!existingRoute.Number.Equals(route.Number) && route.Number > 0)
                    existingRoute.Number = route.Number;


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
            var existingRoute = GetById(id);

            if (existingRoute == null)
                return false;

            try
            {
                dbContext.Routes.Remove(existingRoute);
                dbContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public List<Route> List()
        {
            return dbContext.Routes.ToList();
        }

        public Route GetById(int routeId)
        {
            return dbContext.Routes.Where(x => x.RouteId == routeId).Include(x => x.Rock).ThenInclude(x => x.RockFaceExposure).SingleOrDefault();
        }

        public List<Route> GetByRockId(int rockId)
        {
            return dbContext.Routes.Where(x => x.Rock.RockId== rockId).ToList();

        }

    }
}
