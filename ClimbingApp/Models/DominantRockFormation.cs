namespace ClimbingApp.Models
{
    public class DominantRockFormation
    {
        public int DominantRockFormationId { get; set; }
        public int RockFormationId { get; set; }
        public int RockId { get; set; }
        public virtual RockFormation RockFormation { get; set; }
        public virtual Rock Rock { get; set; }
    }
}
