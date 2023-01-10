using AutoMapper;
using ClimbingApp.Contracts.Repositories;
using ClimbingApp.Models;

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
                existingExpeditionLog.User = expeditionLog.User;
                existingExpeditionLog.Route = expeditionLog.Route;
                existingExpeditionLog.Date = expeditionLog.Date;
                existingExpeditionLog.Valuation = expeditionLog.Valuation;
                existingExpeditionLog.Comment = expeditionLog.Comment;

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
            return dbContext.ExpeditionLogs.Where(x => x.User.UserId== userId).ToList();
        }

    }
}