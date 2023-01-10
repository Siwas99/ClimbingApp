using ClimbingApp.Models;

namespace ClimbingApp.Contracts.Repositories
{
    public interface IExpeditionLogRepository : IRepository<ExpeditionLog>
    {
        public List<ExpeditionLog> GetByUsersId(int userId);

    }
}
