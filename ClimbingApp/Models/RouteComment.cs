namespace ClimbingApp.Models
{
    public class RouteComment
    {
        public int RouteCommentId { get; set; }
        public string Comment { get; set; }
        public User User { get; set; }
        public Route Route { get; set; }
    }
}
