using AutoMapper;
using ClimbingApp.Contracts.Repositories;
using ClimbingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ClimbingApp.Repositories
{
    public class WishlistRepository : IWishlistRepository
    {
        private DataContext dbContext;
        private IDatabaseRepository databaseRepository;
 
        public WishlistRepository(DataContext dbContext, IDatabaseRepository databaseRepository)
        {
            this.dbContext = dbContext;
            this.databaseRepository = databaseRepository;
        }

        public bool Insert(Wishlist wishlist)
        {
            try
            {
                dbContext.Wishlists.Add(wishlist);

                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;

        }

        public bool Insert(Models.Route route, User user)
        {
            var wishlist = new Wishlist
            {
                RouteId = route.RouteId,
                UserId = user.UserId
            };
            try
            {
                dbContext.Wishlists.Add(wishlist);
                dbContext.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool Update(Wishlist wishlist)
        {
            var existingWishlist = GetById(wishlist.WishlistId);

            if (existingWishlist == null)
                return false;

            try
            {
                existingWishlist.User = wishlist.User;
                existingWishlist.Route = wishlist.Route;

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
            var existingWishlist = GetById(id);

            if (existingWishlist == null)
                return false;

            try
            {
                dbContext.Wishlists.Remove(existingWishlist);
                dbContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public List<Wishlist> List()
        {
            return dbContext.Wishlists.ToList();
        }

        public Wishlist GetById(int id)
        {
            return dbContext.Wishlists.Where(x => x.WishlistId == id).SingleOrDefault();
        }

        public List<Wishlist> GetByUserId(int userId)
        {
            return dbContext.Wishlists.Include(x => x.Route).ThenInclude(x => x.Rock).ThenInclude(x => x.Area).ThenInclude(x => x.Region).Where(x => x.User.UserId == userId).ToList();
        }
        public bool CheckIfExists(int routeId, int userId)
        {
            var result = dbContext.Wishlists.Where(x => x.UserId == userId && x.RouteId == routeId).SingleOrDefault();
            if (result != null)
                return true;
            return false;
        }
        public bool DeleteByUserAndRoute(int routeId, int userId)
        {
            var result = dbContext.Wishlists.Where(x => x.UserId == userId && x.RouteId == routeId).SingleOrDefault();
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

    }
    
}
