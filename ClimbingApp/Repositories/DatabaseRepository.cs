using ClimbingApp.Contracts.Repositories;
using System.Linq.Expressions;

namespace ClimbingApp.Repositories
{
    public class DatabaseRepository : IDatabaseRepository
    {
        public IAreaRepository AreaRepository { get; private set; }
        public ICommentRepository CommentRepository { get; private set; }
        public IDominantRockFormationRepository DominantRockFormationRepository { get; private set; }
        public IExpeditionLogRepository ExpeditionLogRepository { get; private set; }
        public IRegionRepository RegionRepository { get; private set; }
        public IRockRepository RockRepository { get; private set; }
        public IRockFaceExposureRepository RockFaceExposureRepository { get; private set; }
        public IRoleRepository RoleRepository { get; private set; }
        public IRouteRepository RouteRepository { get; private set; }
        public IUserRepository UserRepository { get; private set; }
        public IWishlistRepository WishlistRepository { get; private set; }

        public DatabaseRepository(DataContext dbContext)
        {
            AreaRepository = new AreaRepository(dbContext, this);
            CommentRepository = new CommentRepository(dbContext, this);
            DominantRockFormationRepository = new DominantRockFormationRepository(dbContext, this);
            ExpeditionLogRepository = new ExpeditionLogRepository(dbContext, this);
            RegionRepository = new RegionRepository(dbContext, this);
            RockRepository = new RockRepository(dbContext, this);
            RockFaceExposureRepository = new RockFaceExposureRepository(dbContext, this);
            RoleRepository = new RoleRepository(dbContext, this);
            RouteRepository = new RouteRepository(dbContext, this);
            UserRepository = new UserRepository(dbContext, this);
            WishlistRepository = new WishlistRepository(dbContext, this);
        }
    }
}
