using ClimbingApp.Models;

namespace ClimbingApp.Contracts.Repositories
{
    public interface IRockFaceExposureRepository 
    {
        public RockFaceExposure GetRockFaceExposureByName(string name);

    }
}
