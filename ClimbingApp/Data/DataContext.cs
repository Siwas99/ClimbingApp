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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    RoleId = 1,
                    Name = "Admin"
                },
                new Role
                {
                    RoleId = 2,
                    Name = "User"
                }
            );

            modelBuilder.Entity<RockFormation>().HasData(
                new RockFormation
                {
                    RockFormationId = 1,
                    Name = "Slabs"
                },
                new RockFormation
                {
                    RockFormationId = 2,
                    Name = "Vertical"
                },
                new RockFormation
                {
                    RockFormationId = 3,
                    Name = "Overhang"
                },
                new RockFormation
                {
                    RockFormationId = 4,
                    Name = "Roof"
                }
                );

            modelBuilder.Entity<RockFaceExposure>().HasData(
                new RockFaceExposure
                {
                    RockFaceExposureId= 1,
                    Name="North"
                },
                new RockFaceExposure
                {
                    RockFaceExposureId = 2,
                    Name = "East"
                },
                new RockFaceExposure
                {
                    RockFaceExposureId = 3,
                    Name = "South"
                },
                new RockFaceExposure
                {
                    RockFaceExposureId = 4,
                    Name = "West"
                }
                );

            modelBuilder.Entity<ClimbStyle>().HasData(
                new ClimbStyle
                {
                    ClimbStyleId= 1,
                    Name = "On Sight",
                    Description = "Czyste przejście drogi, tzn. bez odpadnięcia i obciążania liny, bez jej znajomości. Oznacza to, że niedozwolone są podpowiedzi lub obserwacja innych wspinaczy."
                },
                new ClimbStyle
                {
                    ClimbStyleId = 2,
                    Name = "Flash",
                    Description = "Czyste przejście drogii, tzn. bez odpadnięcia i obciążania liny, ze znajomością drogi. Oznacza to, że podpowiedzi oraz oglądanie innych wspinaczy jest dozwolone."
                },
                new ClimbStyle
                {
                    ClimbStyleId = 3,
                    Name = "Red Point",
                    Description = "Przejście całej drogi od początku do końca bez odpadnięć i odpoczynków. Dozwolone jest wcześniejsze ćwiczenie drogi i opracowanie sekwencji przechwytów. Styl ten uważa się za normalny w przypadku trudniejszych dróg."
                },
                new ClimbStyle
                {
                    ClimbStyleId = 4,
                    Name = "Top Rope",
                    Description = "Lina asekurująca wspinacza biegnie na górę, przechodzi przez stanowisko i wraca do stojącego na dole partnera. Przejście drogi w tym stylu nie jest obecnie uznawane za klasyczne, jednak z uwagi na najmniejsze ryzyko urazów wspinaczka na wędkę ma znaczenie w treningu, patentowaniu drogi oraz we wspinaczkowej rekreacji, szczególnie u osób początkujących."
                }

                );
        }
    }
}
