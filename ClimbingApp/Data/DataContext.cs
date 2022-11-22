using ClimbingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ClimbingApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Region> Regions { get; set; }
        public DbSet<Rock> Rocks { get; set; }
        public DbSet<Models.Route> Routes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ExpeditionLog> ExpeditionLogs { get; set; } 
        public DbSet<Wishlist> Wishlists { get; set; } 
        public DbSet<RegionComment> RegionComments { get; set; } 
        public DbSet<RockComment> RockComments { get; set; } 
        public DbSet<RouteComment> RouteComments { get; set; } 
    }
}
