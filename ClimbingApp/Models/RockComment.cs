namespace ClimbingApp.Models
{
    public class RockComment
    {
        public int RockCommentId { get; set; }
        public string Comment { get; set; }
        public User User { get; set; }
        public Rock Rock { get; set; }
    }
}
