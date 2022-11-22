namespace ClimbingApp.Models
{
    public class Route
    {
        public int RouteId { get; set; }
        public string Name { get; set; }
        public string Difficulty { get; set; }
        public string? Author { get; set; }
        public int Year { get; set; }
        public Rock Rock { get; set; }
    }
}
