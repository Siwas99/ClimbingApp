namespace ClimbingApp.Models
{
    public class Route
    {
        public int RouteId { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public string? Difficulty { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public int? Year { get; set; }
        public int RockId { get; set; }

        public virtual Rock Rock { get; set; }
    }
}
