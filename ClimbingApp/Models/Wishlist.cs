namespace ClimbingApp.Models
{
    public class Wishlist
    {
        public int WishlistId { get; set; }
        public int UserId { get; set; }
        public int RouteId { get; set; }

        public virtual User User { get; set; }
        public virtual Route Route { get; set; }
    }
}
