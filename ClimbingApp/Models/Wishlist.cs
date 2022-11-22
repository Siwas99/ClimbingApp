namespace ClimbingApp.Models
{
    public class Wishlist
    {
        public int WishlistId { get; set; }
        public User User { get; set; }
        public Route Route { get; set; }
    }
}
