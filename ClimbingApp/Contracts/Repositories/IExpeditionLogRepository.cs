using ClimbingApp.Data.DTO;
using ClimbingApp.Models;

namespace ClimbingApp.Contracts.Repositories
{
    public interface IExpeditionLogRepository : IRepository<ExpeditionLog>
    {
        public List<ExpeditionLog> GetByUsersId(int userId);
        public bool CheckIfExists(int routeId, int userId);
        public bool DeleteByUserAndRoute(int routeId, int userId);
        public UserStats GetUserStats(int userId);

    }
}
