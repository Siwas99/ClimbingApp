using ClimbingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ClimbingApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Area> Areas { get; set; }
        public DbSet<ClimbStyle> ClimbStyle { get; set; }
        public DbSet<DominantRockFormation> DominantRockFormations { get; set; }
        public DbSet<ExpeditionLog> ExpeditionLogs { get; set; } 
        public DbSet<Region> Regions { get; set; }
        public DbSet<Rock> Rocks { get; set; }
        public DbSet<RockFaceExposure> RockFaceExposures { get; set; }
        public DbSet<RockFormation> RockFormations { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Models.Route> Routes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; } 
    }
}
