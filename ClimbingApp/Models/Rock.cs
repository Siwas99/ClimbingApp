using System.ComponentModel.DataAnnotations;

namespace ClimbingApp.Models
{
    public class Rock
    {
        public int RockId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PhotoURL{ get; set; }
        public int Height { get; set; }
        public int Distance { get; set; }
        public int RockFaceExposureId { get; set; }
        public bool isShadedFromTrees { get; set; }
        public int Popularity { get; set; }
        public bool isRecommended { get; set; }
        public bool isLoose { get; set; }
        public int positionLatitude { get; set; }
        public int positionLogitude{ get; set; }
        public int AreaId { get; set; }

        public virtual RockFaceExposure RockFaceExposure { get; set; }
        public virtual Area Area { get; set; }
    }
}
