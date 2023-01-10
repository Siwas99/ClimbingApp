using ClimbingApp.Models;

namespace ClimbingApp.Contracts.Repositories
{
    public interface IWishlistRepository : IRepository<Wishlist>
    {
        public bool Insert(Models.Route route, User user);
        public List<Wishlist> GetByUserId(int userId);

    }
}
