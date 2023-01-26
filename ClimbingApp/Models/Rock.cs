namespace ClimbingApp.Models
{
    public class Rock
    {
        public int RockId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int Height { get; set; }
        public int Distance { get; set; }
        public int RockFaceExposureId { get; set; }
        public bool IsShadedFromTrees { get; set; }
        public int Popularity { get; set; }
        public bool IsRecommended { get; set; }
        public bool IsLoose { get; set; }
        public float Latitude { get; set; }
        public float Longitude{ get; set; }
        public int AreaId { get; set; }

        public virtual RockFaceExposure RockFaceExposure { get; set; }
        public virtual Area Area { get; set; }
    }
}
