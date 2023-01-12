using AutoMapper;
using ClimbingApp.Contracts.Repositories;
using ClimbingApp.Data.Dictionaries;
using ClimbingApp.Data.DTO;
using ClimbingApp.Models;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ClimbingApp.Repositories
{
    public class ExpeditionLogRepository : IExpeditionLogRepository
    {
        private DataContext dbContext;
        IDatabaseRepository databaseRepository;
        
        public ExpeditionLogRepository(DataContext dbContext, IDatabaseRepository databaseRepository)
        {
            this.dbContext = dbContext; 
            this.databaseRepository = databaseRepository;
            
        }

        public bool Insert(ExpeditionLog expeditionLog)
        {
            if(CheckIfExists(expeditionLog.UserId ,expeditionLog.ExpeditionLogId))
                return false;
            try
            {
                dbContext.ExpeditionLogs.Add(expeditionLog);

                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public bool Update(ExpeditionLog expeditionLog)
        {
            var existingExpeditionLog = GetById(expeditionLog.ExpeditionLogId);

            if (existingExpeditionLog == null)
                return false;
            try
            {
                if (!existingExpeditionLog.Date.Equals(expeditionLog.Date) && !DateTime.Equals(expeditionLog.Date, new DateTime(0001, 01, 01, 00,00,00 )))
                    existingExpeditionLog.Date = expeditionLog.Date;

                if (existingExpeditionLog.Valuation != expeditionLog.Valuation && expeditionLog.Valuation >0)
                    existingExpeditionLog.Valuation = expeditionLog.Valuation;

                if (!existingExpeditionLog.Comment.Equals(expeditionLog.Comment) && !String.IsNullOrEmpty(expeditionLog.Comment))
                    existingExpeditionLog.Comment = expeditionLog.Comment;

                if (existingExpeditionLog.ClimbStyleId != expeditionLog.ClimbStyleId && expeditionLog.ClimbStyleId > 0)
                    existingExpeditionLog.ClimbStyleId = expeditionLog.ClimbStyleId;

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
            var existingexpeditionLog = GetById(id);

            if (existingexpeditionLog == null)
                return false;

            try
            {
                dbContext.ExpeditionLogs.Remove(existingexpeditionLog);
                dbContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public List<ExpeditionLog> List()
        {
            return dbContext.ExpeditionLogs.ToList();
        }

        public ExpeditionLog GetById(int id)
        {
            return dbContext.ExpeditionLogs.Where(x => x.ExpeditionLogId == id).SingleOrDefault();
        }

        public List<ExpeditionLog> GetByUsersId(int userId)
        {
            return dbContext.ExpeditionLogs.Include(x => x.ClimbStyle).Include(x => x.Route).ThenInclude(x => x.Rock).ThenInclude(x => x.Area).ThenInclude(x => x.Region).Where(x => x.User.UserId== userId).ToList();
        }

        public bool CheckIfExists(int routeId, int userId)
        {
            var result = dbContext.ExpeditionLogs.Where(x => x.UserId == userId && x.RouteId == routeId).SingleOrDefault();
            if (result != null)
                return true;
            return false;
        }

        public bool DeleteByUserAndRoute(int expeditionLogId, int userId)
        {
            var result = dbContext.ExpeditionLogs.Where(x => x.UserId == userId && x.ExpeditionLogId == expeditionLogId).SingleOrDefault();
            if (result == null)
                return false;
            try
            {
                dbContext.Remove(result);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public UserStats GetUserStats(int userId)
        {
            var userStats = new UserStats();

            userStats.BeatenRoutes = dbContext.ExpeditionLogs.Where(x => x.UserId == userId).Count();
            userStats.OnSights = dbContext.ExpeditionLogs.Include(x => x.ClimbStyle).Where(x => x.UserId == userId && x.ClimbStyle.Name.Equals("On Sight")).Count();
            userStats.Flashes = dbContext.ExpeditionLogs.Include(x => x.ClimbStyle).Where(x => x.UserId == userId && x.ClimbStyle.Name.Equals("Flash")).Count();
            
            var beatenRoutes = dbContext.ExpeditionLogs.Include(x => x.Route).Where(x => x.UserId == userId).ToList();
            var gradesDictionary = new GradesDictionary();

            var hardestOne = 0;
            foreach(var route in beatenRoutes)
            {
                if (!route.Route.Difficulty.IsNullOrEmpty())
                {
                    if (gradesDictionary.Grades.TryGetValue(route.Route.Difficulty, out var current))
                    {
                        if (hardestOne < current)
                            hardestOne = current;
                    }
                }
            }
            userStats.HardestRoute = gradesDictionary.GetKeyFromValue(hardestOne);

            return userStats;
        }


    }
}