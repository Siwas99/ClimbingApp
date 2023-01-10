namespace ClimbingApp.Models
{
    public enum CommentObject
    {
        RegionComment,
        RockComment,
        RouteComment
    }
    public class Comment
    {
        public int CommentId { get; set; }
        public string Commentary { get; set; }
        public int UserId { get; set; }
        public CommentObject CommentObject { get; set; }
        public int CommentObjectId { get; set; }
        
        public virtual User User { get; set; }
    }
}
