using ClimbingApp.Models;

namespace ClimbingApp.Contracts.Repositories
{
    public interface IWishlistRepository : IRepository<Wishlist>
    {
        public bool Insert(Models.Route route, User user);
        public bool CheckIfExists(int routeId, int userId);
        public List<Wishlist> GetByUserId(int userId);
        public bool DeleteByUserAndRoute(int routeId, int userId);

    }
}
