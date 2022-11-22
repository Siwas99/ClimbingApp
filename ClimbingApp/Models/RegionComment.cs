namespace ClimbingApp.Models
{
    public class RegionComment
    {
        public int RegionCommentId { get; set; }
        public string Comment { get; set; }
        public User User { get; set; }
        public Region Region { get; set; }
    }
}
