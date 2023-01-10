using ClimbingApp.Models;

namespace ClimbingApp.Data.DTO
{
    public class RockWithNumberOfRoutes
    {
        public Rock Rock { get; set; }
        public NumberOfRoutes NumberOfRoutes { get; set; }

        public RockWithNumberOfRoutes(Rock rock, NumberOfRoutes numberOfRoutes)
        {
            Rock = rock;
            NumberOfRoutes = numberOfRoutes;
        }
    }
}
