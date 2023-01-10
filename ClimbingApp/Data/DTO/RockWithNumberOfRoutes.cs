using ClimbingApp.Models;

namespace ClimbingApp.Data.DTO
{
    public class AreaWithNumberOfRoutes
    {
        public Area Area { get; set; }
        public NumberOfRoutes NumberOfRoutes { get; set; }

        public AreaWithNumberOfRoutes(Area area, NumberOfRoutes numberOfRoutes)
        {
            Area = area;
            NumberOfRoutes = numberOfRoutes;
        }
    }
}
