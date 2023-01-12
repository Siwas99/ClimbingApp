using ClimbingApp.Models;

namespace ClimbingApp.Data.DTO
{
    public class SearchResult
    {
        public List<Region> Regions { get; set; }
        public List<Area> Areas { get; set; }
        public List<Rock> Rocks { get; set; }
        public List<Models.Route> Routes { get; set; }
    }
}
