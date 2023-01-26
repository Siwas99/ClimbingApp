namespace ClimbingApp.Contracts.Repositories
{
    public interface IDatabaseRepository
    {
        public IAreaRepository AreaRepository { get; }
        public IDominantRockFormationRepository DominantRockFormationRepository { get; }
        public IExpeditionLogRepository ExpeditionLogRepository { get; }
        public IRegionRepository RegionRepository { get; }
        public IRockRepository RockRepository { get; }
        public IRockFaceExposureRepository RockFaceExposureRepository { get; }
        public IRoleRepository RoleRepository { get; }
        public IRouteRepository RouteRepository { get; }
        public IUserRepository UserRepository { get; }
        public IWishlistRepository WishlistRepository { get; }
    }
}
